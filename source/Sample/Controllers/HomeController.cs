using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CourtesyFlush;
using Sample.Properties;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {
        [FlushHead(Title = "Index")]
        public ActionResult Index()
        {
            Thread.Sleep(2000);

            return View();
        }

    

        public ActionResult About()
        {
            ViewBag.Title = DateTime.Now.Second;
            this.FlushHead();

            Thread.Sleep(2000);
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Flushes an alternative header if this pages is using an alternative layout (master page).
        /// </summary>
        /// <returns></returns>
        [FlushHead(Title = "About Alternative", HeaderName = "_Head_Alternative")]
        public ActionResult AboutUs()
        {
            Thread.Sleep(2000);

            return View();
        }

        [FlushHead(TitleResourceName = "mytitlefromresources", TitleResourceType = typeof(Resources))]
        public ActionResult Title()
        {
            Thread.Sleep(2000);

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(string name)
        {
            return Content("Yey");
        }

     
        //[HttpGet, FlushHead(Title = "I am flushed", FlushAntiForgeryToken = true)]
        //public ActionResult Contact()
        //{
        //    return View();
        //}
    

        /*
        [HttpGet]
        public ActionResult Contact()
        {
            string cookieToken;
            string formToken;

            AntiForgery.GetTokens(null, out cookieToken, out formToken);

            if (AntiForgeryConfig.RequireSsl && !Request.IsSecureConnection)
            {
                throw new InvalidOperationException("WebPageResources.AntiForgeryWorker_RequireSSL"); //TODO: Find string message
            }

            Response.Cookies.Set(new HttpCookie(AntiForgeryConfig.CookieName, cookieToken){HttpOnly = true});

            if (!AntiForgeryConfig.SuppressXFrameOptionsHeader)
            {
                // Adding X-Frame-Options header to prevent ClickJacking. See
                // http://tools.ietf.org/html/draft-ietf-websec-x-frame-options-10
                // for more information.
                Response.AddHeader("X-Frame-Options", "SAMEORIGIN");
            }


            var retVal = new TagBuilder("input");
            retVal.Attributes["type"] = "hidden";
            retVal.Attributes["name"] = "__RequestVerificationToken";
            retVal.Attributes["value"] = formToken; //TODO: Move to HTML Helper method

            ViewBag.Message = "Your contact page.";
            ViewBag.Tag = retVal.ToString();

            return View();
        }
*/
    }
}