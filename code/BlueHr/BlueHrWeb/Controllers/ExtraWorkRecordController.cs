using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Helper;
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
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.Excel;

namespace BlueHrWeb.Controllers
{
    public class ExtraWorkRecordController : Controller
    {
        // GET: ExtraWorkRecord
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            SetDropDownList(null);

            int pageIndex = PagingHelper.GetPageIndex(page);

            ExtraWorkRecordSearchModel q = new ExtraWorkRecordSearchModel();

            IExtraWorkRecordService ss = new ExtraWorkRecordService(Settings.Default.db);

            IPagedList<ExtraWorkRecord> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            ExtraWorkRecordInfoModel info = ss.GetExtraWorkRecordInfo(q);
            ViewBag.Info = info;

            return View(models);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "staffNr,extraWorkTypeId,durStart,durEnd")] ExtraWorkRecordSearchModel q)
        {
            SetDropDownList(null);

            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IExtraWorkRecordService ss = new ExtraWorkRecordService(Settings.Default.db);

            IPagedList<ExtraWorkRecord> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", models);
        }

        // GET: ExtraWorkRecord/Details/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExtraWorkRecord/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: ExtraWorkRecord/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public ActionResult Create([Bind(Include = "extraWorkTypeId,staffNr,otTime,startHour,endHour,duration,durationType,otReason")] ExtraWorkRecord model)
        {
            ResultMessage msg = new ResultMessage();

            try
            { 
                model.durationType = (int)DurationType.Hour;
 
                msg = DoValidation(model);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IExtraWorkRecordService cs = new ExtraWorkRecordService(Settings.Default.db);
                    bool isSucceed = cs.Create(model);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "添加失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                } 
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        // GET: ExtraWorkRecord/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IExtraWorkRecordService cs = new ExtraWorkRecordService(Settings.Default.db);

            ExtraWorkRecord jt = cs.FindById(id);
            SetDropDownList(jt);
            return View(jt);
        }

        // POST: ExtraWorkRecord/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, extraWorkTypeId,staffNr,startHour,endHour,otTime, duration,durationType,otReason")] ExtraWorkRecord model)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                model.durationType = (int)DurationType.Hour;
                   msg = DoValidation(model); 

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IExtraWorkRecordService cs = new ExtraWorkRecordService(Settings.Default.db);
                    bool isSucceed = cs.Update(model);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "更新失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: ExtraWorkRecord/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IExtraWorkRecordService cs = new ExtraWorkRecordService(Settings.Default.db);

            ExtraWorkRecord cp = cs.FindById(id);
            SetDropDownList(cp);
            return View(cp);
        }

        // POST: ExtraWorkRecord/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                //审批后不可删除
                IExtraWorkRecordService shfSi = new ExtraWorkRecordService(Settings.Default.db);
                ExtraWorkRecord shf = shfSi.FindById(id);

                if (null != shf && shf.ExtraWorkRecordApprovals.Count() > 0)
                {
                    msg.Success = false;
                    msg.Content = "加班审批后不可删除!";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IExtraWorkRecordService cs = new ExtraWorkRecordService(Settings.Default.db);
                    bool isSucceed = cs.DeleteById(id);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "删除失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private void SetDropDownList(ExtraWorkRecord model)
        {
            if (model != null)
            {
                SetExtraWorkTypeList(model.extraWorkTypeId);
                SetDurationTypeCodeList(model.durationType);
            }
            else
            {
                SetExtraWorkTypeList(null);
                SetDurationTypeCodeList(null);
            }
        }

        private void SetExtraWorkTypeList(int? type, bool allowBlank = true)
        {
            IExtraWorkTypeService cs = new ExtraWorkTypeService(Settings.Default.db);


            List<ExtraWorkType> certType = cs.All();

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var certt in certType)
            {
                if (type.HasValue && type.ToString().Equals(certt.id))
                {
                    select.Add(new SelectListItem { Text = certt.name, Value = certt.id.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = certt.name, Value = certt.id.ToString(), Selected = false });
                }
            }
            ViewData["extraWorkTypeList"] = select;
        }

        private void SetDurationTypeCodeList(int? model, bool allowBlank = false)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(DurationType));

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var it in item)
            {
                if (model.HasValue && model.ToString().Equals(it.Value))
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
                }
            }
            ViewData["durationTypeList"] = select;
        }

        public ActionResult Import()
        {
            var ff = Request.Files[0];
            string fileName = BlueHrWeb.Helpers.FileHelper.SaveAsTmp(ff);
            ExtraWorkTypeExcelHelper helper = new ExtraWorkTypeExcelHelper(Settings.Default.db, fileName);
            ImportMessage msg = helper.Import();

            //添加"text/html",防止IE 自动下载json 格式返回的数据
            return Json(msg, "text/html");
        }

        [HttpPost]
        //•	员工号（输入，不可空）
        //•	缺勤类别（选择，不可空）
        //•	缺勤原因（输入，可空），
        //•	缺勤的小时或天长。（输入，不可空）
        //•	时间单位（选择，不可空，选项为： 小时/天，默认为小时） 
        public ResultMessage DoValidation(ExtraWorkRecord model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.staffNr))
            {
                msg.Success = false;
                msg.Content = "员工号不能为空";

                return msg;
            }
            else
            {
                IStaffService ss = new StaffService(Settings.Default.db);
                if (ss.FindByNr(model.staffNr) == null)
                {
                    msg.Success = false;
                    msg.Content = "员工号不存在";

                    return msg;
                }
            }

            if (model.extraWorkTypeId <= 0)
            {
                msg.Success = false;
                msg.Content = "加班类别不能为空";

                return msg;
            }

            if (model.otTime== null)
            {
                msg.Success = false;
                msg.Content = "加班时间不能为空，或格式必须正确";

                return msg;
            }

            if (model.duration <= 0)
            {
                msg.Success = false;
                msg.Content = "加班时长不能为空";

                return msg;
            }

            //IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);
            //List<AbsenceRecrod> abs = cs.GetAll();

            //if (model.id <= 0)
            //{
            //    bool isRecordExists = abs.Where(p => p.staffNr == model.staffNr || p.absenceTypeId == model.absenceTypeId
            //    || p.remark == model.remark || p.duration == model.duration).ToList().Count() > 0;

            //    if (isRecordExists)
            //    {
            //        msg.Success = false;
            //        msg.Content = "数据已经存在!";

            //        return msg;
            //    }
            //}
            //else
            //{
            //    bool isRecordExists = abs.Where(p => (p.staffNr == model.staffNr || p.absenceTypeId == model.absenceTypeId
            //    || p.remark == model.remark || p.duration == model.duration) && p.id != model.id).ToList().Count() > 0;

            //    if (isRecordExists)
            //    {
            //        msg.Success = false;
            //        msg.Content = "数据已经存在!";

            //        return msg;
            //    }
            //}

            return new ResultMessage() { Success = true, Content = "" };
        }

        [HttpPost]
        public JsonResult ApprovalExtraWorkRecord(string extralRecordId, string approvalStatus, string approvalRemarks)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                //check user

                if (Session["user"] == null)
                {
                    msg.Success = false;
                    msg.Content = "用户未登录，请登录后重试！";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }

                ExtraWorkRecordApproval extralApproval = new ExtraWorkRecordApproval();
                extralApproval.extraWorkId = !string.IsNullOrEmpty(extralRecordId) ? int.Parse(extralRecordId) : -1;
                extralApproval.approvalStatus = approvalStatus;
                extralApproval.approvalTime = DateTime.Now;
                extralApproval.remarks = approvalRemarks;

                if (Session["user"] != null)
                {
                    User user = Session["user"] as User;
                    extralApproval.userId = user.id;
                }

                IExtraWorkRecordService cs = new ExtraWorkRecordService(Settings.Default.db);
                bool isSucceed = cs.ApprovalTheRecord(extralApproval);

                msg.Success = isSucceed;
                msg.Content = "审批成功！";

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg.Success = false;
                msg.Content = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
