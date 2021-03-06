﻿using System.Web;
using System.Web.Optimization;

namespace NasgaMe
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                         "~/Scripts/jquery.blockUI.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                 "~/Scripts/search.js"));

            bundles.Add(new ScriptBundle("~/bundles/results").Include(
                 "~/Scripts/results.js"));

            bundles.Add(new ScriptBundle("~/bundles/compare").Include(
                "~/Scripts/compare.js"));

            bundles.Add(new ScriptBundle("~/bundles/compare2").Include(
              "~/Scripts/compare2.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap3-typeahead.min.js",
                      "~/Scripts/bootstrap-sortable.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-sortable.css",
                      "~/Content/Site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
