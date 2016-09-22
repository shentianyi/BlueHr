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
using BlueHrWeb.CustomAttributes;

namespace BlueHrWeb.Controllers
{
    public class AttendanceRecordDetailController : Controller
    {
        // GET: AttendanceRecordDetail
        [UserAuthorize]
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

        [AdminAuthorize]
        // GET: AttendanceRecordDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AttendanceRecordDetail/Create
        [HttpPost]
        public JsonResult Create([Bind(Include = "staffNr,recordAt,device")] AttendanceRecordDetail ard)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(ard);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IAttendanceRecordDetailService ards = new AttendanceRecordDetailService(Settings.Default.db);
                    bool isSucceed = ards.Create(ard);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "可能员工打卡时间已经存在";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [AdminAuthorize]
        // GET: AttendanceRecordDetail/Edit/5
        public ActionResult Edit(int id)
        {
            IAttendanceRecordDetailService ards = new AttendanceRecordDetailService(Settings.Default.db);

            AttendanceRecordDetail ard = ards.FindById(id);
            return View(ard);
        }

        // POST: AttendanceRecordDetail/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,staffNr,recordAt,device")] AttendanceRecordDetail attendanceRecordDetail)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(attendanceRecordDetail);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IAttendanceRecordDetailService ards = new AttendanceRecordDetailService(Settings.Default.db);
                    bool isSucceed = ards.Update(attendanceRecordDetail);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "可能员工打卡时间已经存在";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AdminAuthorize]
        // GET: AttendanceRecordDetail/Delete/5
        public ActionResult Delete(int id)
        {
            IAttendanceRecordDetailService ards = new AttendanceRecordDetailService(Settings.Default.db);

            AttendanceRecordDetail ard = ards.FindById(id);
            return View(ard);
        }

        // POST: AttendanceRecordDetail/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {

                IAttendanceRecordDetailService ards = new AttendanceRecordDetailService(Settings.Default.db);
                bool isSucceed = ards.DeleteById(id);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "" : "删除失败";

                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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
                td.SendCalculateAttMessage(calculateAt.Date.AddDays(-2), shiftCodes);
                
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

        private ResultMessage DoValidation(AttendanceRecordDetail model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.staffNr))
            {
                msg.Success = false;
                msg.Content = "员工号不能为空";

                return msg;
            }
            
            if (string.IsNullOrWhiteSpace(model.recordAt.ToString()))
            {
                msg.Success = false;
                msg.Content = "日期不能为空";

                return msg;
            }

            if (string.IsNullOrEmpty(model.device))
            {
                msg.Success = false;
                msg.Content = "设备号不能为空";

                return msg;
            }

            IStaffService ss = new StaffService(Settings.Default.db);
            bool staffResult = ss.IsStaffExist(model.staffNr);

            if (!staffResult)
            {
                msg.Success = false;
                msg.Content = "员工不存在,请先创建员工";

                return msg;
            }

            return new ResultMessage() { Success = true, Content = "" };
        }
    }
}