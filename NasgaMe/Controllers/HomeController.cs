using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NasgaMe.Aspects;
using NasgaMe.Models;
using Newtonsoft.Json;
using ServiceStack.Mvc;
using NasgaMe.DataLayer;

namespace NasgaMe.Controllers
{
    public class HomeController : Controller
    {
        [UpdateData]
        [HttpGet]
        public ActionResult Index()
        {
            return View(new Search());
        }

        [UpdateData]
        [HttpGet]
        public ActionResult Compare()
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

        [HttpPost]
        public void Compare(Search search)
        {
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
        }

        [HttpGet]
        public JsonResult GetAthletePRs(string nameAndClass)
        {
            if (nameAndClass == String.Empty) return new JsonResult
            {
                Data = string.Empty.ToArray(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            var athlete = new Athlete(nameAndClass);
            var jsonAthletePRs = new JsonAthletePRs(athlete);

            return new JsonResult
            {
                Data = JsonConvert.SerializeObject(jsonAthletePRs),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

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