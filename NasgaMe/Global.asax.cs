using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NasgaMe.Models;
using Scraper;

namespace NasgaMe
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			//TODO seed method to get prior years as far back as the database goes: 2009
			//TODO method to check if the data was already updated today
			//TODO if data already updated, return; else purge data, scrape data like below, then update data update date
			//TODO decide format for storing updated date. Done by class or overall?
			//TODO later: index page is search, return any atlete matching search name and the respective class, clicking that result loads the results for that athlete
			//TODO later later: flight compare

			var currentYear = DateTime.Now.Year;
			var athleteClasses = WebConfigurationManager.AppSettings["AthleteClasses"].Split(',');
			var url = WebConfigurationManager.AppSettings["AthleteUrl"];
			var allParameters = athleteClasses.Select(athleteClass => new List<Tuple<string, string>>
			{
			    new Tuple<string, string>("class", athleteClass), new Tuple<string, string>("rankyear", currentYear.ToString())
			}).ToList();

		    var resultsForYear = Scrape.asyncScrape(url, allParameters);
			List<AthleteRanking> athleteRankings = resultsForYear.Select(AthleteRanking.ParseAthleteData).ToList();
			//pass to datalayer method that takes a list of athlete rankings, opens database connection, and saves them
			//save updated for the year
		}
	}
}
