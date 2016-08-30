using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueHrLib.Log;

namespace BlueHrWeb.Controllers
{
    public class HomeController : Controller
    {
        ILogger logger = LogFactory.CreateLogger(typeof(HomeController));

        public ActionResult Index()
        {
            logger.Info("Home控制器");
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
    }
}