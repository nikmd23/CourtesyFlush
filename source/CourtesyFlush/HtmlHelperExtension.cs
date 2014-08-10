using System.Web.Mvc;
using System.Web.Mvc.Html;
using CourtesyFlush;

namespace System.Web.WebPages
{
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString FlushHead(this HtmlHelper html)
        {
            if (!html.ViewData.ContainsKey("HeadFlushed"))
                return html.Partial("_Head");

            return new MvcHtmlString(string.Empty);
        }

#if NET45
        public static MvcHtmlString FlushedAntiForgeryToken(this HtmlHelper html)
        {
            var token = html.ViewContext.HttpContext.Items[ControllerBaseExtension.FlushedAntiForgeryTokenKey] as string;

            var tag = new TagBuilder("input");
            tag.Attributes["type"] = "hidden";
            tag.Attributes["name"] = "__RequestVerificationToken";
            tag.Attributes["value"] = token;

            return new MvcHtmlString(tag.ToString());
        }
#endif
    }
}