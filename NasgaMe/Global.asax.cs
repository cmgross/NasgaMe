using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NasgaMe.DataLayer;
using NasgaMe.Models;
using Scraper;
using ServiceStack.OrmLite;

namespace NasgaMe
{
	public class MvcApplication : HttpApplication
	{
		public static OrmLiteConnectionFactory DbFactory;
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			DbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["NasgaMe"].ConnectionString, SqlServerDialect.Provider);

			using (var db = DbFactory.OpenDbConnection())
			{
				const bool overwrite = false;
				db.CreateTables(overwrite, typeof(AthleteRanking));
				db.CreateTables(overwrite, typeof(SystemStatus));
			}

			//TODO 3.) refactor below to business layer aspect (and separate/simplify methods to do one thing, ie no database calls in method that does something else), and add to controllers so that updates are checked every load but only done once per day
			//TODO 4.) uninstall/reinstall ServiceStack, provide API to return athlete records as json array: nasga.me/athlete/{name}/{class}
			
			var systemStatus = DatabaseService.GetSystemStatus();
			if (systemStatus.Repopulate) systemStatus.CurrentYearLastUpdated = RepopulateDatabase();
			if (systemStatus.CurrentYearLastUpdated.Date < DateTime.Now.Date) UpdateCurrentYearRankings();
		}

		private void UpdateCurrentYearRankings()
		{
			var currentYear = DateTime.Now.Year;
			DatabaseService.PurgeRankingsForYear(currentYear.ToString());
			if (NasgaScrape(new List<int> { currentYear })) DatabaseService.Update(new SystemStatus { CurrentYearLastUpdated = DateTime.Now });
		}

		private DateTime RepopulateDatabase()
		{
			DatabaseService.PurgeData();
			var yearsToScrape = new List<int>();
			for (var i = 2009; i < DateTime.Now.AddYears(1).Year; i++)
				yearsToScrape.Add(i);

			SystemStatus systemStatus;
			DateTime updatedDateTime;
			if (NasgaScrape(yearsToScrape))
			{

				systemStatus = new SystemStatus { Repopulate = false, CurrentYearLastUpdated = DateTime.Now };
				updatedDateTime = DateTime.Now;
			}
			else
			{
				systemStatus = new SystemStatus { Repopulate = true };
				updatedDateTime = new DateTime();
			}
			DatabaseService.Insert(systemStatus);
			return updatedDateTime;
		}

		private bool NasgaScrape(IEnumerable<int> years)
		{
			var athleteClasses = WebConfigurationManager.AppSettings["AthleteClasses"].Split(',');
			var url = WebConfigurationManager.AppSettings["AthleteUrl"];
			var athleteRankings = new List<AthleteRanking>();
			foreach (var year in years)
			{
				var allParameters = athleteClasses.Select(athleteClass => new List<Tuple<string, string>>
				{
					new Tuple<string, string>("class", athleteClass), new Tuple<string, string>("rankyear", year.ToString())
				}).ToList();

				var resultsForYear = Scrape.asyncScrape(url, allParameters);
				athleteRankings.AddRange(resultsForYear.Select(AthleteRanking.ParseAthleteData).ToList());
			}
			return DatabaseService.BulkInsert(athleteRankings);
		}
	}
}
