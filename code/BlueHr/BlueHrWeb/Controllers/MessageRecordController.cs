using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.Search;
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
using BlueHrWeb.CustomAttributes;
using BlueHrLib.Helper;

namespace BlueHrWeb.Controllers
{
    public class MessageRecordController : Controller
    {
        // GET: MessageRecord
        // allOrUnread: unread/all
        // type: alert/manage/basic/all
        [UserAuthorize]
        public ActionResult Index(string allOrUnread="unread",int? page=null)
        {
            ViewBag.allOrUnread = allOrUnread;

            MessageRecordSearchModel q = new MessageRecordSearchModel();

            int pageIndex = PagingHelper.GetPageIndex(page);
            IMessageRecordService mrs = new MessageRecordService(Settings.Default.db);
            IPagedList<MessageRecordView> records = mrs
                .GetByCateAndAllOrUnread(MessageRecordCatetory.Alert, (allOrUnread=="all" ? true : false))
                .ToPagedList<MessageRecordView>(pageIndex,  Settings.Default.pageSize);

            ViewBag.Query = q;

            return View(records);
        }

        public ActionResult ToEmployees(string allOrUnread = "unread", int? page = null)
        {
            ViewBag.allOrUnread = allOrUnread;

            MessageRecordSearchModel q = new MessageRecordSearchModel();

            int pageIndex = PagingHelper.GetPageIndex(page);
            IMessageRecordService mrs = new MessageRecordService(Settings.Default.db);
            IPagedList<MessageRecordView> records = mrs
                .GetEmployee(MessageRecordCatetory.Alert)
                .ToPagedList<MessageRecordView>(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View(records);
        }

        public ActionResult Search([Bind(Include = "StaffNr,StaffNrAct")] MessageRecordSearchModel q)
        {
            string allOrUnread = Request.QueryString["allOrUnread"];
            ViewBag.allOrUnread = allOrUnread;
          

            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IMessageRecordService mrs = new MessageRecordService(Settings.Default.db);
            IPagedList<MessageRecordView> records = mrs
                .GetByCateAndAllOrUnread(MessageRecordCatetory.Alert, (allOrUnread == "all" ? true : false),q)
                .ToPagedList<MessageRecordView>(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", records);
        }


        [HttpGet] 
        public ActionResult UnReadCount()
        {
            return Json(new MessageRecordService(Settings.Default.db).CountUnRead(),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Read(int id)
        {
            ResultMessage msg = new ResultMessage();
            try {
                msg.Success = new MessageRecordService(Settings.Default.db).Read(id);
            }
            catch(Exception ex)
            {
                msg.Content = ex.Message;
            }
            return Json(msg);
        }

        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IMessageRecordService at = new MessageRecordService(Settings.Default.db);

            var MessageRecord = at.GetAllTableName();

            if (MessageRecord != null)
            {
                //获取当前记录的属性
                foreach (var property in MessageRecord[0].GetType().GetProperties())
                {
                    select.Add(new SelectListItem { Text = property.Name, Value = property.Name });
                }
            }

            ViewData["getAllTableNameList"] = select;
        }

        private void SetSearchConditions(bool? type, bool allowBlank = false)
        {
            var item = EnumHelper.GetList(typeof(SearchConditions));

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var it in item)
            {
                if (type.HasValue && type.ToString().Equals(it.Value))
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
                }
            }
            ViewData["searchConditionsList"] = select;
        }
    }
}