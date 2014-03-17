using System.Web.Mvc;

namespace PerfMatters.Flush
{
    public static class ControllerBaseExtension
    {
        public static void Flush(this ControllerBase controller, ActionResult result)
        {
            result.ExecuteResult(controller.ControllerContext);
            controller.ControllerContext.HttpContext.Response.Flush();
        }

        public static void FlushHead(this ControllerBase controller)
        {
            FlushHead(controller, null, null);
        }

        public static void FlushHead(this ControllerBase controller, string title)
        {
            FlushHead(controller, title, null);
        }

        public static void FlushHead(this ControllerBase controller, object model)
        {
            FlushHead(controller, null, model);
        }

        public static void FlushHead(this ControllerBase controller, string title, object model)
        {
            if (title != null)
                controller.ViewBag.Title = title;

            if (model != null)
                controller.ViewData.Model = model;

            var partialViewResult = new PartialViewResult
            {
                ViewName = "_Head",
                ViewData = controller.ViewData,
                TempData = controller.TempData,
            };

            controller.ViewBag.HeadFlushed = true;
            Flush(controller, partialViewResult);
        }
    }
}