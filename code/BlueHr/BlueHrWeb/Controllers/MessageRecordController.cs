using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.Helpers;
using BlueHrWeb.Models;
using BlueHrWeb.Properties;
using MvcPaging;
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
        // allOrUnread: unread/all
        // type: alert/manage/basic/all
        public ActionResult Index(string allOrUnread="unread",string type="alert",int? page=null)
        {
            ViewBag.allorUnread = allOrUnread;
            ViewBag.type = type;


            int pageIndex = PagingHelper.GetPageIndex(page);
            IMessageRecordService mrs = new MessageRecordService(Settings.Default.db);
            IPagedList<MessageRecordView> records = mrs
                .GetByTypesAndAllOrUnread(MessageCategory.CateListDic[type], (allOrUnread=="all" ? true : false))
                .ToPagedList<MessageRecordView>(pageIndex, Settings.Default.pageSize);


            ViewBag.alertCount = 0;
            ViewBag.manageCount = 0;
            ViewBag.basicCount = 0;
            ViewBag.all = 0;
            return View(records);
        }

        [HttpGet] 
        public ActionResult UnReadCount()
        {
            return Json(new MessageRecordService(Settings.Default.db).CountUnRead(),JsonRequestBehavior.AllowGet);
        }
    }
}