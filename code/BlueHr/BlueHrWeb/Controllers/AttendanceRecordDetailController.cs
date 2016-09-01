using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.Helpers;
using BlueHrWeb.Properties;
using MvcPaging;
using BlueHrLib.Helper.Excel;
using BlueHrLib.Data.Message;

namespace BlueHrWeb.Controllers
{
    public class AttendanceRecordDetailController : Controller
    {
        // GET: AttendanceRecordDetail
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);
            AttendanceRecordDetailSearchModel q = new AttendanceRecordDetailSearchModel();
            IAttendanceRecordService ss = new AttendanceRecordService(Settings.Default.db);

            IPagedList<AttendanceRecordDetailView> records = ss.SearchDetailView(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View(records);
        }

        public ActionResult Search([Bind(Include = "StaffNr")] AttendanceRecordDetailSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IAttendanceRecordService ss = new AttendanceRecordService(Settings.Default.db);

            IPagedList<AttendanceRecordDetailView> records = ss.SearchDetailView(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", records);
        }

        public ActionResult Import()
        {
            var ff = Request.Files[0];
            string fileName = FileHelper.SaveAsTmp(ff);
            AttendanceRecordDetailExcelHelper helper = new AttendanceRecordDetailExcelHelper(Settings.Default.db, fileName);
            ImportMessage msg = helper.Import();

            return Json(msg);
        }
    }
}