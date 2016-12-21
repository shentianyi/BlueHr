﻿using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Message;
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

namespace BlueHrWeb.Controllers
{
    public class ShiftJobRecordController : Controller
    {
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            ShiftJobRecordSearchModel q = new ShiftJobRecordSearchModel();

            IShiftJobRecordService lrs = new ShiftJobRecordService(Settings.Default.db);

            IPagedList<ShiftJobRecord> shiftJobRecords = lrs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;
            SetDropDownList(null);

            return View(shiftJobRecords);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "staffNr")] ShiftJobRecordSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IShiftJobRecordService rrs = new ShiftJobRecordService(Settings.Default.db);

            IPagedList<ShiftJobRecord> shiftJobRecords = rrs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;
            SetDropDownList(null);

            return View("Index", shiftJobRecords);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            IShiftJobRecordService sjs = new ShiftJobRecordService(Settings.Default.db);
            SetDropDownList(sjs.FindById(id));
            return View(sjs.FindById(id));
        }

        // GET: ShiftJobRecord/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: ShiftJobRecord/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "staffNr, afterCompanyId, afterDepartmentId, afterJobId, remark")] ShiftJobRecord shiftJobRecord)
        {
            ResultMessage msg = new ResultMessage();

            msg = DoValidation(shiftJobRecord);

            if (!msg.Success)
            {
                return Json(msg, JsonRequestBehavior.DenyGet);
            }

            //先判断是否与已经提交申请了
            IShiftJobRecordService lrs = new ShiftJobRecordService(Settings.Default.db);

            ShiftJobRecordSearchModel q = new ShiftJobRecordSearchModel();

            ShiftJobRecord shiftJobRecordExist = lrs.Search(q).FirstOrDefault(c => c.staffNr.Equals(shiftJobRecord.staffNr));

            if (shiftJobRecordExist != null&& !shiftJobRecordExist.approvalUserId.HasValue)
            {
                //如果没有值， 将可以再次提交申请
                msg.Success = false;
                msg.Content = "该员工已经提交申请， 请等待审核";
                return Json(msg, JsonRequestBehavior.DenyGet);
            }

            //可以进行查询了，准备进行填充数据
            IStaffService ss = new StaffService(Settings.Default.db);
            Staff tempstaff = ss.FindByNr(shiftJobRecord.staffNr);

            shiftJobRecord.beforeCompanyId = tempstaff.companyId;
            shiftJobRecord.beforeDepartmentId = tempstaff.departmentId;
            shiftJobRecord.beforeJobId = tempstaff.jobTitleId;

            User user = System.Web.HttpContext.Current.Session["user"] as User;
            shiftJobRecord.userId = user.id;

            shiftJobRecord.createdAt = DateTime.Now;

            try
            {

                bool isSucceed = lrs.Create(shiftJobRecord);
                msg.Success = isSucceed;
                msg.Content = isSucceed ? "离职申请提交成功" : "离职申请提交失败";

                return Json(msg, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                msg.Success = false;
                msg.Content = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: ShiftJobRecord/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IShiftJobRecordService sjs = new ShiftJobRecordService(Settings.Default.db);
            SetDropDownList(sjs.FindById(id));

            return View(sjs.FindById(id));
        }

        // POST: ShiftJobRecord/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id, staffNr, afterCompanyId, afterDepartmentId, afterJobId, remark")] ShiftJobRecord shiftJobRecord)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(shiftJobRecord);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IShiftJobRecordService cs = new ShiftJobRecordService(Settings.Default.db);
                    bool isSucceed = cs.Update(shiftJobRecord);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "离职申请更新成功" : "离职申请更新失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: ShiftJobRecord/Approval/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Approval([Bind(Include = "id, approvalStatus, approvalRemark")] ShiftJobRecord shiftJobRecord)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                IStaffService ss = new StaffService(Settings.Default.db);

                //填充数据
                string tempApprovalStatus = shiftJobRecord.approvalStatus;
                string tempApprovalRemark = shiftJobRecord.approvalRemark;
                IShiftJobRecordService ijrs = new ShiftJobRecordService(Settings.Default.db);
                shiftJobRecord = ijrs.FindById(shiftJobRecord.id);
                shiftJobRecord.approvalStatus = tempApprovalStatus;
                shiftJobRecord.approvalRemark = tempApprovalRemark;

                //先获取之前的Jobstr
                Staff staff = ss.FindByNr(shiftJobRecord.staffNr);
                string oldCompany = staff.Company == null ? string.Empty : staff.Company.name;
                string oldDepartment = staff.Department == null ? string.Empty : staff.Department.name;
                string oldJobTitle = staff.JobTitle == null ? string.Empty : staff.JobTitle.name;
                string oldJobStr = string.Format("{0}-{1}-{2}", oldCompany, oldDepartment, oldJobTitle);

                User user = System.Web.HttpContext.Current.Session["user"] as User;
                shiftJobRecord.approvalAt = DateTime.Now;
                shiftJobRecord.approvalUserId = user.id;
                IShiftJobRecordService lrs = new ShiftJobRecordService(Settings.Default.db);
                bool isSucceed = lrs.Update(shiftJobRecord);

                string[] changeJob=new string[4];
                changeJob[0] = shiftJobRecord.staffNr;
                changeJob[1] = shiftJobRecord.afterCompanyId.ToString();
                changeJob[2] = shiftJobRecord.afterDepartmentId.ToString();
                changeJob[3] = shiftJobRecord.afterJobId.ToString();
                //改变staff表
                bool JobReturn = ss.ChangeJob(changeJob);
                try
                {
                    Staff newStaff = ss.FindByNr(changeJob[0]);
                    string newCompany = newStaff.Company == null ? string.Empty : newStaff.Company.name;
                    string newDepartment = newStaff.Department == null ? string.Empty : newStaff.Department.name;
                    string newJobTitle = newStaff.JobTitle == null ? string.Empty : newStaff.JobTitle.name;
                    string newJobStr = string.Format("{0}-{1}-{2}", newCompany, newDepartment, newJobTitle);
                    IMessageRecordService mrs = new MessageRecordService(Settings.Default.db);
                    //创建messageRecord记录
                    mrs.CreateStaffShiftJobMessage(changeJob[0], (Session["user"] as User).id, oldJobStr, newJobStr);
                }
                catch { }

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "审批成功" : "审批失败";

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: ShiftJobRecord/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IShiftJobRecordService sjs = new ShiftJobRecordService(Settings.Default.db);

            ShiftJobRecord lr = sjs.FindById(id);
            //SetDropDownList(cp);
            SetDropDownList(lr);

            return View(lr);
        }

        // POST: ShiftJobRecord/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                IShiftJobRecordService lrs = new ShiftJobRecordService(Settings.Default.db);
                bool isSucceed = lrs.DeleteById(id);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "离职记录删除成功" : "离职记录删除失败";

                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult AdvancedSearch(ShiftJobRecordSearchModel q)
        {
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            q.loginUser = user;
            ViewBag.Query = q;

            IShiftJobRecordService ss = new ShiftJobRecordService(Settings.Default.db);
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IPagedList<ShiftJobRecord> ShiftJobRecords = null;
            IQueryable<ShiftJobRecord> ShiftJobRecordtemp = null;
            IQueryable<ShiftJobRecord> ShiftJobRecordtemp1 = null;
            List<ShiftJobRecord> Result = new List<ShiftJobRecord>();
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
                            ShiftJobRecordtemp1 = ss.AdvancedSearch(AllTableNameArray[0], SearchConditionsArray[0], searchValueFirstArray[0])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                            if (AllTableNameArray.Length > 1)
                            {
                                int i = 1;
                                ShiftJobRecordtemp = ss.AdvancedSearch(AllTableNameArray[i], SearchConditionsArray[i], searchValueFirstArray[i])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                foreach (var temp in ShiftJobRecordtemp)
                                {
                                    if (ShiftJobRecordtemp1.FirstOrDefault(s => s.id.Equals(temp.id)) != null) Result.Add(temp);
                                }
                                if (AllTableNameArray.Length > 2)
                                {
                                    for (var i1 = 2; i1 < AllTableNameArray.Length; i1++)
                                    {
                                        IQueryable<ShiftJobRecord> ShiftJobRecordtemp2 = null;
                                        ShiftJobRecordtemp2 = ss.AdvancedSearch(AllTableNameArray[i1], SearchConditionsArray[i1], searchValueFirstArray[i1])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                        List<ShiftJobRecord> Resulttemp = new List<ShiftJobRecord>();

                                        foreach (var addtemp in Result)
                                        {
                                            Resulttemp.Add(addtemp);
                                        }

                                        foreach (var temp in Result)
                                        {
                                            if (ShiftJobRecordtemp2.FirstOrDefault(s => s.id.Equals(temp.id)) == null)
                                            {
                                                ShiftJobRecord removetemp = temp;
                                                Resulttemp.Remove(Resulttemp.Where(s => s.id == removetemp.id).FirstOrDefault());
                                            }
                                        }
                                        Result = Resulttemp;
                                    }
                                }
                            }
                            else
                            {
                                Result = ShiftJobRecordtemp1.ToList();
                            }
                        }
                        catch (Exception)
                        {
                            Result = null;
                        }

                    }
                }
            }
            try
            {
                ShiftJobRecords = Result.Distinct().ToPagedList(pageIndex, Settings.Default.pageSize);
            }catch { ShiftJobRecords = null; }

            SetDropDownList(null);
            return View("Index", ShiftJobRecords);
        }


        [HttpPost]
        public ResultMessage DoValidation(ShiftJobRecord shiftJobRecord)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(shiftJobRecord.staffNr))
            {
                msg.Success = false;
                msg.Content = "员工号不能为空";

                return msg;
            }

            IStaffService ss = new StaffService(Settings.Default.db);
            if (ss.FindByNr(shiftJobRecord.staffNr) == null)
            {
                msg.Success = false;
                msg.Content = "员工号不存在";

                return msg;
            }

            if (shiftJobRecord.afterCompanyId == null)
            {
                msg.Success = false;
                msg.Content = "公司不可为空";

                return msg;
            }
            if (shiftJobRecord.afterDepartmentId == null)
            {
                msg.Success = false;
                msg.Content = "部门不可为空";

                return msg;
            }

            //if (shiftJobRecord.afterJobId == null)
            //{
            //    msg.Success = false;
            //    msg.Content = "职位不可为空";

            //    return msg;
            //}

            return new ResultMessage() { Success = true, Content = "" };
        }

        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IShiftJobRecordService lrs = new ShiftJobRecordService(Settings.Default.db);
            ShiftJobRecordSearchModel lrsm = new ShiftJobRecordSearchModel();
            var ShiftJobRecord = lrs.Search(lrsm).ToList();
            if (ShiftJobRecord.Count == 0)
            {
                ShiftJobRecord tempShiftJobRecord = new ShiftJobRecord();
                ShiftJobRecord.Add(tempShiftJobRecord);
            }
            if (ShiftJobRecord != null)
            {
                int i = 1;
                //获取当前记录的属性
                foreach (var property in ShiftJobRecord[0].GetType().GetProperties())
                {
                    if (i <= 15) select.Add(new SelectListItem { Text = property.Name, Value = property.Name });
                    i++;
                }
            }

            ViewData["getAllTableNameList"] = select;
        }

        private void SetSearchConditions(bool? type, bool allowBlank = false)
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

        private void SetDropDownList(ShiftJobRecord shiftJobRecord)
        {
            if (shiftJobRecord != null)
            {
                SetJobTitleList(shiftJobRecord.beforeJobId);
                SetJobTitleList(shiftJobRecord.afterJobId);
                SetCompanyList(shiftJobRecord.beforeCompanyId);
                SetCompanyList(shiftJobRecord.afterCompanyId);
                SetDepartmentList(shiftJobRecord.afterCompanyId, shiftJobRecord.afterDepartmentId);
                SetDepartmentList(shiftJobRecord.beforeCompanyId, shiftJobRecord.beforeDepartmentId);
                SetAllTableName(false);
                SetSearchConditions(null);
            }
            else
            {
                SetJobTitleList(null);
                SetCompanyList(null);
                SetDepartmentList(null, null);
                SetAllTableName(false);
                SetSearchConditions(null);
            }
        }

        private void SetJobTitleList(int? type, bool allowBlank = true)
        {
            IJobTitleService js = new JobTitleService(Settings.Default.db);

            JobTitleSearchModel jtsm = new JobTitleSearchModel();

            List<JobTitle> jt = js.Search(jtsm).ToList();

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var it in jt)
            {
                if (type.HasValue && type.ToString().Equals(it.id))
                {
                    select.Add(new SelectListItem { Text = it.name, Value = it.id.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = it.name, Value = it.id.ToString(), Selected = false });
                }
            }
            ViewData["jobTitleList"] = select;
        }


        private void SetDepartmentList(int? companyId, int? type, bool allowBlank = true)
        {
            IDepartmentService ds = new DepartmentService(Settings.Default.db);

            List<SelectListItem> select = new List<SelectListItem>();

            List<Department> departments = new List<Department>();
            if (companyId.HasValue)
            {
                departments = ds.FindByCompanyId(companyId).ToList();

                if (allowBlank)
                {
                    select.Add(new SelectListItem { Text = "", Value = "" });
                }

                foreach (var department in departments)
                {
                    if (type.HasValue && type.ToString().Equals(department.id))
                    {
                        select.Add(new SelectListItem { Text = department.fullName, Value = department.id.ToString(), Selected = true });
                    }
                    else
                    {
                        select.Add(new SelectListItem { Text = department.fullName, Value = department.id.ToString(), Selected = false });
                    }
                }
            }

            ViewData["departmentList"] = select;
        }

        private void SetCompanyList(int? type, bool allowBlank = true)
        {
            ICompanyService cs = new CompanyService(Settings.Default.db);

            CompanySearchModel csm = new CompanySearchModel();

            List<Company> companies = cs.Search(csm).ToList();

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var company in companies)
            {
                if (type.HasValue && type.ToString().Equals(company.id))
                {
                    select.Add(new SelectListItem { Text = company.name, Value = company.id.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = company.name, Value = company.id.ToString(), Selected = false });
                }
            }
            ViewData["companyList"] = select;
        }
    }
}
