using BlueHrLib.Data;
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
using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;
using BlueHrLib.Data.Message;

namespace BlueHrWeb.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            CompanySearchModel q = new CompanySearchModel();

            ICompanyService ss = new CompanyService(Settings.Default.db);

            IPagedList<Company> companies = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View(companies);
        }

        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult TreeShow()
        {
            SetCompanyList(null);
            return View();
        }

        [RoleAndDataAuthorizationAttribute]

        public ActionResult Search([Bind(Include = "Name")] CompanySearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            ICompanyService ss = new CompanyService(Settings.Default.db);

            IPagedList<Company> companies = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", companies);
        }

        // GET: Company/Details/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Company/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "Name, remark, address")] Company company)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                // TODO: Add insert logic here

                ICompanyService cs = new CompanyService(Settings.Default.db);

                bool isSucceed = cs.Create(company);
                msg.Success = isSucceed;
                msg.Content = isSucceed ? "添加成功" : "添加失败";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Company/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            ICompanyService cs = new CompanyService(Settings.Default.db);

            Company cp = cs.FindById(id);

            return View(cp);
        }

        // POST: Company/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id, name, address, remark")] Company company)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                // TODO: Add update logic here
                ICompanyService cs = new CompanyService(Settings.Default.db);
                bool isSucceed = cs.Update(company);
                msg.Success = isSucceed;
                msg.Content = isSucceed ? "编辑成功" : "编辑失败";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Company/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            ICompanyService cs = new CompanyService(Settings.Default.db);

            Company cp = cs.FindById(id);

            return View(cp);
        }

        // POST: Company/Delete/5
        [HttpPost]
        [RoleAndDataAuthorizationAttribute]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            ICompanyService cs = new CompanyService(Settings.Default.db);
            Company company = cs.FindById(id);
            try
            {
                // TODO: Add delete logic here
                bool isSucceed = cs.DeleteById(id);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "删除成功" : "删除失败";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult CompanyTree()
        {
            List<Dictionary<string, string>> Result = new List<Dictionary<string, string>>();

            CompanySearchModel q = new CompanySearchModel();
            ICompanyService ss = new CompanyService(Settings.Default.db);
            List<Company> companies = ss.Search(q).ToList();

            foreach(var company in companies)
            {
                Dictionary<string, string> cp = new Dictionary<string, string>();
                cp.Add("id", company.id.ToString());
                cp.Add("name", company.name);
                cp.Add("address", company.address);
                cp.Add("remark", company.remark);
                cp.Add("open", "true");
                cp.Add("iconSkin", "shopIcon");

                Result.Add(cp);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult CountDepartment()
        {
            List<Dictionary<string, string>> Result = new List<Dictionary<string, string>>();
            ICompanyService cs = new CompanyService(Settings.Default.db);
            List<Company> CompanyName = new List<Company>();
            CompanyName = cs.GetAll();
            foreach (var it in CompanyName)
            {
                Dictionary<string, string> cp = new Dictionary<string, string>();
                cp.Add("CompanyName",it.name);
                cp.Add("DepartmentCount",cs.countDepartment(it.id).ToString()); 
                Result.Add(cp);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CompanyOrganization(int companyId)
        {
            List<Dictionary<string, string>> Result = new List<Dictionary<string, string>>();
            ICompanyService ss = new CompanyService(Settings.Default.db);
            Company companie = ss.FindById(companyId);

            IStaffService staffService = new StaffService(Settings.Default.db);
            int? departmentId = null;
            int peopleCount = staffService.FindByCompanyAndDepartment(companyId, departmentId).Count();

            Dictionary<string, string> Company = new Dictionary<string, string>();
            Company.Add("key", "Company" + companie.id);
            Company.Add("name", companie.name);
            Company.Add("address", companie.address);
            Company.Add("remark", companie.remark);
            Company.Add("peopleCount", "共" + peopleCount.ToString() + "人");
            Company.Add("Source", "../Images/treeIcon/diy/company.png");

            Result.Add(Company);

            //通过公司获取到相应的部门
            IDepartmentService cs = new DepartmentService(Settings.Default.db);
            DepartmentSearchModel dq = new DepartmentSearchModel();
            List<Department> departments = cs.FindByCompanyId(companie.id).ToList();
            //获取到所有的部门
            foreach (var department in departments)
            {
                Dictionary<string, string> cp = new Dictionary<string, string>();


                if (department.parentId.HasValue)
                {
                    int DeparentmentPeopleCount = staffService.FindByCompanyAndDepartment(companyId, department.parentId).Count();
                    cp.Add("key", department.id.ToString());
                    cp.Add("name", department.name);
                    cp.Add("parent", department.parentId.ToString());
                    cp.Add("companyId", department.companyId.ToString());
                    cp.Add("remark", department.remark);
                    cp.Add("peopleCount", DeparentmentPeopleCount.ToString() + "人");
                    cp.Add("Source", "../Images/treeIcon/diy/user.png");
                }
                else
                {
                    int TwiceDeparentmentPeopleCount = staffService.FindByCompanyAndDepartment(companyId, department.id).Count();

                    cp.Add("key", department.id.ToString());
                    cp.Add("name", department.name);
                    cp.Add("parent", "Company" + companie.id);
                    cp.Add("companyId", department.companyId.ToString());
                    cp.Add("remark", department.remark);
                    cp.Add("peopleCount", TwiceDeparentmentPeopleCount.ToString() + "人");
                    cp.Add("Source", "../Images/treeIcon/diy/user.png");
                }

                Result.Add(cp);

                //通过部门 获取到所属员工, 暂时不考虑
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        
        private void SetCompanyList(int? type, bool allowBlank = false)
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
                if (type.HasValue && type.Value == company.id)
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


        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            ICompanyService at = new CompanyService(Settings.Default.db);

            var Company = at.GetAllTableName();

            if (Company != null)
            {
                //获取当前记录的属性
                foreach (var property in Company[0].GetType().GetProperties())
                {
                    select.Add(new SelectListItem { Text = property.Name, Value = property.Name });
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
    }
}
