using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brilliantech.Framwork.Utils.LogUtil;
using BlueHrWeb.CustomAttributes;
//using BlueHrLib.Log;

namespace BlueHrWeb.Controllers
{
    public class HomeController : Controller
    {
        // ILogger logger = LogFactory.CreateLogger(typeof(HomeController));

        [UserAuthorize]
        public ActionResult Index()
        {
            LogUtil.Logger.Info("Home控制器");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NoAuthPage(string id)
        {
            //没有权限访问的类别

            if (id == "1")
            {
                ViewBag.Msg = "抱歉，你没有权限访问该页面，请联系管理员！";
            }

            if (id == "2")
            {
                ViewBag.Msg = "抱歉，你没有权限进行操作，请联系管理员！";
            }

            return View();
        }
    }
}