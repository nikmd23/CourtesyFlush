using System.Web.Mvc;
using System.Web.Mvc.Html;

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
    }
}