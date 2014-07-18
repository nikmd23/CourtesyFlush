using System;
using System.Threading;
using System.Web.Mvc;
using CourtesyFlush;

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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}