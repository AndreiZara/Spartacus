﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Spartacus.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Account",
            //    url: "Account/{action}/{id}",
            //    defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
