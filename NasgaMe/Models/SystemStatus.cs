using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgaMe.Models
{
    public class SystemStatus
    {
        public bool Repopulate { get; set; }
        public DateTime CurrentYearLastUpdated { get; set; }
    }
}