using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.ServiceModel;
using System.ServiceModel.Activation;
using RESTPerf.Web.WCFService;

namespace RESTPerf.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Product", // Route name
                "Product/{id}", // URL with parameters
                new { controller = "Product", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "ProductAsync", // Route name
                "ProductAsync/{id}", // URL with parameters
                new { controller = "ProductAsync", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            // Edit the base address of Service1 by replacing the "Service1" string below
            routes.Add(new ServiceRoute("WCFService/Product", new WebServiceHostFactory(), typeof(ProductService)));
            routes.Add(new ServiceRoute("WCFService/ProductAsync", new WebServiceHostFactory(), typeof(ProductAsyncService)));


            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}