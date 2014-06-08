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

            //TODO cache this
            var results = DataLayer.DatabaseService.GetAthleteClassPairings().Where(a => a.Contains(term));
            //TODO check to see how many commas are in the resulting string, if more than one, we need to split on the last one
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