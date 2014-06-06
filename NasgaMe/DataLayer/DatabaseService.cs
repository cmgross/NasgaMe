using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NasgaMe.Models;
using ServiceStack.OrmLite;

namespace NasgaMe.DataLayer
{
    public class DatabaseService
    {
        public static bool BulkInsert(List<AthleteRanking> athleteRankings)
        {
            try
            {
                using (IDbConnection db = MvcApplication.DbFactory.OpenDbConnection())
                {
                    foreach (var athleteRanking in athleteRankings)
                    {
                        db.Insert(athleteRanking);
                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}