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
    public class AttendanceRecordDetailController : Controller
    {
        // GET: AttendanceRecordDetail
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);
            AttendanceRecordDetailSearchModel q = new AttendanceRecordDetailSearchModel();
            IAttendanceRecordDetailService ss = new AttendanceRecordDetailService(Settings.Default.db);

            IPagedList<AttendanceRecordDetailView> records = ss.SearchDetailView(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;
            SetShiftList();
            return View(records);
        }

        public ActionResult Search([Bind(Include = "CompanyId,DepartmentId,StaffNr,RecordAtFrom,RecordAtEnd")] AttendanceRecordDetailSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IAttendanceRecordDetailService ss = new AttendanceRecordDetailService(Settings.Default.db);

            IPagedList<AttendanceRecordDetailView> records = ss.SearchDetailView(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;
            SetShiftList();
            return View("Index", records);
        }

        public ActionResult Import()
        {
            var ff = Request.Files[0];
            string fileName = BlueHrWeb.Helpers.FileHelper.SaveAsTmp(ff);
            AttendanceRecordDetailExcelHelper helper = new AttendanceRecordDetailExcelHelper(Settings.Default.db, fileName);
            ImportMessage msg = helper.Import();

            //添加"text/html",防止IE 自动下载json 格式返回的数据
            return Json(msg, "text/html");
        }
        [HttpPost]
        public ActionResult Calculate()
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                TaskDispatcher td=new TaskDispatcher(Settings.Default.queue);
                DateTime calculateAt = DateTime.Parse(Request.Form.Get("DateTime"));
               List< string> shiftCodes = null;
                 
                if (!string.IsNullOrEmpty(Request.Form.Get("ShiftCode")))
                {
                    shiftCodes = new List<string>() { Request.Form.Get("ShiftCode") };
                };
                td.SendCalculateAttMessage(calculateAt, shiftCodes);
                
                msg.Success = true;
                msg.Content = "计算任务新建成功，请到系统任务页面查看结果!";
            }
            catch (Exception e)
            {
                msg.Content = e.Message;
            }

            return Json(msg);
        }


        private void SetShiftList()
        {
            IShiftService ss = new ShiftService(Settings.Default.db);


            List<Shift> jt = ss.All();

            List<SelectListItem> select = new List<SelectListItem>();

            select.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var it in jt)
            {
                select.Add(new SelectListItem { Text = it.name, Value = it.code.ToString(), Selected = false });
            }
            ViewData["shiftList"] = select;
        }

    }
}