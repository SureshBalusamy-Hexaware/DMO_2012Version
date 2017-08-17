using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DM_UI
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
                //defaults: new { controller = "Hexarule", action = "Index", id = UrlParameter.Optional }                
                //defaults: new { controller = "InfaGen", action = "InfaGenConfig", id = UrlParameter.Optional }
                //defaults: new { controller = "DIMA", action = "DIMAConfig", id = UrlParameter.Optional }
                //defaults: new { controller = "DashBoard", action = "Index", id=UrlParameter.Optional }
                //defaults: new { controller = "DataProfiler", action = "Configuration", id = UrlParameter.Optional }                
                //defaults: new { controller = "Automaton", action = "Configuration", id = UrlParameter.Optional }                
            );
        }
    }
}
