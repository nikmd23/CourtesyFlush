using System.Web.Mvc;

namespace PerfMatters.Flush
{
    public class FlushHeadAttribute : ActionFilterAttribute
    {
        public string Title { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller;
            controller.FlushHead(Title);
        }
    }
}