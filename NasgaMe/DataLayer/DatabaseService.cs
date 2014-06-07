using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NasgaMe.Models;
using ServiceStack.OrmLite;

namespace NasgaMe.DataLayer
{
    public class DatabaseService
    {
        #region Generics
        public static void Insert<T>(T value)
        {
            using (var db = MvcApplication.DbFactory.OpenDbConnection())
            {
                db.Insert(value);
            }
        }

        public static void Update<T>(T value)
        {
            using (var db = MvcApplication.DbFactory.OpenDbConnection())
            {
                db.Update(value);
            }
        }
        #endregion

        public static bool BulkInsert(List<AthleteRanking> athleteRankings)
        {
            try
            {
                using (var db = MvcApplication.DbFactory.OpenDbConnection())
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

        public static SystemStatus GetSystemStatus()
        {
            SystemStatus status;
            using (var db = MvcApplication.DbFactory.OpenDbConnection())
            {
                status = db.Select<SystemStatus>().FirstOrDefault();
            }
            return status ?? new SystemStatus {Repopulate = true};
        }

        public static void PurgeData()
        {
            using (var db = MvcApplication.DbFactory.OpenDbConnection())
            {
                db.DeleteAll<SystemStatus>();
                db.DeleteAll<AthleteRanking>();
            }
        }

        public static void PurgeRankingsForYear(string year)
        {
            using (var db = MvcApplication.DbFactory.OpenDbConnection())
            {
                db.Delete<AthleteRanking>(p => p.Year == year);
            }
        }
    }
}