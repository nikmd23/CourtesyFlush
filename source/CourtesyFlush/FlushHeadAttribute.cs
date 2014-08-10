using System;
using System.Web.Mvc;

namespace CourtesyFlush
{
    public class FlushHeadAttribute : ActionFilterAttribute
    {
        public string Title { get; set; }
#if NET45
        public bool FlushAntiForgeryToken { get; set; }
#endif

        private readonly Func<ActionDescriptor, ViewDataDictionary> _viewDataFunction;

        public FlushHeadAttribute()
        {
        }

        public FlushHeadAttribute(Func<ActionDescriptor, ViewDataDictionary> viewDataFunction)
        {
            _viewDataFunction = viewDataFunction;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller;

            if (_viewDataFunction != null)
                controller.ViewData = _viewDataFunction(filterContext.ActionDescriptor);
#if NET45            
            controller.FlushHead(Title, null, FlushAntiForgeryToken);
#else
            controller.FlushHead(Title, null);
#endif
        }
    }
}