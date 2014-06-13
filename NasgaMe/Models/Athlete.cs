using System.Collections.Generic;
using System.Linq;
using NasgaMe.DataLayer;

namespace NasgaMe.Models
{
    public class Athlete
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public List<Record> Records { get; set; }

        public class Record
        {
            public string Year { get; set; }
            public string Rank { get; set; }
            public string Braemar { get; set; }
            public string Open { get; set; }
            public string HeavyWeight { get; set; }
            public string LightWeight { get; set; }
            public string HeavyHammer { get; set; }
            public string LightHammer { get; set; }
            public string Caber { get; set; }
            public string Sheaf { get; set; }
            public string Wfh { get; set; }

        }

        public Athlete(string nameAndClass)
        {
            var nameAndClassSplit = nameAndClass.Split(',');
            Name = nameAndClassSplit[0].Trim();
            Class = nameAndClassSplit[1].Trim();
            Records = new List<Record>();
            var athleteRankings = DatabaseService.GetAthleteRankings(Name, Class);
            AllTimeRecords(athleteRankings);
            CreateRecords(athleteRankings);
        }

        private void CreateRecords(IEnumerable<AthleteRanking> athleteRankings)
        {
            foreach (var athleteRanking in athleteRankings.OrderByDescending(r => r.Year))
            {
                var record = new Record
                {
                    Year = athleteRanking.Year,
                    Rank = athleteRanking.Rank,
                    Braemar = athleteRanking.BraemarThrow,
                    Open = athleteRanking.OpenThrow,
                    HeavyWeight = athleteRanking.HeavyWeightThrow,
                    LightWeight = athleteRanking.LightWeightThrow,
                    HeavyHammer = athleteRanking.HeavyHammerThrow,
                    LightHammer = athleteRanking.LightHammerThrow,
                    Caber = athleteRanking.CaberPoints.ToString(),
                    Sheaf = athleteRanking.SheafThrow,
                    Wfh = athleteRanking.WfhThrow

                };
                Records.Add(record);
            }
        }

        private void AllTimeRecords(List<AthleteRanking> athleteRankings)
        {
            var bestBraemar = athleteRankings.OrderByDescending(b => b.BraemarPoints).First().BraemarThrow;
            var bestOpen = athleteRankings.OrderByDescending(o => o.OpenPoints).First().OpenThrow;
            var bestHeavy = athleteRankings.OrderByDescending(b => b.HeavyWeightPoints).First().HeavyWeightThrow;
            var bestLight = athleteRankings.OrderByDescending(b => b.LightWeightPoints).First().LightWeightThrow;
            var bestHeavyHammer = athleteRankings.OrderByDescending(b => b.HeavyHammerPoints).First().HeavyHammerThrow;
            var bestLightHammer = athleteRankings.OrderByDescending(b => b.LightHammerPoints).First().LightHammerThrow;
            var bestCaber = athleteRankings.OrderByDescending(b => b.CaberPoints).First().CaberPoints.ToString();
            var bestSheaf = athleteRankings.OrderByDescending(b => b.SheafPoints).First().SheafThrow;
            var bestWfh = athleteRankings.OrderByDescending(b => b.WfhPoints).First().WfhThrow;

            var allTimeRecords = new Record
            {
                Year = "All Time",
                Rank = "-",
                Braemar = bestBraemar,
                Open = bestOpen,
                HeavyWeight = bestHeavy,
                LightWeight = bestLight,
                HeavyHammer = bestHeavyHammer,
                LightHammer = bestLightHammer,
                Caber = bestCaber,
                Sheaf = bestSheaf,
                Wfh = bestWfh
            };
            Records.Add(allTimeRecords);
        }
    }


}