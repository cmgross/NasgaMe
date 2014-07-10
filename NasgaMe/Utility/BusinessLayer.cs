using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using NasgaMe.DataLayer;
using NasgaMe.Models;
using Scraper;

namespace NasgaMe.Utility
{
	public static class BusinessLayer
	{
		public static void UpdateCurrentYearRankings()
		{
			//TODO breakapart these business layer methods to adhere to single responsbility principle!!!
			var currentYear = DateTime.Now.Year;
			DatabaseService.PurgeRankingsForYear(currentYear.ToString());
			if (NasgaScrape(new List<int> { currentYear })) DatabaseService.Update(new SystemStatus { CurrentYearLastUpdated = DateTime.Now });
		}

		public static DateTime RepopulateDatabase()
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

	   public static bool NasgaScrape(IEnumerable<int> years)
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