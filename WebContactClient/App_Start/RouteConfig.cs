using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebContactClient
{
    /// <summary>
    /// RouteConfig - This class has method to registre all URL mapping.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// RouteConfig - This method is used to map URL to controller and its action method.
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Map contact route URL
            routes.MapRoute(
                name: "Contact",
                url: "{controller}/{action}/{aintContactID}",
                defaults: new { controller = "Contact", action = "ContactList", aintContactID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}