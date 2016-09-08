using BlueHrLib.Data.Message;
using BlueHrLib.Service.Implement;
using BlueHrWeb.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueHrWeb.Controllers
{
    public class MessageRecordController : Controller
    {
        // GET: MessageRecord
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet] 
        public ActionResult UnReadCount()
        {
            return Json(new MessageRecordService(Settings.Default.db).CountUnRead(),JsonRequestBehavior.AllowGet);
        }
    }
}