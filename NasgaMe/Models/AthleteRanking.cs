using System.Text.RegularExpressions;

namespace NasgaMe.Models
{
    public class AthleteRanking
    {
        public string Id
        {
            get { return string.Format("{0}/{1}/{2}", Year, Class, Rank); }
        }
        public string Year { get; set; }
        public string Class { get; set; }
        public string Rank { get; set; }
        public string Name { get; set; }
        public string TotalPoints { get; set; }
        public string BraemarThrow { get; set; }
        public int BraemarPoints { get; set; }
        public string OpenThrow { get; set; }
        public int OpenPoints { get; set; }
        public string HeavyWeightThrow { get; set; }
        public int HeavyWeightPoints { get; set; }
        public string LightWeightThrow { get; set; }
        public int LightWeightPoints { get; set; }
        public string HeavyHammerThrow { get; set; }
        public int HeavyHammerPoints { get; set; }
        public string LightHammerThrow { get; set; }
        public int LightHammerPoints { get; set; }
        public int CaberPoints { get; set; }
        public string SheafThrow { get; set; }
        public int SheafPoints { get; set; }
        public string WfhThrow { get; set; }
        public int WfhPoints { get; set; }

        public static AthleteRanking ParseAthleteData(string[] athleteData)
        {
            string[] yearAndClass = Regex.Split(athleteData[0], "&nbsp;:&nbsp;");
            var athleteRanking = new AthleteRanking
            {
                Year = yearAndClass[1],
                Class = yearAndClass[0].Replace("All+", ""),
                Rank = athleteData[1],
                Name = athleteData[2],
                TotalPoints = athleteData[3],
                BraemarThrow = athleteData[4],
                BraemarPoints = int.Parse(athleteData[5]),
                OpenThrow = athleteData[6],
                OpenPoints = int.Parse(athleteData[7]),
                HeavyWeightThrow = athleteData[8],
                HeavyWeightPoints = int.Parse(athleteData[9]),
                LightWeightThrow = athleteData[10],
                LightWeightPoints = int.Parse(athleteData[11]),
                HeavyHammerThrow = athleteData[12],
                HeavyHammerPoints = int.Parse(athleteData[13]),
                LightHammerThrow = athleteData[14],
                LightHammerPoints = int.Parse(athleteData[15]),
                CaberPoints = int.Parse(athleteData[16]),
                SheafThrow = athleteData[17],
                SheafPoints = int.Parse(athleteData[18]),
                WfhThrow = athleteData[19],
                WfhPoints = int.Parse(athleteData[20])
            };
            return athleteRanking;
        }
    }
}