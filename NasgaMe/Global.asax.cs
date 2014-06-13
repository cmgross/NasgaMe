using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NasgaMe.DataLayer;
using NasgaMe.Models;
using Scraper;
using ServiceStack.OrmLite;

namespace NasgaMe
{
	public class MvcApplication : HttpApplication
	{
		public static OrmLiteConnectionFactory DbFactory;
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			DbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["NasgaMe"].ConnectionString, SqlServerDialect.Provider);

			using (var db = DbFactory.OpenDbConnection())
			{
				const bool overwrite = false;
				db.CreateTables(overwrite, typeof(AthleteRanking));
				db.CreateTables(overwrite, typeof(SystemStatus));
			}
            //TODO 2.) hide table until tbody elements count > 0
			//TODO 3.) uninstall/reinstall ServiceStack, provide API to return athlete records as json array: nasga.me/athlete/{name}/{class}
		}
	}
}
