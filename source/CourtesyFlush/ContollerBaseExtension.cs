using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CourtesyFlush
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
            FlushHead(controller, null, null, null);
        }

        public static void FlushHead(this ControllerBase controller, string title)
        {
            FlushHead(controller, title, null, null);
        }

        public static void FlushHead(this ControllerBase controller, object model)
        {
            FlushHead(controller, null, model, null);
        }

        public static void FlushHead(this ControllerBase controller, string title, object model, string headername)
        {
            if (title != null)
                controller.ViewBag.Title = title;

            if (model != null)
                controller.ViewData.Model = model;

            if (String.IsNullOrWhiteSpace(headername))
                headername = "_Head";

            var partialViewResult = new PartialViewResult
            {
                ViewName = headername,
                ViewData = controller.ViewData,
                TempData = controller.TempData,
            };

            controller.ViewBag.HeadFlushed = true;
            Flush(controller, partialViewResult);
        }

#if NET45
        internal const string FlushedAntiForgeryTokenKey = "_FlushedAntiForgeryToken";

        public static void FlushHead(this ControllerBase controller, string title, object model, bool flushAntiForgeryToken, string headername)
        {
            if (flushAntiForgeryToken) 
                WriteForgeryToken(controller);

            FlushHead(controller, title, model, headername);
        }

        private static void WriteForgeryToken(ControllerBase controller)
        {
            string cookieToken, formToken;
            var context = controller.ControllerContext.HttpContext;

            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            context.Items[FlushedAntiForgeryTokenKey] = formToken;

            if (AntiForgeryConfig.RequireSsl && !context.Request.IsSecureConnection)
            {
                throw new InvalidOperationException("WebPageResources.AntiForgeryWorker_RequireSSL");
                    //TODO: Find string message
            }

            var response = context.Response;
            response.Cookies.Set(new HttpCookie(AntiForgeryConfig.CookieName, cookieToken) {HttpOnly = true});

            if (!AntiForgeryConfig.SuppressXFrameOptionsHeader)
            {
                // Adding X-Frame-Options header to prevent ClickJacking. See
                // http://tools.ietf.org/html/draft-ietf-websec-x-frame-options-10
                // for more information.
                response.AddHeader("X-Frame-Options", "SAMEORIGIN");
            }
        }
#endif
    }
}