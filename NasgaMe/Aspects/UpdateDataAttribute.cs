using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgaMe.DataLayer;
using PostSharp.Aspects;
using NasgaMe.Utility;

namespace NasgaMe.Aspects
{
    [Serializable]
    public class UpdateDataAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            var systemStatus = DatabaseService.GetSystemStatus();
            if (systemStatus.Repopulate) systemStatus.CurrentYearLastUpdated = BusinessLayer.RepopulateDatabase();
            if (systemStatus.CurrentYearLastUpdated.Date < DateTime.Now.Date) BusinessLayer.UpdateCurrentYearRankings();

        }
    }
}