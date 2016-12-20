using BlueHrLib.Data;
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
    public class PartTimeJobController : Controller
    {
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            PartTimeJobSearchModel q = new PartTimeJobSearchModel();

            IPartTimeJobService pjs = new PartTimeJobService(Settings.Default.db);

            IPagedList<PartTimeJob> PartTimeJobs = pjs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;
            SetDropDownList(null);

            return View(PartTimeJobs);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "staffNr")] PartTimeJobSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IPartTimeJobService pjs = new PartTimeJobService(Settings.Default.db);

            IPagedList<PartTimeJob> PartTimeJobs = pjs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;
            SetDropDownList(null);

            return View("Index", PartTimeJobs);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            IPartTimeJobService pjs = new PartTimeJobService(Settings.Default.db);
            SetDropDownList(pjs.FindById(id));
            return View(pjs.FindById(id));
        }

        // GET: PartTimeJob/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: PartTimeJob/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "staffNr, companyId, departmentId, jobTitleId, startTime,endTime")] PartTimeJob partTimeJob)
        {
            ResultMessage msg = new ResultMessage();

            msg = DoValidation(partTimeJob);
            partTimeJob.isDelete = false;
            if (!msg.Success)
            {
                return Json(msg, JsonRequestBehavior.DenyGet);
            }

            IPartTimeJobService lrs = new PartTimeJobService(Settings.Default.db);

            PartTimeJobSearchModel q = new PartTimeJobSearchModel();

            PartTimeJob partTimeJobExist = lrs.Search(q).FirstOrDefault(c => c.staffNr.Equals(partTimeJob.staffNr));

            if (partTimeJobExist != null && partTimeJobExist.companyId == partTimeJob.companyId && partTimeJobExist.departmentId == partTimeJob.departmentId && partTimeJobExist.jobTitleId == partTimeJob.jobTitleId)
            {
                msg.Success = false;
                msg.Content = "已有该员工的该兼职，请检查！";
                return Json(msg, JsonRequestBehavior.DenyGet);
            }
            try
            {

                bool isSucceed = lrs.Create(partTimeJob);
                msg.Success = isSucceed;
                msg.Content = isSucceed ? "员工兼职添加成功" : "员工兼职添加失败";

                return Json(msg, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                msg.Success = false;
                msg.Content = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: PartTimeJob/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IPartTimeJobService sjs = new PartTimeJobService(Settings.Default.db);
            SetDropDownList(sjs.FindById(id));

            return View(sjs.FindById(id));
        }

        // POST: PartTimeJob/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id, staffNr, companyId, departmentId, jobTitleId, startTime,endTime")] PartTimeJob partTimeJob)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(partTimeJob);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IPartTimeJobService cs = new PartTimeJobService(Settings.Default.db);
                    bool isSucceed = cs.Update(partTimeJob);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "员工兼职更新成功" : "员工兼职更新失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: PartTimeJob/SetIsDelete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult SetIsDelete(int id)
        {
            ResultMessage msg = new ResultMessage();
            IPartTimeJobService pjs = new PartTimeJobService(Settings.Default.db);

            try
            {
                PartTimeJob ptjb = new PartTimeJob();
                ptjb.id = id;
                ptjb.isDelete = true;
                bool isSucceed=pjs.Update(ptjb);
                msg.Success = isSucceed;
                msg.Content = isSucceed ? "员工兼职撤销成功" : "员工兼职撤销失败";

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: PartTimeJob/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IPartTimeJobService sjs = new PartTimeJobService(Settings.Default.db);

            PartTimeJob lr = sjs.FindById(id);
            //SetDropDownList(cp);
            SetDropDownList(lr);

            return View(lr);
        }

        // POST: PartTimeJob/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                IPartTimeJobService lrs = new PartTimeJobService(Settings.Default.db);
                bool isSucceed = lrs.DeleteById(id);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "员工兼职删除成功" : "员工兼职删除失败";

                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult AdvancedSearch(PartTimeJobSearchModel q)
        {
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            q.loginUser = user;
            ViewBag.Query = q;

            IPartTimeJobService ss = new PartTimeJobService(Settings.Default.db);
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IPagedList<PartTimeJob> PartTimeJobs = null;
            IQueryable<PartTimeJob> PartTimeJobtemp = null;
            IQueryable<PartTimeJob> PartTimeJobtemp1 = null;
            List<PartTimeJob> Result = new List<PartTimeJob>();
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
                            PartTimeJobtemp1 = ss.AdvancedSearch(AllTableNameArray[0], SearchConditionsArray[0], searchValueFirstArray[0])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                            if (AllTableNameArray.Length > 1)
                            {
                                int i = 1;
                                PartTimeJobtemp = ss.AdvancedSearch(AllTableNameArray[i], SearchConditionsArray[i], searchValueFirstArray[i])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                foreach (var temp in PartTimeJobtemp)
                                {
                                    if (PartTimeJobtemp1.FirstOrDefault(s => s.id.Equals(temp.id)) != null) Result.Add(temp);
                                }
                                if (AllTableNameArray.Length > 2)
                                {
                                    for (var i1 = 2; i1 < AllTableNameArray.Length; i1++)
                                    {
                                        IQueryable<PartTimeJob> PartTimeJobtemp2 = null;
                                        PartTimeJobtemp2 = ss.AdvancedSearch(AllTableNameArray[i1], SearchConditionsArray[i1], searchValueFirstArray[i1])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                        List<PartTimeJob> Resulttemp = new List<PartTimeJob>();

                                        foreach (var addtemp in Result)
                                        {
                                            Resulttemp.Add(addtemp);
                                        }

                                        foreach (var temp in Result)
                                        {
                                            if (PartTimeJobtemp2.FirstOrDefault(s => s.id.Equals(temp.id)) == null)
                                            {
                                                PartTimeJob removetemp = temp;
                                                Resulttemp.Remove(Resulttemp.Where(s => s.id == removetemp.id).FirstOrDefault());
                                            }
                                        }
                                        Result = Resulttemp;
                                    }
                                }
                            }
                            else
                            {
                                Result = PartTimeJobtemp1.ToList();
                            }
                        }
                        catch (Exception)
                        {
                            Result = null;
                        }

                    }
                }
            }
            PartTimeJobs = Result.Distinct().ToPagedList(pageIndex, Settings.Default.pageSize);

            SetDropDownList(null);
            return View("Index", PartTimeJobs);
        }


        [HttpPost]
        public ResultMessage DoValidation(PartTimeJob partTimeJob)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(partTimeJob.staffNr))
            {
                msg.Success = false;
                msg.Content = "员工号不能为空";

                return msg;
            }

            IStaffService ss = new StaffService(Settings.Default.db);
            if (ss.FindByNr(partTimeJob.staffNr) == null)
            {
                msg.Success = false;
                msg.Content = "员工号不存在";

                return msg;
            }

            if (partTimeJob.companyId ==0)
            {
                msg.Success = false;
                msg.Content = "公司不可为空";

                return msg;
            }
            if (partTimeJob.departmentId == 0)
            {
                msg.Success = false;
                msg.Content = "部门不可为空";

                return msg;
            }
            if (partTimeJob.jobTitleId == 0)
            {
                msg.Success = false;
                msg.Content = "职位不可为空";

                return msg;
            }

            return new ResultMessage() { Success = true, Content = "" };
        }

        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IPartTimeJobService lrs = new PartTimeJobService(Settings.Default.db);
            PartTimeJobSearchModel lrsm = new PartTimeJobSearchModel();
            var PartTimeJob = lrs.Search(lrsm).ToList();
            if (PartTimeJob.Count == 0)
            {
                PartTimeJob tempPartTimeJob = new PartTimeJob();
                PartTimeJob.Add(tempPartTimeJob);
            }
            if (PartTimeJob != null)
            {
                int i = 1;
                //获取当前记录的属性
                foreach (var property in PartTimeJob[0].GetType().GetProperties())
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

        private void SetDropDownList(PartTimeJob partTimeJob)
        {
            if (partTimeJob != null)
            {
                SetJobTitleList(partTimeJob.jobTitleId);
                SetCompanyList(partTimeJob.companyId);
                SetDepartmentList(partTimeJob.companyId, partTimeJob.departmentId);
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
