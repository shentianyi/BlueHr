using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Helper;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.Helpers;
using BlueHrWeb.Properties;
using MvcPaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueHrWeb.Controllers
{
    public class StaffController : Controller
    {
        // GET: Company
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            StaffSearchModel q = new StaffSearchModel();

            IStaffService ss = new StaffService(Settings.Default.db);

            IPagedList<Staff> staffs = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SetIsOnTrialList(null);
            SetSexList(null);
            SetJobTitleList(null);
            SetCompanyList(null);
            SetDepartmentList(null, null);

            return View(staffs);
        }

        public ActionResult Search([Bind(Include = "Nr, Name, Id, Sex, JobTitleId, CompanyId, DepartmentId, CompanyEmployAtFrom, CompanyEmployAtTo, IsOnTrial")] StaffSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IStaffService ss = new StaffService(Settings.Default.db);

            IPagedList<Staff> staffs = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SetIsOnTrialList(q.IsOnTrial);
            SetSexList(q.Sex);
            SetJobTitleList(q.JobTitleId);
            SetCompanyList(q.CompanyId);
            SetDepartmentList(q.CompanyId, q.DepartmentId);

            return View("Index", staffs);
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            SetIsOnTrialList(0);
            SetSexList(0);
            SetJobTitleList(null);
            SetCompanyList(null);
            SetDepartmentList(null, null);
            SetStaffTypeList(null);
            SetDegreeTypeList(null);
            SetInSureTypeList(null);
            SetIsPayCPFList(0);
            SetResidenceTypeList(0);

            return View();
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Nr, Name, Sex, BirthDay, FirstCompanyEmployAt, CompanyEmployAt, WorkStatus, "+
            "IsOnTrial, TrialOverAt, CompanyId, DepartmentId, JobTitleId, Photo, StaffTypeId, DegreeTypeId, Speciality, ResidenceAddress, Address, Id, Phone,"+
            "ContactName, ContactPhone, ContactFamilyMemberType, Domicile, ResidenceType, InSureTypeId, IsPayCPF, ContractExpireAt, ContractCount, Ethnic, Remark")] Staff staff)
        {
            try
            {
                // TODO: Add insert logic here
                IStaffService ss = new StaffService(Settings.Default.db);
                bool result = ss.Create(staff);
                if (result)
                {
                    return RedirectToAction("Index");
                }else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            ICompanyService cs = new CompanyService(Settings.Default.db);

            Company cp = cs.FindById(id);

            return View(cp);
        }

        // POST: Company/Edit/5
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
        public ActionResult Delete(int id)
        {
            ICompanyService cs = new CompanyService(Settings.Default.db);

            Company cp = cs.FindById(id);

            return View(cp);
        }

        // POST: Company/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                ICompanyService cs = new CompanyService(Settings.Default.db);
                cs.DeleteById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void SetIsOnTrialList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(IsOnTrail));

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
            ViewData["isOnTrialList"] = select;
        }

        private void SetSexList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(Sex));

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
            ViewData["sexList"] = select;
        }

        private void SetIsPayCPFList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(IsPayCPF));

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
            ViewData["isPayCPFList"] = select;
        }

        private void SetResidenceTypeList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(ResidenceType));

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
            ViewData["residenceTypeList"] = select;
        }

        private void SetJobTitleList(int? type, bool allowBlank = true)
        {
            IJobTitleService js = new JobTitleService(Settings.Default.db);

            JobTitleSearchModel jtsm = new JobTitleSearchModel();

            List<JobTitle> jt =  js.Search(jtsm).ToList();

            List <SelectListItem> select = new List<SelectListItem>();

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

        private void SetStaffTypeList(int? type, bool allowBlank = true)
        {
            IStaffTypeService sts = new StaffTypeService(Settings.Default.db);

            StaffTypeSearchModel stsm = new StaffTypeSearchModel();

            List<StaffType> st = sts.Search(stsm).ToList();

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var it in st)
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
            ViewData["staffTypeList"] = select;
        }

        private void SetInSureTypeList(int? type, bool allowBlank = true)
        {
            IInSureTypeService ists = new InSureTypeService(Settings.Default.db);

            InSureTypeSearchModel istsm = new InSureTypeSearchModel();

            List<InsureType> it = ists.Search(istsm).ToList();

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var item in it)
            {
                if (type.HasValue && type.ToString().Equals(item.id))
                {
                    select.Add(new SelectListItem { Text = item.name, Value = item.id.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = item.name, Value = item.id.ToString(), Selected = false });
                }
            }
            ViewData["inSureTypeList"] = select;
        }

        private void SetDegreeTypeList(int? type, bool allowBlank = true)
        {
            IDegreeTypeService dts = new DegreeTypeService(Settings.Default.db);

            DegreeTypeSearchModel dtsm = new DegreeTypeSearchModel();

            List<DegreeType> dt = dts.Search(dtsm).ToList();

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var it in dt)
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
            ViewData["degreeTypeList"] = select;
        }

        [HttpGet]
        public JsonResult GetCompanyAndDepartment()
        {
            ICompanyService cs = new CompanyService(Settings.Default.db);
            CompanySearchModel csm = new CompanySearchModel();
            List<Company> companies = cs.Search(csm).ToList();
            IDepartmentService ds = new DepartmentService(Settings.Default.db);

            Dictionary<string, Dictionary<string, string>> departments = new Dictionary<string, Dictionary<string, string>>();

            foreach (var company in companies)
            {
                List<Department> deps = ds.FindByCompanyId(company.id).ToList();
                Dictionary<string, string> department = new Dictionary<string, string>();

                foreach(var dep in deps)
                {
                    department.Add(dep.id.ToString(), dep.name);
                }

                departments.Add(company.id.ToString(), department);
            }

            return Json(departments, JsonRequestBehavior.AllowGet);
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

                foreach(var department in departments)
                {
                    if (type.HasValue && type.ToString().Equals(department.id))
                    {
                        select.Add(new SelectListItem { Text = department.name, Value = department.id.ToString(), Selected = true });
                    }
                    else
                    {
                        select.Add(new SelectListItem { Text = department.name, Value = department.id.ToString(), Selected = false });
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
