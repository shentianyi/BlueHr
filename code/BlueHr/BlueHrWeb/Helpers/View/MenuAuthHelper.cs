using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlueHrLib.Data;
using BlueHrLib.Service.Implement;
using BlueHrWeb.Properties;

namespace BlueHrWeb.Helpers.View
{
    public static class MenuAuthHelper
    {

        public static bool CanShow(string controller, string action)
        {
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            if (user == null || !user.isLocked.HasValue || user.isLocked.Value)
            {
                return false;
            }
            if (user.email == System.Configuration.ConfigurationManager.AppSettings["SysAdministrator"])
            {
                return true;
            }
            if (!user.role.HasValue)
            {
                return false;
            }
            return new SysRoleAuthorizationService(Settings.Default.db).AuthUrl(user.role.Value, controller, action);
        }
    }
}