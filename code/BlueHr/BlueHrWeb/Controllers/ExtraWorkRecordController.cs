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

            //在员工管理-员工列表、排班管理-排班管理、缺勤管理、加班管理的列表中，用户如果有权限查看列表，那么只可以查看他所管理部门中的所有员工(员工中已有部门、公司)
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            q.lgUser = user;

            IExtraWorkRecordService ss = new ExtraWorkRecordService(Settings.Default.db);

            IPagedList<ExtraWorkRecordView> models = ss.ExtraWorkViewSearch(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            ExtraWorkRecordInfoModel info = ss.GetExtraWorkRecordInfo(q);
            ViewBag.Info = info;

            return View(models);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "staffNr,extraWorkTypeId,durStart,durEnd")] ExtraWorkRecordSearchModel q)
        {
            //在员工管理-员工列表、排班管理-排班管理、缺勤管理、加班管理的列表中，用户如果有权限查看列表，那么只可以查看他所管理部门中的所有员工(员工中已有部门、公司)
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            q.lgUser = user;

            SetDropDownList(null);

            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IExtraWorkRecordService ss = new ExtraWorkRecordService(Settings.Default.db);

            IPagedList<ExtraWorkRecordView> models = ss.ExtraWorkViewSearch(q).ToPagedList(pageIndex, Settings.Default.pageSize);

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
        public JsonResult Create([Bind(Include = "extraWorkTypeId,staffNr,otTime,startHour,endHour,duration,durationType,otReason")] ExtraWorkRecord model)
        {
            ResultMessage msg = new ResultMessage();

            User user = System.Web.HttpContext.Current.Session["user"] as User;

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
                    model.userId = user.id;
                    bool isSucceed = cs.Create(model);

                    msg.Success = isSucceed;

                    msg.Content = isSucceed ? "创建成功" : "创建失败， 请检查";

                    return Json(msg, JsonRequestBehavior.DenyGet);
                } 
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.DenyGet);
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
                    msg.Content = isSucceed ? "更新成功" : "更新失败，请检查";

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

        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult AdvancedSearch(ExtraWorkRecordSearchModel q)
        {
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            q.lgUser = user;
            ViewBag.Query = q;

            IExtraWorkRecordService ewrs = new ExtraWorkRecordService(Settings.Default.db);
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IPagedList<ExtraWorkRecordView> extraWorkRecords = null;
            IQueryable<ExtraWorkRecordView> extraWorkRecordtemp = null;
            IQueryable<ExtraWorkRecordView> extraWorkRecordtemp1 = null;
            List<ExtraWorkRecordView> Result = new List<ExtraWorkRecordView>();
            if (!string.IsNullOrEmpty(Request.Form["allTableName"]))
            {
                if (!string.IsNullOrEmpty(Request.Form["searchConditions"]))
                {
                    if (!string.IsNullOrEmpty(Request.Form.Get("searchValueFirst")))
                    {
                        string AllTableName = Request.Form["allTableName"].ToString();
                        string[] AllTableNameArray = AllTableName.Split(',');
                        string SearchConditions = Request.Form["searchConditions"];
                        string[] SearchConditionsArray = SearchConditions.Split(',');
                        string searchValueFirst = Request.Form["searchValueFirst"];
                        string[] searchValueFirstArray = searchValueFirst.Split(',');

                        try
                        {
                            extraWorkRecordtemp1 = ewrs.AdvancedSearch(AllTableNameArray[0], SearchConditionsArray[0], searchValueFirstArray[0])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                            if (AllTableNameArray.Length > 1)
                            {
                                int i = 1;
                                extraWorkRecordtemp = ewrs.AdvancedSearch(AllTableNameArray[i], SearchConditionsArray[i], searchValueFirstArray[i])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                foreach (var temp in extraWorkRecordtemp)
                                {
                                    if (extraWorkRecordtemp1.FirstOrDefault(s => s.id.Equals(temp.id)) != null) Result.Add(temp);
                                }
                                if (AllTableNameArray.Length > 2)
                                {
                                    for (var i1 = 2; i1 < AllTableNameArray.Length; i1++)
                                    {
                                        IQueryable<ExtraWorkRecordView> ExtraWorkRecordtemp2 = null;
                                        ExtraWorkRecordtemp2 = ewrs.AdvancedSearch(AllTableNameArray[i1], SearchConditionsArray[i1], searchValueFirstArray[i1])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                        List<ExtraWorkRecordView> Resulttemp = new List<ExtraWorkRecordView>();

                                        foreach (var addtemp in Result)
                                        {
                                            Resulttemp.Add(addtemp);
                                        }

                                        foreach (var temp in Result)
                                        {
                                            if (ExtraWorkRecordtemp2.FirstOrDefault(s => s.id.Equals(temp.id)) == null)
                                            {
                                                ExtraWorkRecordView removetemp = temp;
                                                Resulttemp.Remove(Resulttemp.Where(s => s.id == removetemp.id).FirstOrDefault());
                                            }
                                        }
                                        Result = Resulttemp;
                                    }
                                }
                            }
                            else
                            {
                                Result = extraWorkRecordtemp1.ToList();
                            }
                        }
                        catch (Exception)
                        {
                            Result = null;
                        }

                    }
                }
            }
            extraWorkRecords = Result.Distinct().ToPagedList(pageIndex, Settings.Default.pageSize);
            SetDropDownList(null);

            return View("Index", extraWorkRecords);
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
                    msg.Content = isSucceed ? "删除成功" : "删除失败";

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
                SetAllTableName(null);
                SetSearchConditions(null);
            }
            else
            {
                SetExtraWorkTypeList(null);
                SetDurationTypeCodeList(null);
                SetAllTableName(null);
                SetSearchConditions(null);
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
        [RoleAndDataAuthorizationAttribute]
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

                var ids = extralRecordId.Split(',');
                bool isSucceed=false;
                foreach (var id in ids)
                {
                    ExtraWorkRecordApproval extralApproval = new ExtraWorkRecordApproval();
                    extralApproval.extraWorkId = !string.IsNullOrEmpty(id) ? int.Parse(id) : -1;
                    extralApproval.approvalStatus = approvalStatus;
                    extralApproval.approvalTime = DateTime.Now;
                    extralApproval.remarks = approvalRemarks;

                    if (Session["user"] != null)
                    {
                        User user = Session["user"] as User;
                        extralApproval.userId = user.id;
                    }

                    IExtraWorkRecordService cs = new ExtraWorkRecordService(Settings.Default.db);
                 isSucceed= cs.ApprovalTheRecord(extralApproval);
                }
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
        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IExtraWorkRecordService at = new ExtraWorkRecordService(Settings.Default.db);

            var ExtraWorkRecord = at.GetAllTableName();

            if (ExtraWorkRecord != null)
            {
                //获取当前记录的属性
                foreach (var property in ExtraWorkRecord[0].GetType().GetProperties())
                {
                    select.Add(new SelectListItem { Text = property.Name, Value = property.Name });
                }
            }

            ViewData["getAllTableNameList"] = select;
        }

        //private void SetSearchConditions(bool? type, bool allowBlank = false)
        //{
        //    var item = EnumHelper.GetList(typeof(SearchConditions));

        //    List<SelectListItem> select = new List<SelectListItem>();

        //    if (allowBlank)
        //    {
        //        select.Add(new SelectListItem { Text = "", Value = "" });
        //    }

        //    foreach (var it in item)
        //    {
        //        if (type.HasValue && type.ToString().Equals(it.Value))
        //        {
        //            select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
        //        }
        //        else
        //        {
        //            select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
        //        }
        //    }
        //    ViewData["searchConditionsList"] = select;
        //}

        private void SetAllTableName(string type, bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IExtraWorkRecordService ss = new ExtraWorkRecordService(Settings.Default.db);

            var extraWorkRecords = ss.GetAllTableName();
            if (extraWorkRecords.Count == 0)
            {
                ExtraWorkRecordView ewrtemp = new ExtraWorkRecordView();
                extraWorkRecords.Add(ewrtemp);
            }
            int i = 1;
            string showName = "";
            //获取当前记录的属性
            foreach (var property in extraWorkRecords[0].GetType().GetProperties())
            {
                switch (i)
                {
                    case 1: showName = "ID"; break;
                    case 2: showName = "加班类型ID"; break;
                    case 3: showName = "员工号"; break;
                    case 4: showName = "加班持续时长"; break;
                    case 5: showName = "原因"; break;
                    case 6: showName = "时间"; break;
                    case 7: showName = "开始时间（小时）"; break;
                    case 8: showName = "结束时间（小时）"; break;
                    case 9: showName = "系统代码"; break;
                    case 10: showName = "名称"; break;
                    case 11: showName = "员工号"; break;
                    case 12: showName = "员工姓名"; break;
                    case 49: showName = "创建用户ID"; break;
                    case 51: showName = "审批时间"; break;
                    case 52: showName = "审批用户ID"; break;
                    case 53: showName = "审批状态"; break;
                    case 54: showName = "备注"; break;
                    default:
                        break;
                }
                if (!string.IsNullOrWhiteSpace(type) && type.Equals(property.Name)&&showName!="")
                {
                    select.Add(new SelectListItem { Text = showName, Value = property.Name, Selected = true });
                }
                else if(showName != "")
                {
                    select.Add(new SelectListItem { Text = showName, Value = property.Name, Selected = false });
                }
                i++;
            }


            ViewData["getAllTableNameList"] = select;
        }

        private void SetSearchConditions(int? type, bool allowBlank = false)
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
