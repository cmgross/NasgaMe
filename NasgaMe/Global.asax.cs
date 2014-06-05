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
            //var multiFormValues = new List<NameValueCollection>();
            var results = new List<string[]>();
            DateTime start = DateTime.Now;
           
           
            foreach (var athleteClass in athleteClasses)
            {
                var formValues = new NameValueCollection  //"class=Pro&rankyear=2013&x=26&y=10"
                        {
                            {"class", athleteClass},
                            {"rankyear", currentYear.ToString()},
                            {"x", "26"},
                            {"y", "10"}
                        };
                results.AddRange(Scrape.scrapeYearAndClass(url, formValues));//this is the call to the F# library
                //multiFormValues.Add(formValues);
            }
            TimeSpan timeDiff = DateTime.Now - start; //~6 seconds
            var totalTime = timeDiff.TotalSeconds;
            //var resultsForYear = Scraper.Scrape.scrapeClassesForYear(url, multiFormValues);


            //var cResults = AthleteRanking.GetResults("All+Amateurs", currentYear.ToString());
            //get list of results back for given year and store it in database, then update http://stackoverflow.com/questions/11721360/is-it-possible-to-create-batch-insert
            //get back list of string[]s, sent to method that takes List<string[]> and converts to AthleteRanking, pass to method that saves List<AthleteRanking> to db
        }
    }
}
