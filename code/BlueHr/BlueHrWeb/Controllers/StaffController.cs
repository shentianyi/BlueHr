using BlueHrLib;
using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Helper;
using BlueHrLib.Helper.Excel;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.CustomAttributes;
using BlueHrWeb.Helpers;
using BlueHrWeb.Properties;
using MvcPaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
        [UserAuthorize]
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

        [UserAuthorize]
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
            //防止SetSexList 抛出异常
            if (q.Sex.HasValue)
            {
                staff.sex = Convert.ToString(q.Sex);
            }
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
        public ActionResult Create([Bind(Include = "Nr, Name, Sex, BirthDay, FirstCompanyEmployAt,totalCompanySeniority, CompanyEmployAt,"+
            "companySeniority, WorkStatus, IsOnTrial, TrialOverAt, CompanyId, DepartmentId, jobTitleId, Photo, StaffTypeId, DegreeTypeId, "+
            "Speciality, ResidenceAddress, Address, Id, Phone, ContactName, ContactPhone, ContactFamilyMemberType, Domicile, "+
            "ResidenceType, inSureTypeId, IsPayCPF, contractExpireStr, ContractCount,totalSeniority, Ethnic, Remark, workingYears")] Staff staff)
        {
            // TODO: Add insert logic here

            List<BankCard> bankInfo = new List<BankCard>();

            if (!string.IsNullOrWhiteSpace(Request.Form["bank"]))
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["bankCard"]))
                {
                    string bankTmp = Request.Form["bank"];
                    string bankCardTmp = Request.Form["bankCard"];
                    string bankAddressTmp = Request.Form["bankAddress"];
                    string bankRemarkTmp = Request.Form["bankRemark"];

                    string[] bankArray = bankTmp.Split(',');
                    string[] bankCardArray = bankCardTmp.Split(',');
                    string[] bankAddressArray = bankAddressTmp.Split(',');
                    string[] bankRemarkArray = bankRemarkTmp.Split(',');

                    for (var i = 0; i < bankArray.Length; i++)
                    {
                        BankCard bankCardDB = new BankCard();
                        bankCardDB.bank = bankArray[i];
                        bankCardDB.nr = bankCardArray[i];
                        bankCardDB.bankAddress = bankAddressArray[i];
                        bankCardDB.remark = bankRemarkArray[i];
                        bankCardDB.staffNr = staff.nr;
                        bankInfo.Add(bankCardDB);
                    }
                }
                else
                {

                }
            }
            else
            {

            }

            List<FamilyMemeber> familyInfo = new List<FamilyMemeber>();

            if (!string.IsNullOrWhiteSpace(Request.Form["familyName"]))
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["familyType"]))
                {
                    if (!string.IsNullOrWhiteSpace(Request.Form["familyBirthday"]))
                    {
                        string familyNameTmp = Request.Form["familyName"];
                        string familyTypeTmp = Request.Form["familyType"];
                        string familyBirthdayTmp = Request.Form["familyBirthday"];

                        string[] familyNameArray = familyNameTmp.Split(',');
                        string[] familyTypeArray = familyTypeTmp.Split(',');
                        string[] familyBirthdayArray = familyBirthdayTmp.Split(',');

                        for (var i = 0; i < familyNameArray.Length; i++)
                        {
                            FamilyMemeber familyMemberDB = new FamilyMemeber();
                            familyMemberDB.memberName = familyNameArray[i];
                            familyMemberDB.familyMemberType = familyTypeArray[i];
                            familyMemberDB.birthday = Convert.ToDateTime(familyBirthdayArray[i]);
                            familyMemberDB.staffNr = staff.nr;
                            familyInfo.Add(familyMemberDB);
                        }
                    }
                    else
                    {
                    }

                }
                else
                {
                }
            }
            else
            {
            }

            if (!string.IsNullOrWhiteSpace(staff.photo))
            {
                //要想显示照片， 必须添加头  data:image/jpg;base64,
                string base64Photo = BlueHrLib.Helper.FileHelper.ImageToBase64(HttpRuntime.AppDomainAppPath + "UploadImage/" + staff.photo);
                base64Photo = "data:image/jpg;base64," + base64Photo;
                staff.photo = base64Photo;
            }

            IStaffService ss = new StaffService(Settings.Default.db);

            bool result = ss.Create(staff);

            if (result)
            {
                SetDropDownList(staff);

                //添加银行卡和子女信息
                IBankCardService bcs = new BankCardService(Settings.Default.db);

                for (var i = 0; i < bankInfo.Count; i++)
                {
                    bool bankResult = bcs.Create(bankInfo[i]);

                    if (!bankResult)
                    {
                        SetDropDownList(null);
                        return View();
                    }
                }

                IFamilyMemberService fms = new FamilyMemberService(Settings.Default.db);

                for (var j = 0; j < familyInfo.Count; j++)
                {
                    bool familyResult = fms.Create(familyInfo[j]);

                    if (!familyResult)
                    {
                        SetDropDownList(null);
                        return View();
                    }
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
        [UserAuthorize]
        public ActionResult Edit(string nr)
        {
            IStaffService ss = new StaffService(Settings.Default.db);

            Staff staff = ss.FindByNr(nr);
            SetDropDownList(staff);
            StaffSearchModel q = new StaffSearchModel();

            try
            {
                q.companyId = staff.companyId;
                q.departmentId = staff.departmentId;
            }
            catch (Exception)
            {
                q.companyId = null;
                q.departmentId = null;
            }
            ViewBag.Query = q;

            return View(staff);
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Nr, Name, Sex, BirthDay, FirstCompanyEmployAt,totalCompanySeniority, CompanyEmployAt,"+
            "companySeniority, WorkStatus, IsOnTrial, TrialOverAt, CompanyId, DepartmentId, jobTitleId, Photo, StaffTypeId, DegreeTypeId, "+
            "Speciality, ResidenceAddress, Address, Id, Phone, ContactName, ContactPhone, ContactFamilyMemberType, Domicile, "+
            "ResidenceType, inSureTypeId, IsPayCPF, contractExpireStr, ContractCount,totalSeniority, Ethnic, Remark, workingYears")] Staff staff)
        {
            try
            {
                // TODO: Add update logic here
                //如果在编辑的时候没有替换图片，那么就不做替换
                //如果替换了图片，那么就进行处理

                if (!string.IsNullOrWhiteSpace(staff.photo))
                {
                    //要想显示照片， 必须添加头  data:image/jpg;base64,
                    string base64Photo = BlueHrLib.Helper.FileHelper.ImageToBase64(HttpRuntime.AppDomainAppPath + "UploadImage/" + staff.photo);
                    if (base64Photo != null)
                    {
                        base64Photo = "data:image/jpg;base64," + base64Photo;
                        staff.photo = base64Photo;
                    }
                }

                //获取信息 进行编辑
                //银行卡和子女信息 使用ajax 进行更新和删除

                IStaffService cs = new StaffService(Settings.Default.db);
                // 创建修改用户基本信息记录##User##
                staff.OperatorId = (Session["user"] as User).id;

                bool updateResult = cs.Update(staff);

                if (!updateResult)
                {
                    SetDropDownList(staff);
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                SetDropDownList(null);
                return View();
            }
        }

        // GET: Company/Delete/5
        [UserAuthorize]
        public ActionResult Delete(string nr)
        {
            IStaffService ss = new StaffService(Settings.Default.db);

            Staff staff = ss.FindByNr(nr);

            SetDropDownList(staff);

            StaffSearchModel q = new StaffSearchModel();

            try
            {
                q.companyId = staff.companyId;
                q.departmentId = staff.departmentId;
            }
            catch (Exception)
            {
                q.companyId = null;
                q.departmentId = null;
            }
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
                SetDropDownList(null);
                return View();
            }
        }

        //可以考虑 写入银行卡的Controller
        [HttpPost]
        public JsonResult CreateBankCard(string[] bankCard)
        {
            BankCard bc = new BankCard();

            bc.bank = bankCard[0];
            bc.nr = bankCard[1];
            bc.bankAddress = bankCard[2];
            bc.remark = bankCard[3];
            bc.staffNr = bankCard[4];

            IBankCardService bcs = new BankCardService(Settings.Default.db);

            BankCard bankCardReturn = bcs.CreateFromAjax(bc);

            ResultMessage msg;

            if (bankCardReturn != null)
            {
                msg = new ResultMessage() { Success = true };
                //将ID返回给前端用来删除
                msg.Content = bankCardReturn.id.ToString();
            }
            else
            {
                msg = new ResultMessage() { Success = false };
                msg.Content = "新增失败";
            }

            return Json(msg, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult DeleteBankCardById(int id)
        {
            IBankCardService bcs = new BankCardService(Settings.Default.db);

            bool deleteBankCardResult = bcs.DeleteById(id);

            ResultMessage msg = new ResultMessage() { Success = deleteBankCardResult };

            if (deleteBankCardResult)
            {
                msg.Content = "删除成功";
            }
            else
            {
                msg.Content = "删除失败";
            }

            return Json(msg, JsonRequestBehavior.DenyGet);
        }

        //可以考虑 写入家庭成员的Controller
        [HttpPost]
        public JsonResult CreateFamily(string[] family)
        {
            FamilyMemeber fm = new FamilyMemeber();

            fm.memberName = family[0];
            fm.familyMemberType = family[1];
            fm.birthday = Convert.ToDateTime(family[2]);
            fm.staffNr = family[3];

            IFamilyMemberService fms = new FamilyMemberService(Settings.Default.db);

            FamilyMemeber familyReturn = fms.CreateFromAjax(fm);

            ResultMessage msg;

            if (familyReturn != null)
            {
                msg = new ResultMessage() { Success = true };
                //将ID返回给前端用来删除
                msg.Content = familyReturn.id.ToString();
            }
            else
            {
                msg = new ResultMessage() { Success = false };
                msg.Content = "新增失败";
            }

            return Json(msg, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult DeleteFamilyById(int id)
        {
            IFamilyMemberService fms = new FamilyMemberService(Settings.Default.db);

            bool deleteFamilyResult = fms.DeleteById(id);

            ResultMessage msg = new ResultMessage() { Success = deleteFamilyResult };

            if (deleteFamilyResult)
            {
                msg.Content = "删除成功";
            }
            else
            {
                msg.Content = "删除失败";
            }

            return Json(msg, JsonRequestBehavior.DenyGet);
        }

        //上传图片
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
        public ActionResult Import()
        {
            var ff = Request.Files[0];
            string fileName = Helpers.FileHelper.SaveAsTmp(ff);
            StaffExcelHelper helper = new StaffExcelHelper(Settings.Default.db, fileName);
            ImportMessage msg = helper.Import();

            //添加"text/html",防止IE 自动下载json 格式返回的数据
            return Json(msg, "text/html");
        }

        [HttpPost]
        public JsonResult changeJob(string[] changeJob)
        {
            //string StaffNr = changeJob[0];
            //int CompanyId = Convert.ToInt16(changeJob[1]);
            //int DepartmentId = Convert.ToInt16(changeJob[2]);
            //int JobTitleId = Convert.ToInt16(changeJob[3]);

            IStaffService ss = new StaffService(Settings.Default.db);
            Staff staff = ss.FindByNr(changeJob[0]);
            string oldCompany = staff.Company == null ? string.Empty : staff.Company.name;
            string oldDepartment = staff.Department == null ? string.Empty : staff.Department.name;
            string oldJobTitle = staff.JobTitle == null ? string.Empty : staff.JobTitle.name;
            string oldJobStr = string.Format("{0}-{1}-{2}", oldCompany, oldDepartment, oldJobTitle);
            bool JobReturn = ss.ChangeJob(changeJob);

            ResultMessage msg;

            if (JobReturn)
            {
                msg = new ResultMessage() { Success = true };
                //将ID返回给前端用来删除
                msg.Content = "调岗成功";

                // 创建调岗记录##User##
                try
                {
                    Staff newStaff = ss.FindByNr(changeJob[0]);
                    string newCompany = newStaff.Company == null ? string.Empty : newStaff.Company.name;
                    string newDepartment = newStaff.Department == null ? string.Empty : newStaff.Department.name;
                    string newJobTitle = newStaff.JobTitle == null ? string.Empty : newStaff.JobTitle.name;
                    string newJobStr = string.Format("{0}-{1}-{2}", newCompany, newDepartment, newJobTitle);
                    IMessageRecordService mrs = new MessageRecordService(Settings.Default.db);

                    mrs.CreateStaffShiftJobMessage(changeJob[0], (Session["user"] as　User).id, oldJobStr, newJobStr);
                }
                catch { }
            }
            else
            {
                msg = new ResultMessage() { Success = false };
                msg.Content = "调岗失败";
            }

            return Json(msg, JsonRequestBehavior.DenyGet);
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
        [UserAuthorize]
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

            // 创建转正记录##User##
            try
            {
                IMessageRecordService mrs = new MessageRecordService(Settings.Default.db);
                mrs.CreateStaffFullMemeberMessage(record.staffNr, (Session["user"] as User).id);
            }
            catch { }
            return Json(msg);
        }


        /// <summary>
        /// 员工离职
        /// </summary>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult Resign(string nr)
        {
            IStaffService ss = new StaffService(Settings.Default.db);
            Staff staff = ss.FindByNr(nr);
            SetResignTypeList(null);
            return View(staff);
        }

        /// <summary>
        /// 执行员工离职
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DoResign([Bind(Include = "resignTypeId, staffNr, resignAt,resignChecker,remark")] ResignRecord record)
        {
            //用户创建员工离职记录:
            //•	离职原因（选择，不可空）
            //•	离职日期（选择，不可空）
            //•	离职批准人（输入，可空）
            //•	备注（输入，可空）
            //如果离职记录创建成功，则将人员在职状态改为：离职

            ResultMessage msg = new ResultMessage();

            IResignRecordService ss = new ResignRecordService(Settings.Default.db);
            msg = ss.Create(record);

            if (msg.Success)
            {
                IStaffService staffSi = new StaffService(Settings.Default.db);
                Staff staff = staffSi.FindByNr(record.staffNr);

                staff.workStatus = (int)WorkStatus.OffWork;

                staffSi.Update(staff);
            }

            // 创建离职记录##User##
            try
            {
                IMessageRecordService mrs = new MessageRecordService(Settings.Default.db);
                mrs.CreateStaffResignMessage(record.staffNr, (Session["user"] as User).id);
            }
            catch { }
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

        private void SetResignTypeList(int? type, bool allowBlank = true)
        {
            IResignTypeService cs = new ResignTypeService(Settings.Default.db);

            ResignTypeSearchModel csm = new ResignTypeSearchModel();

            List<ResignType> certType = cs.Search(csm).ToList();

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
            ViewData["resignTypeList"] = select;
        }
    }
}
