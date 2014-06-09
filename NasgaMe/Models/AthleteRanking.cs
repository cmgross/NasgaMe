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
                Class = FormatClass(yearAndClass[0]),
                Rank = string.Format("{0}/{1}", athleteData[2], athleteData[1]),
                Name = FormatName(athleteData[3]),
                TotalPoints = athleteData[4],
                BraemarThrow = athleteData[5],
                BraemarPoints = int.Parse(athleteData[6]),
                OpenThrow = athleteData[7],
                OpenPoints = int.Parse(athleteData[8]),
                HeavyWeightThrow = athleteData[9],
                HeavyWeightPoints = int.Parse(athleteData[10]),
                LightWeightThrow = athleteData[11],
                LightWeightPoints = int.Parse(athleteData[12]),
                HeavyHammerThrow = athleteData[13],
                HeavyHammerPoints = int.Parse(athleteData[14]),
                LightHammerThrow = athleteData[15],
                LightHammerPoints = int.Parse(athleteData[16]),
                CaberPoints = int.Parse(athleteData[17]),
                SheafThrow = athleteData[18],
                SheafPoints = int.Parse(athleteData[19]),
                WfhThrow = athleteData[20],
                WfhPoints = int.Parse(athleteData[21])
            };
            return athleteRanking;
        }

        private static string FormatName(string unformattedName)
        {
            //data from Nasgaweb contains a nonbreaking space character, as well as commas that we want eliminated
            return unformattedName.Replace("&nbsp;", " ").Replace(",", " ");
        }

        private static string FormatClass(string athleteClass)
        {
            //classes contain All+ as well as extra pluralization, ie, All+Amateurs, we simply want Amataeur
            switch (athleteClass)
            {
                case "All+Amateurs":
                    return "Amateur";
                case "All+Masters":
                    return "Master";
                case "All+Women":
                    return "Women";
                default:
                    return athleteClass;
            }
        }
    }
}