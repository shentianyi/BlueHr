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
using BlueHrLib.MQTask;
using BlueHrLib.MQTask.Parameter;
using BlueHrLib.Helper;

namespace BlueHrWeb.Controllers
{
    public class AttendanceRecordCalController : Controller
    {
        // GET: AttendanceRecordDetail
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);
            AttendanceRecordCalSearchModel q = new AttendanceRecordCalSearchModel();
            IAttendanceRecordService ss = new AttendanceRecordService(Settings.Default.db);

            IPagedList<AttendanceRecordCalView> records = ss.SearchCalView(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View(records);
        }

        public ActionResult Search([Bind(Include = "CompanyId,DepartmentId,StaffNr,IsException,IsExceptionHandled,AttendanceDateFrom,AttendanceDateEnd")] AttendanceRecordCalSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IAttendanceRecordService ss = new AttendanceRecordService(Settings.Default.db);
            IPagedList<AttendanceRecordCalView> records = ss.SearchCalView(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;
            return View("Index", records);
        }
         
    }
}