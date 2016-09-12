using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueHrWeb.CustomAttributes
{
    public class AdminAuthorizeAttribute : System.Web.Mvc.FilterAttribute, System.Web.Mvc.IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            if (user == null || user.role != (int)RoleType.Admin)
            {
                System.Web.HttpContext.Current.Session["user"] = null;
                filterContext.Result =
                 new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary {{ "controller", "Account" },
                                              { "action", "Login" },
                                             { "returnUrl",    filterContext.HttpContext.Request.RawUrl } });
                return;
            }
        }
    }
}