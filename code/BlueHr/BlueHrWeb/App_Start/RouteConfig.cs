using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlueHrWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "StaffEdit",
               url: "Staff/Edit/{nr}",
               defaults: new { controller = "Staff", action = "Edit", nr = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "StaffDelete",
              url: "Staff/Delete/{nr}",
              defaults: new { controller = "Staff", action = "Delete", nr = UrlParameter.Optional }
           );

            //routes.MapRoute(
            //    name: "MessageRecordIndex",
            //    url: "MessageRecord/Index/{allOrUnread}/{type}",
            //    defaults: new { controller = "MessageRecord", action = "Index", allOrUnread = UrlParameter.Optional, type = UrlParameter.Optional }
            //    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
