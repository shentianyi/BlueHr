using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using BlueHrLib.Log;
using Brilliantech.Framwork.Utils.LogUtil;

namespace BlueHrWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
       // ILogger logger = LogFactory.CreateLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            LogUtil.Logger.Info("系统启动...");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            LogUtil.Logger.Info("系统停止");
        }
    }
}
