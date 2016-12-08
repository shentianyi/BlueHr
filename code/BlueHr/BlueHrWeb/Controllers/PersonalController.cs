using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.Helpers;
using BlueHrWeb.Properties;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueHrWeb.CustomAttributes;
using BlueHrLib.Helper;
using BlueHrLib.Data.Enum;

namespace BlueHrWeb.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Application
        // 我的申请 
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Application()
        {
            //可以使用ViewData进行传值
            User user = System.Web.HttpContext.Current.Session["user"] as User;

            //加班申请信息
            IExtraWorkRecordService rwrs = new ExtraWorkRecordService(Settings.Default.db);
            ExtraWorkRecordSearchModel ewrsSearchModel = new ExtraWorkRecordSearchModel();
            ewrsSearchModel.lgUser = user;
            List<ExtraWorkRecordView> extraWorkRecord = rwrs.ExtraWorkViewSearch(ewrsSearchModel).Where(c=>c.userId.Equals(user.id)).Where(c=>c.approvalStatus==null).ToList();
            List<Dictionary<string, string>> AllExtraWork = new List<Dictionary<string, string>>();

            foreach (var extraWork in extraWorkRecord)
            {
                Dictionary<string, string> extraWorks = new Dictionary<string, string>();

                extraWorks.Add("ID", extraWork.id.ToString());
                extraWorks.Add("StaffNr", extraWork.staffNr);
                extraWorks.Add("StaffNrName", extraWork.staffName);
                extraWorks.Add("ExtraWorkType", extraWork.name);
                extraWorks.Add("Time", extraWork.otTimeStr);
                extraWorks.Add("StartHour", extraWork.startHour.ToString());
                extraWorks.Add("EndHour", extraWork.endHour.ToString());
                extraWorks.Add("Duration", extraWork.duration + " (h)");
                //extraWorks.Add("DurationType", extraWork.durationType==100?"天":"小时");
                extraWorks.Add("Reason", extraWork.otReason);
                extraWorks.Add("ApprovalStatus", extraWork.approvalStatus == null ? "审批中" : extraWork.approvalStatus);
                extraWorks.Add("ApprovalRemarks", extraWork.remarks);

                AllExtraWork.Add(extraWorks);
            }

            ViewData["ExtraWork"] = AllExtraWork;

            //离职申请信息
            IResignRecordService rrs = new ResignRecordService(Settings.Default.db);
            ResignRecordSearchModel rrsSearchModel = new ResignRecordSearchModel();
            ewrsSearchModel.lgUser = user;
            List<ResignRecord> resignRecords = rrs.Search(rrsSearchModel).Where(c => c.userId.Equals(user.id)).Where(c => c.approvalStatus == null).ToList();
            List<Dictionary<string, string>> AllResignRecords = new List<Dictionary<string, string>>();

            foreach (var resignRecord in resignRecords)
            {
                Dictionary<string, string> resigns = new Dictionary<string, string>();

                IResignTypeService rts = new ResignTypeService(Settings.Default.db);
                ResignType resignType = rts.FindById(resignRecord.resignTypeId);

                resigns.Add("ID", resignRecord.id.ToString());
                resigns.Add("StaffNr", resignRecord.staffNr);
                resigns.Add("ResignAt", resignRecord.resignAt.ToString());
                resigns.Add("ResignReason", resignRecord.resignReason);
                resigns.Add("CreateAt", resignRecord.createdAt.ToString());
                resigns.Add("ApprovalStatus", resignRecord.approvalStatus == null ? "审批中" : resignRecord.approvalStatus);
                resigns.Add("ResignChecker", resignRecord.resignChecker);
                resigns.Add("ApprovalRemark", resignRecord.approvalRemark);

                AllResignRecords.Add(resigns);
            }

            ViewData["Resign"] = AllResignRecords;

            return View();
        }

        // GET: Approval 
        // 我的审核
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Approval()
        {
            return View();
        }

        // GET: Schedule 
        // 日程安排
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Schedule()
        {
            return View();
        }

        [UserAuthorize]
        [RoleAndDataAuthorization]
        [HttpPost]
        public JsonResult AddSchedule(string category, string subject, string significance, string context, DateTime startTime, DateTime endTime) 
        {
            IPersonScheduleService pss = new PersonScheduleService(Settings.Default.db);
            PersonSchedule personSchedule = new PersonSchedule();
            personSchedule.category = category;
            personSchedule.subject = subject;
            personSchedule.significance = significance;
            personSchedule.context = context;
            personSchedule.startTime = startTime;
            personSchedule.endTime = endTime;
            personSchedule.createdAt = DateTime.Now;

            PersonSchedule ps = pss.Create(personSchedule);

            Dictionary<string, string> Result = new Dictionary<string, string>();
            Result.Add("id", ps.id.ToString());
            Result.Add("category", ps.category);
            Result.Add("subject", ps.subject);
            Result.Add("significance", ps.significance);
            Result.Add("context", ps.context);
            Result.Add("startTime", ps.startTime.ToString());
            Result.Add("endTime", ps.endTime.ToString());

            return Json(Result, JsonRequestBehavior.DenyGet);
        }
        [UserAuthorize]
        [RoleAndDataAuthorization]
        [HttpPost]
        public JsonResult DeleteSchedule (int id)
        {
            IPersonScheduleService pss = new PersonScheduleService(Settings.Default.db);
            bool Result = false;

            try
            {
                Result = pss.DeleteById(id);

                return Json(Result, JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(Result, JsonRequestBehavior.DenyGet);
            }
        }

        // GET: Note 
        // 便笺本
        //[UserAuthorize]
        //[RoleAndDataAuthorizationAttribute]
        //public ActionResult Note()
        //{
        //    return View();
        //}

        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        [HttpGet]
        public JsonResult AllPersonSchedule()
        {
            List<Dictionary<string, string>> Result = new List<Dictionary<string, string>>();

            IPersonScheduleService pss = new PersonScheduleService(Settings.Default.db);
            PersonScheduleSearchModel q = new PersonScheduleSearchModel();

            List<PersonSchedule> personSchedules = pss.Search(q).ToList();

            foreach(var personSchedule in personSchedules)
            {
                Dictionary<string, string> personScheduleDic = new Dictionary<string, string>();

                personScheduleDic.Add("id", personSchedule.id.ToString());
                personScheduleDic.Add("start", string.Format("{0:s}",personSchedule.startTime));
                personScheduleDic.Add("end", string.Format("{0:s}",personSchedule.endTime));
                personScheduleDic.Add("category", personSchedule.category);
                personScheduleDic.Add("title", personSchedule.context);
                personScheduleDic.Add("significance", personSchedule.significance);
                personScheduleDic.Add("subject", personSchedule.subject);
                personScheduleDic.Add("createdAt", string.Format("{0:s}",personSchedule.createdAt));
                personScheduleDic.Add("isDeleted", personSchedule.isDeleted.ToString());

                Result.Add(personScheduleDic);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

    }
}
