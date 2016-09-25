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
using BlueHrLib.Data.Enum;

namespace BlueHrWeb.Controllers
{
    public class AttendanceRecordCalController : Controller
    {
        // GET: AttendanceRecordCal
        [UserAuthorize]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);
            AttendanceRecordCalSearchModel q = new AttendanceRecordCalSearchModel();
            IAttendanceRecordCalService ss = new AttendanceRecordCalService(Settings.Default.db);

            IPagedList<AttendanceRecordCalView> records = ss.SearchCalView(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SetExtraWorkTypeList(null);

            return View(records);
        }
        [UserAuthorize]
        public ActionResult Search([Bind(Include = "CompanyId,DepartmentId,StaffNr,IsException,IsExceptionHandled,AttendanceDateFrom,AttendanceDateEnd")] AttendanceRecordCalSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IAttendanceRecordCalService ss = new AttendanceRecordCalService(Settings.Default.db);
            IPagedList<AttendanceRecordCalView> records = ss.SearchCalView(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SetExtraWorkTypeList(null);

            return View("Index", records);
        }


        [AdminAuthorize]
        // GET: AttendanceRecordCal/Create
        public ActionResult Create()
        {
            SetExtraWorkTypeList(null);

            return View();
        }

        // POST: AttendanceRecordCal/Create
        [HttpPost]
        public JsonResult Create([Bind(Include = "staffNr,attendanceDate, actWorkingHour, remark, actExtraWorkingHour, extraWorkType")] AttendanceRecordCal arc)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(arc);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IAttendanceRecordCalService arcs = new AttendanceRecordCalService(Settings.Default.db);
                    bool isSucceed = arcs.Create(arc);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "创建失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        // GET: AttendanceRecordDetail/Edit/5
        [HttpGet]
        [UserAuthorize]
        public ActionResult Edit(int id)
        {
            IAttendanceRecordCalService ss = new AttendanceRecordCalService(Settings.Default.db);
            AttendanceRecordCalView record = ss.FindViewById(id);

            if (record != null)
            {
                SetExtraWorkTypeList(record.extraworkType);
                AbsenceRecordView absenceRecord = new AbsenceRecordService(Settings.Default.db).FindViewByStaffNrAndDate(record.staffNr, record.attendanceDate);
                ExtraWorkRecordView extraWorkRecord = new ExtraWorkRecordService(Settings.Default.db).FindViewByStaffNrAndDate(record.staffNr, record.attendanceDate);

                List<AttendanceRecordDetailView> records = new List<AttendanceRecordDetailView>();
                IAttendanceRecordDetailService s = new AttendanceRecordDetailService(Settings.Default.db);
                records = s.GetDetailsViewByStaffAndDateWithExtrawork(record.staffNr, record.attendanceDate);
                ViewData["attendRecords"] = records;
                ViewData["absenceRecord"] = absenceRecord;
                ViewData["extraWorkRecord"] = extraWorkRecord;
            }else
            {
                SetExtraWorkTypeList(null);
            }

            return View(record);
        }

        [AdminAuthorize]
        // GET: AttendanceRecordCal/Delete/5
        public ActionResult Delete(int id)
        {
            IAttendanceRecordCalService arcs = new AttendanceRecordCalService(Settings.Default.db);

            AttendanceRecordCal record = arcs.FindById(id);

            SetExtraWorkTypeList(record.extraworkType);




            if (record != null)
            { 
                AbsenceRecordView absenceRecord = new AbsenceRecordService(Settings.Default.db).FindViewByStaffNrAndDate(record.staffNr, record.attendanceDate);
                ExtraWorkRecordView extraWorkRecord = new ExtraWorkRecordService(Settings.Default.db).FindViewByStaffNrAndDate(record.staffNr, record.attendanceDate);

                List<AttendanceRecordDetailView> records = new List<AttendanceRecordDetailView>();
                IAttendanceRecordDetailService s = new AttendanceRecordDetailService(Settings.Default.db);
                records = s.GetDetailsViewByStaffAndDateWithExtrawork(record.staffNr, record.attendanceDate);
                ViewData["attendRecords"] = records;
                ViewData["absenceRecord"] = absenceRecord;
                ViewData["extraWorkRecord"] = extraWorkRecord;
            }
            else
            {
                SetExtraWorkTypeList(null);
            }


            return View(record);
        }

        // POST: AttendanceRecordCal/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {

                IAttendanceRecordCalService arcs = new AttendanceRecordCalService(Settings.Default.db);
                bool isSucceed = arcs.DeleteById(id);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "" : "删除失败";

                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        // POST: AttendanceRecordCal/UpdateActHour/5
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
            double actExtraHour = 0;
            if (!double.TryParse(Request.Form["actWorkingHourRound"], out actHour))
            {
                msg.Content = "工作日工时必须是数字";
                return Json(msg);
            }

            // 加班时间可以为空 
            // 如果加班为空， 直接写入0
            // 如果不为空， 则所填必须为数字
            if (!string.IsNullOrWhiteSpace(Request.Form["actExtraWorkingHourRound"]) && !double.TryParse(Request.Form["actExtraWorkingHourRound"], out actExtraHour))
            {
                msg.Content = "加班工时必须是数字";
                return Json(msg);
            }

            bool handled = false;
            if (!bool.TryParse(Request.Form["isExceptionHandled"], out handled))
            {
                msg.Content = "是否处理值非法，请刷新页面重新填写";
                return Json(msg);
            }

            IAttendanceRecordCalService ss = new AttendanceRecordCalService(Settings.Default.db);
            AttendanceRecordCal record = ss.FindById(id);

            string oldHour = record.actWorkingHour.ToString();
            string oldActHour = record.actExtraWorkingHour.ToString();

            //判断加班类型是否为空， 如果为空， 就返回null
            // 如果做判断，直接使用int.Parse转，会报异常

            int? ExtraWorkType = null;

            if (!string.IsNullOrWhiteSpace(Request.Form["extraWorkType"]))
            {
                ExtraWorkType = int.Parse(Request.Form["extraWorkType"]);
            }

            msg = ss.UpdateActHourById(id, actHour,actExtraHour, handled,Request.Form["remark"], ExtraWorkType);

            string newHour = actHour.ToString();
            string newActHour = actExtraHour.ToString();
            // 创建调整考勤记录##User##
            try
            {
                IMessageRecordService mrs = new MessageRecordService(Settings.Default.db);
                mrs.CreateStaffUpdateAttHourMessage(record.staffNr, (Session["user"] as User).id, oldHour, newHour, oldActHour, newActHour);
            }
            catch { }

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

        [HttpGet]
       public ActionResult ExportReport([Bind(Include = "CompanyId,DepartmentId")] StaffSearchModel q)
        {
            var msg = new ReportMessage();
            if (Request.QueryString["type"] == "100")
            {
                msg = new ReportExcelHelper(Settings.Default.db).ExportExtraSumReport(
                    DateTime.Parse(Request.QueryString["startDate"]),
                    DateTime.Parse(Request.QueryString["endDate"]), q);
            }
            else
            {

                msg = new ReportExcelHelper(Settings.Default.db).ExportHandledAttendDetail(
                    DateTime.Parse(Request.QueryString["startDate"]),
                    DateTime.Parse(Request.QueryString["endDate"]), q);
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 加班类型可以为空， 有可能不加班
        /// </summary>
        /// <param name="type">加班类型字段</param>
        /// <param name="allowBlank">可以为空</param>
        private void SetExtraWorkTypeList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(SystemExtraType));

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
            ViewData["extraWorkTypeList"] = select;
        }

        private ResultMessage DoValidation(AttendanceRecordCal model)
        {
            ResultMessage msg = new ResultMessage();
            double actHour = 0;
            double actExtraHour = 0;

            if (string.IsNullOrEmpty(model.staffNr))
            {
                msg.Success = false;
                msg.Content = "员工号不能为空";

                return msg;
            }

            if (string.IsNullOrWhiteSpace(model.attendanceDate.ToString()))
            {
                msg.Success = false;
                msg.Content = "日期不能为空";

                return msg;
            }

            if (!double.TryParse(model.actWorkingHour.ToString(), out actHour))
            {
                msg.Success = false;
                msg.Content = "工作时长必须是数字";

                return msg;
            }

            //可以为空
            //不为空的时候检查是否是数字

            if (!string.IsNullOrWhiteSpace(model.actExtraWorkingHour.ToString()) && !double.TryParse(model.actExtraWorkingHour.ToString(), out actExtraHour))
            {
                msg.Success = false;
                msg.Content = "加班时长必须是数字";

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