using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EntitySolution.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "1",
               url: "{Home}/{ProductDetails}/{id}",
               defaults: new { controller = "Home", action = "ProductDetails", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "2",
              url: "{Home}/{NewsDetail}/{id}",
              defaults: new { controller = "Home", action = "NewsDetail", id = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "3",
             url: "{Home}/{Search}/{key}",
             defaults: new { controller = "Home", action = "Search", key = UrlParameter.Optional }
         );
             
        }
    }
}
