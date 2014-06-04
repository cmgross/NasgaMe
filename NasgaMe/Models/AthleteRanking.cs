namespace NasgaMe.Models
{
    public class AthleteRanking
    {
        public string Id
        {
            get { return Year + "/" + Class + "/" + Rank; }
        }
        public string Year { get; set; }
        public string Class { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public int TotalPoints { get; set; }
        public string BraemarThrow { get; set; } //save in the format it needs to be in
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
    }
}