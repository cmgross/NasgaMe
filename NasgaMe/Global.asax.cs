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

			DbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["NasgaMe"].ConnectionString,SqlServerDialect.Provider);

			using (var db = DbFactory.OpenDbConnection())
			{
				const bool overwrite = false;
				db.CreateTables(overwrite, typeof(AthleteRanking));
                db.CreateTables(overwrite, typeof(SystemStatus));
			}

		    var systemStatus = DatabaseService.GetSystemStatus();
		    if (systemStatus.Repopulate) systemStatus.CurrentYearLastUpdated = RepopulateDatabase();
		    if (systemStatus.CurrentYearLastUpdated.Date < DateTime.Now.Date) UpdateCurrentYearRankings();
            //TODO later: index page is search, return any atlete matching search name and the respective class, clicking that result loads the results for that athlete
            //TODO later later: flight compare
		}

        private void UpdateCurrentYearRankings()
        {
            var currentYear = DateTime.Now.Year;
            DatabaseService.PurgeRankingsForYear(currentYear.ToString());
            if (NasgaScrape()) DatabaseService.Update(new SystemStatus{CurrentYearLastUpdated = DateTime.Now});
        }

        private DateTime RepopulateDatabase()
        {
            DatabaseService.PurgeData();
            //TODO seed method to get prior years as far back as the database goes: 2009, including current year
            //method to scrape data that takes classes listing, and list of years, and inserts them
            //if NasgaScrape() //give it list of all years including current
            var now = DateTime.Now;
            var systemStatus = new SystemStatus {Repopulate = false, CurrentYearLastUpdated = now};
            DatabaseService.Insert(systemStatus);
            return now;
        }

	    private bool NasgaScrape() //take list of ints which are the years to scrape
	    {
            var athleteClasses = WebConfigurationManager.AppSettings["AthleteClasses"].Split(',');
            var url = WebConfigurationManager.AppSettings["AthleteUrl"];
            var allParameters = athleteClasses.Select(athleteClass => new List<Tuple<string, string>>
			{
				new Tuple<string, string>("class", athleteClass), new Tuple<string, string>("rankyear", currentYear.ToString())
			}).ToList();

            var resultsForYear = Scrape.asyncScrape(url, allParameters);
            List<AthleteRanking> athleteRankings = resultsForYear.Select(AthleteRanking.ParseAthleteData).ToList();
            var worked = DatabaseService.BulkInsert(athleteRankings);
	    }
	}
}
