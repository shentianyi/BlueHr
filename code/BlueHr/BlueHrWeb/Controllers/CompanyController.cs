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
        public ActionResult Create([Bind(Include = "Name, remark, address")] Company company)
        {
            try
            {
                // TODO: Add insert logic here

                ICompanyService cs = new CompanyService(Settings.Default.db);

                cs.Create(company);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
        public ActionResult Edit([Bind(Include = "id, name, address, remark")] Company company)
        {
            try
            {
                // TODO: Add update logic here
                ICompanyService cs = new CompanyService(Settings.Default.db);
                cs.Update(company);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
        public ActionResult Delete(int id, FormCollection collection)
        {
            ICompanyService cs = new CompanyService(Settings.Default.db);
            Company company = cs.FindById(id);
            try
            {
                // TODO: Add delete logic here
                cs.DeleteById(id);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMsg = "删除失败， 请先删除部门后再删除公司";
                return View(company);
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
            ICompanyService cs = new CompanyService(Settings.Default.db);
            List<Company> CompanyName = new List<Company>();
            CompanyName = cs.GetAll();
            Dictionary<string, int> Result = new Dictionary<string, int>();
            foreach (var it in CompanyName)
            {
                Result.Add(it.name, cs.countDepartment(it.id));
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult CompanyOrganization(int companyId)
        {
            List<Dictionary<string, string>> Result = new List<Dictionary<string, string>>();
            ICompanyService ss = new CompanyService(Settings.Default.db);
            Company companie = ss.FindById(companyId);
          
            Dictionary<string, string> Company = new Dictionary<string, string>();
            Company.Add("key", "Company" + companie.id);
            Company.Add("name", companie.name);
            Company.Add("address", companie.address);
            Company.Add("remark", companie.remark);

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
                    cp.Add("key", department.id.ToString());
                    cp.Add("name", department.name);
                    cp.Add("parent", department.parentId.ToString());
                    cp.Add("companyId", department.companyId.ToString());
                    cp.Add("remark", department.remark);
                }
                else
                {
                    cp.Add("key", department.id.ToString());
                    cp.Add("name", department.name);
                    cp.Add("parent", "Company" + companie.id);
                    cp.Add("companyId", department.companyId.ToString());
                    cp.Add("remark", department.remark);
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
