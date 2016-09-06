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
            IAttendanceRecordCalService ss = new AttendanceRecordCalService(Settings.Default.db);

            IPagedList<AttendanceRecordCalView> records = ss.SearchCalView(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View(records);
        }

        public ActionResult Search([Bind(Include = "CompanyId,DepartmentId,StaffNr,IsException,IsExceptionHandled,AttendanceDateFrom,AttendanceDateEnd")] AttendanceRecordCalSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IAttendanceRecordCalService ss = new AttendanceRecordCalService(Settings.Default.db);
            IPagedList<AttendanceRecordCalView> records = ss.SearchCalView(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;
            return View("Index", records);
        }

        // GET: AttendanceRecordDetail/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            IAttendanceRecordCalService ss = new AttendanceRecordCalService(Settings.Default.db);
            AttendanceRecordCalView record = ss.FindViewById(id);

            if (record != null)
            {
                List<AttendanceRecordDetailView> records = new List<AttendanceRecordDetailView>();
                IAttendanceRecordDetailService s = new AttendanceRecordDetailService(Settings.Default.db);
                records = s.GetDetailsViewByStaffAndDate(record.staffNr, record.attendanceDate.Add(new TimeSpan(23, 59, 59)));
                ViewData["attendRecords"] = records;
            }
            return View(record);
        }

        // POST: AttendanceRecordDetail/UpdateActHour/5
        /// <summary>
        /// 修改实际工时
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateActHour(int id)
        {
            ResultMessage msg = new ResultMessage();
            double actHour = 0;
            if (!double.TryParse(Request.Form["actWorkingHourRound"], out actHour))
            {
                msg.Content = "实际工时必须是数字";
                return Json(msg);
            }
            bool handled = false;
            if (!bool.TryParse(Request.Form["isExceptionHandled"], out handled))
            {
                msg.Content = "是否处理值非法，请刷新页面重新填写";
                return Json(msg);
            }


            IAttendanceRecordCalService ss = new AttendanceRecordCalService(Settings.Default.db);
            msg = ss.UpdateActHourById(id, actHour, handled,Request.Form["remark"]);
            return Json(msg);
        }

        public ActionResult ExceptionList()
        {
            IAttendanceRecordCalService ss = new AttendanceRecordCalService(Settings.Default.db);
            DateTime startDate = DateTime.Now.Date.AddDays(-20);
            DateTime endDate = DateTime.Now.Date;
            if (!string.IsNullOrEmpty(Request.QueryString["startDate"]))
            {
                DateTime.TryParse(Request.QueryString["startDate"], out startDate);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["endDate"]))
            {
                DateTime.TryParse(Request.QueryString["endDate"], out endDate);
            }


            List<AttendanceRecordCalExceptionView> records = ss.GetCalExceptionHandleList(startDate, endDate);
            for (var d = startDate; d <= endDate; d = d.AddDays(1))
            {
                if (records.Where(s => s.attendanceDate.Equals(d)).FirstOrDefault() == null)
                {
                    records.Add(new AttendanceRecordCalExceptionView()
                    {
                        attendanceDate = d,
                        isExceptionHandledCount = 0
                    });
                }
            }
            records = records.OrderByDescending(s => s.attendanceDate).ToList();

            ViewBag.startDate = startDate.ToString("yyyy-MM-dd");
            ViewBag.endDate = endDate.ToString("yyyy-MM-dd");

            return View(records);
        }

    }
}