using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgaMe.Models
{
    public class AthleteRecordsViewModel
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public List<string> Years { get; set; }
        public List<string> Ranks { get; set; }
        public List<string> BraemarThrows { get; set; }
        public List<string> OpenThrows { get; set; }
        public List<string> HeavyWeightThrows { get; set; }
        public List<string> LightWeightThrows { get; set; }
        public List<string> HeavyHammerThrows { get; set; }
        public List<string> LightHammerThrows { get; set; }
        public List<string> CaberPoints { get; set; }
        public List<string> SheafThrows { get; set; }
        public List<string> WfhThrows { get; set; }

        public AthleteRecordsViewModel(Athlete athlete)
        {
            Name = athlete.Name;
            Class = athlete.Class;
            List<Athlete.Record> orderedRecords = athlete.Records.OrderByDescending(c => c.Year).ToList();
            Years = orderedRecords.Select(y => y.Year).ToList();
            Ranks = orderedRecords.Select(r => r.Rank).ToList();
            BraemarThrows = orderedRecords.Select(b => b.Braemar).ToList();
            OpenThrows = orderedRecords.Select(o => o.Open).ToList();
            HeavyWeightThrows = orderedRecords.Select(h => h.HeavyWeight).ToList();
            LightWeightThrows = orderedRecords.Select(l => l.LightWeight).ToList();
            HeavyHammerThrows = orderedRecords.Select(hh => hh.HeavyHammer).ToList();
            LightHammerThrows = orderedRecords.Select(lh => lh.LightHammer).ToList();
            CaberPoints = orderedRecords.Select(c => c.Caber).ToList();
            SheafThrows = orderedRecords.Select(s => s.Sheaf).ToList();
            WfhThrows = orderedRecords.Select(w => w.Wfh).ToList();
        }
    }
}