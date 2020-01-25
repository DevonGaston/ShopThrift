using System.Web;
using System.Web.Mvc;

namespace ShopThrift.WebUIs
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
