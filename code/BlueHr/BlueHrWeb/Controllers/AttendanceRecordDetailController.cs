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
            IAttendanceRecordService ss = new AttendanceRecordService(Settings.Default.db);

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

            IAttendanceRecordService ss = new AttendanceRecordService(Settings.Default.db);

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

            return Json(msg);
        }
        [HttpPost]
        public ActionResult Calculate()
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                CalAtt calAttParam = new CalAtt()
                {
                    AttDateTime = DateTime.Parse(Request.Form.Get("DateTime")),

                };
                if (!string.IsNullOrEmpty(Request.Form.Get("ShiftCode")))
                {
                    calAttParam.ShiftCodes = new List<string>()
                {
                    Request.Form.Get("ShiftCode")
                };
                }
                TaskSetting task = new TaskSetting()
                {
                    TaskCreateAt = DateTime.Now,
                    TaskType = TaskType.CalAtt,
                    JsonParameter = JSONHelper.stringify(calAttParam)

                };

                //ICalculateService cs = new CalculateService(Settings.Default.db);
                //cs.Start(Settings.Default.mrpQueue, setting);
                new TaskDispatcher(Settings.Default.queue).SendMQMessage(task);
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