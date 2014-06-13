using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgaMe.Models
{
    public class JsonAthletePRs
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public string Braemar { get; set; }
        public string Open { get; set; }
        public string HeavyWeight { get; set; }
        public string LightWeight { get; set; }
        public string HeavyHammer { get; set; }
        public string LightHammer { get; set; }
        public string Caber { get; set; }
        public string Sheaf { get; set; }
        public string Wfh { get; set; }

        public JsonAthletePRs(Athlete athlete)
        {
            var allTimePRs = athlete.Records.First(r => r.Year == "All Time");
            Name = athlete.Name;
            Class = athlete.Class;
            Braemar = allTimePRs.Braemar;
            Open = allTimePRs.Open;
            HeavyWeight = allTimePRs.HeavyWeight;
            LightWeight = allTimePRs.LightWeight;
            HeavyHammer = allTimePRs.HeavyHammer;
            LightHammer = allTimePRs.LightHammer;
            Caber = allTimePRs.Caber;
            Sheaf = allTimePRs.Sheaf;
            Wfh = allTimePRs.Wfh;
        }
    }
}