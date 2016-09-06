using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Message;
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
using System.IO;
using System.Linq;
using System.Net.Http;
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

            SetDropDownList(null);

            return View(staffs);
        }

        public ActionResult Search([Bind(Include = "Nr, Name, Id, Sex, JobTitleId, CompanyId, DepartmentId, CompanyEmployAtFrom, CompanyEmployAtTo, IsOnTrial")] StaffSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IStaffService ss = new StaffService(Settings.Default.db);

            IPagedList<Staff> staffs = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            Staff staff = new Staff();
            staff.companyId = Convert.ToInt16(q.companyId);
            staff.departmentId = Convert.ToInt16(q.departmentId);
            staff.sex = Convert.ToString(q.Sex);
            staff.jobTitleId = q.JobTitleId;
            staff.isOnTrial = Convert.ToBoolean(q.IsOnTrial);
            SetDropDownList(staff);

            ViewBag.Query = q;

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
            SetDropDownList(null);

            return View();
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Nr, Name, Sex, BirthDay, FirstCompanyEmployAt, CompanyEmployAt,"+
            " WorkStatus, IsOnTrial, TrialOverAt, CompanyId, DepartmentId, jobTitleId, Photo, StaffTypeId, DegreeTypeId, "+
            "Speciality, ResidenceAddress, Address, Id, Phone, ContactName, ContactPhone, ContactFamilyMemberType, Domicile, "+
            "ResidenceType, inSureTypeId, IsPayCPF, ContractExpireAt, ContractCount, Ethnic, Remark")] Staff staff)
        {
            // TODO: Add insert logic here

            //银行卡信息
            string bank = null, bankCard = null, bankAddress = null, bankRemark = null;
            if(!string.IsNullOrWhiteSpace(Request.Form["bank"])){
                bank = Request.Form["bank"];
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["bankCard"]))
            {
                bankCard = Request.Form["bankCard"];
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["bankAddress"]))
            {
                bankAddress = Request.Form["bankAddress"];
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["bankRemark"]))
            {
                bankRemark = Request.Form["bankRemark"];
            }

            BankCard bankCardDB = new BankCard();
            bankCardDB.nr = bankCard;
            bankCardDB.bankAddress = bankAddress;
            bankCardDB.bank = bank;
            bankCardDB.remark = bankRemark;
            bankCardDB.staffNr = staff.nr;

            staff.BankCard.Add(bankCardDB);

            //子女信息
            string familyName = null, familyType = null, familyBirthday = null;
            if (!string.IsNullOrWhiteSpace(Request.Form["familyName"]))
            {
                familyName = Request.Form["familyName"];
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["familyType"]))
            {
                familyType = Request.Form["familyType"];
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["familyBirthday"]))
            {
                familyBirthday = Request.Form["familyBirthday"];
            }

            FamilyMemeber familyMember = new FamilyMemeber();
            familyMember.memberName = familyName;
            familyMember.familyMemberType = familyType;
            //强制类型转换，多测试
            familyMember.birthday = Convert.ToDateTime(familyBirthday);
            familyMember.staffNr = staff.nr;

            IStaffService ss = new StaffService(Settings.Default.db);

            bool result = ss.Create(staff);

            if (result)
            {
                SetDropDownList(staff);

                //添加银行卡和子女信息
                IBankCardService bcs = new BankCardService(Settings.Default.db);

                bool bankResult =  bcs.Create(bankCardDB);

                if (!bankResult)
                {
                    SetDropDownList(null);
                    return View();
                }

                IFamilyMemberService fms = new FamilyMemberService(Settings.Default.db);
                bool familyResult = fms.Create(familyMember);

                if (!familyResult)
                {
                    SetDropDownList(null);
                    return View();
                }

                return RedirectToAction("Index");
            }
            else
            {
                SetDropDownList(null);
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(string nr)
        {
            IStaffService ss = new StaffService(Settings.Default.db);

            Staff staff = ss.FindByNr(nr);
     
                SetDropDownList(staff);

                StaffSearchModel q = new StaffSearchModel();

                q.companyId = staff.companyId;
                q.departmentId = staff.departmentId;

                ViewBag.Query = q;

                return View(staff);
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, name, address, remark")] Staff staff)
        {
            try
            {
                // TODO: Add update logic here
                IStaffService cs = new StaffService(Settings.Default.db);
                //cs.Update(staff);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        public ActionResult Delete(string nr)
        {
            IStaffService ss = new StaffService(Settings.Default.db);

            Staff staff = ss.FindByNr(nr);

            SetDropDownList(staff);

            StaffSearchModel q = new StaffSearchModel();

            q.companyId = staff.companyId;
            q.departmentId = staff.departmentId;

            ViewBag.Query = q;

            return View(staff);
        }

        // POST: Company/Delete/5
        [HttpPost]
        public ActionResult Delete(string nr, FormCollection collection)
        {
            // TODO: Add delete logic here
            IStaffService ss = new StaffService(Settings.Default.db);

            bool result = ss.DeleteByNr(nr);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult uploadImage()
        {
            var ff = Request.Files[0];

            string fileName = Helpers.FileHelper.SaveUploadImage(ff);
            ResultMessage msg = new ResultMessage() { Success = true };
            msg.Content = fileName;
            //防止IE直接下载json数据
            return Json(msg, "text/html");
           // return Json(fileName, JsonRequestBehavior.DenyGet);
        }

        private void SetDropDownList(Staff staff)
        {
            if (staff != null)
            {
                SetIsOnTrialList(staff.isOnTrial);
                SetSexList(Convert.ToInt16(staff.sex));
                SetJobTitleList(staff.jobTitleId);
                SetCompanyList(staff.companyId);
                SetDepartmentList(staff.companyId, staff.departmentId);
                SetStaffTypeList(staff.staffTypeId);
                SetDegreeTypeList(staff.degreeTypeId);
                SetInSureTypeList(staff.insureTypeId);
                SetIsPayCPFList(staff.isPayCPF);
                SetResidenceTypeList(staff.residenceType);
                SetWorkStatusList(staff.workStatus);
            }
            else
            {
                SetIsOnTrialList(false);
                SetSexList(null);
                SetJobTitleList(null);
                SetCompanyList(null);
                SetDepartmentList(null, null);
                SetStaffTypeList(null);
                SetDegreeTypeList(null);
                SetInSureTypeList(null);
                SetIsPayCPFList(false);
                SetResidenceTypeList(0);
                SetWorkStatusList(100);
            }
        }

        private void SetIsOnTrialList(bool? type, bool allowBlank = true)
        {
            List<EnumItem> item = new List<EnumItem>() { new EnumItem() { Text = "是", Value = "true" }, new EnumItem() { Text = "否", Value = "false" } };
            //EnumHelper.GetList(typeof(IsOnTrail));

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

        private void SetIsPayCPFList(bool? type, bool allowBlank = true)
        {
            List<EnumItem> item = new List<EnumItem>() { new EnumItem() { Text = "是", Value = "true" }, new EnumItem() { Text = "否", Value = "false" } };

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

        private void SetWorkStatusList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(WorkStatus));

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
            ViewData["workStatusList"] = select;
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

                foreach (var dep in deps)
                {
                    department.Add(dep.id.ToString(), dep.name);
                }

                departments.Add(company.id.ToString(), department);
            }

            return Json(departments, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 员工转正
        /// </summary>
        /// <returns></returns>
        public ActionResult ToFullMemeber(string nr)
        {
            IStaffService ss = new StaffService(Settings.Default.db);
            Staff staff = ss.FindByNr(nr);
            return View(staff);
        }

        /// <summary>
        /// 执行员工转正
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DoToFullMemeber([Bind(Include = "staffNr, isPassCheck, beFullAt, checkAt,beFullChecker,checkScore,remark")] FullMemberRecord record)
        {
            ResultMessage msg = new ResultMessage();
            if (record.isPassCheck)
            {
                if (record.beFullAt == null)
                {
                    msg.Content = "请填写转正日期";
                    return Json(msg);
                }
            }

            IStaffService ss = new StaffService(Settings.Default.db);
            msg = ss.ToFullMember(record);

            return Json(msg);
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
