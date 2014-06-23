using System.Web;
using System.Web.Mvc;
using PerfMatters.Flush;

namespace Sample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new FlushHeadAttribute(actionDescriptor => new ViewDataDictionary<int>
            {
                {"Title", "Global"},
                {"Description", "This is the meta description."}
            }));
        }
    }
}
