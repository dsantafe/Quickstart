using AutoMapper;
using Quickstart.BL.DTOs;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Quickstart.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        internal static MapperConfiguration MapperConfiguration { get; set; } 
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MapperConfiguration = MapperConfig.MapperConfiguration();
        }
    }
}
