using System;
using System.Reflection;
using System.Web.Mvc;

namespace CourtesyFlush
{
    public class FlushHeadAttribute : ActionFilterAttribute
    {
        public string Title { get; set; }
        public string TitleResourceName { get; set; }
        public Type TitleResourceType { get; set; }


        public string HeaderName { get; set; }
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

            //Display the localized title from the resource files.
            if (String.IsNullOrWhiteSpace(Title) && !String.IsNullOrWhiteSpace(TitleResourceName) && TitleResourceType != null)
            {
                PropertyInfo nameProperty = TitleResourceType.GetProperty(TitleResourceName, BindingFlags.Static | BindingFlags.Public);
                Title = (string)nameProperty.GetValue(nameProperty.DeclaringType, null);
            }

#if NET45            
            controller.FlushHead(Title, null, FlushAntiForgeryToken, HeaderName);
#else
            controller.FlushHead(Title, null, HeaderName);
#endif
        }
    }
}