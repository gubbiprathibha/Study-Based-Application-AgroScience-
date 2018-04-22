using System.Web;
using System.Web.Mvc;

namespace StudyBasedApplication.WebApp.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}