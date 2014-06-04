using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NasgaScraper;

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
            foreach (var athleteClass in athleteClasses) //TODO launch these at the same time using async
            {
                //get list of results from fsharp code, write them to database
            }
            //var x = NasgaScrape.AddNumbers(10, 20);
            //var y = NasgaScrape.SubtractNumbers(10, 20);
            //get list of results back for given year and store it in database, then update
        }
    }
}
