﻿using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NasgaMe.Models;
using ServiceStack.Mvc;
using NasgaMe.DataLayer;

namespace NasgaMe.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()  
        {
            return View(new Search());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Search search)
        {
            if (!ModelState.IsValid) return View("Error");
            var athlete = new Athlete(search.NameAndClass);
            var athleteRecordsViewModel = new AthleteRecordsViewModel(athlete);
            return View("Results", athleteRecordsViewModel); 
        }

        [HttpGet]
        public JsonResult GetSearchResults(string term)
        {
            if (term == String.Empty) return new JsonResult
            {
                Data = string.Empty.ToArray(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            term = term.ToLower();
            var results = DatabaseService.GetAthleteClassPairings().Where(a => a.ToLower().Contains(term));
            return new JsonResult
            {
                Data = results.ToArray(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }//TODO 6 later later: flight compare

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new ServiceStackJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }
    }
}