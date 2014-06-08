using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ServiceStack.Mvc;

namespace NasgaMe.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetSearchResults(string term)
        {
            if (term == String.Empty) return new JsonResult
            {
                Data = string.Empty.ToArray(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            //TODO later: index page is search, return any atlete matching search name and the respective class, clicking that result loads the results for that athlete
            //TODO later later: flight compare
            //TODO cache this
            var results = DataLayer.DatabaseService.GetAthleteClassPairings().Where(a => a.Contains(term));
            return new JsonResult
            {
                Data = results.ToArray(),
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