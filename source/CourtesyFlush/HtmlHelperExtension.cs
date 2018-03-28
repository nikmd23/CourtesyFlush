using System.Web.Mvc;
using System.Web.Mvc.Html;
using CourtesyFlush;

namespace System.Web.WebPages
{
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString FlushHead(this HtmlHelper html)
        {
            return FlushHead(html, null);
        }

        public static MvcHtmlString FlushHead(this HtmlHelper html, string headername)
        {
            if (String.IsNullOrWhiteSpace(headername))
                headername = "_Head";

            if (!html.ViewData.ContainsKey("HeadFlushed"))
                return html.Partial(headername);

            return new MvcHtmlString(string.Empty);
        }

#if NET45
        public static MvcHtmlString FlushedAntiForgeryToken(this HtmlHelper html)
        {
            var token = html.ViewContext.HttpContext.Items[ControllerBaseExtension.FlushedAntiForgeryTokenKey] as string;

            if (string.IsNullOrEmpty(token))
            {
                // Fall back to the standard AntiForgeryToken if no FlushedAntiForgeryToken exists.
                return html.AntiForgeryToken();
            }

            var tag = new TagBuilder("input");
            tag.Attributes["type"] = "hidden";
            tag.Attributes["name"] = "__RequestVerificationToken";
            tag.Attributes["value"] = token;

            return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
        }
#endif
    }
}
