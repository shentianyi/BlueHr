using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.Helpers;
using BlueHrWeb.Properties;
using Brilliantech.Framwork.Utils.LogUtil;
using MvcPaging;
using BlueHrWeb.CustomAttributes;
using BlueHrLib.Helper.Excel;
using BlueHrLib.Data.Message;
using BlueHrLib.Helper;
using BlueHrLib.Data.Enum;

namespace BlueHrWeb.Controllers
{
    public class DepartmentController : Controller
    {

        IDepartmentService departmentService = new DepartmentService(Settings.Default.db);

        #region 部门列表

        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);
            DepartmentSearchModel q = new DepartmentSearchModel();
            IPagedList<Department> departments = departmentService.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SetCompanyList(null);

            return View(departments);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        #endregion

        #region 创建部门


        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            //读取所有公司列表
            SetCompanyList(null, false);
            SetDepartmentList(null,null);
            return View();
        }

        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create(FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                var companyId = collection["CompanyId"];
                var departmentName = collection["name"];
                var departmentRemark = collection["remark"];

                var department = new Department()
                {
                    name = departmentName,
                    remark = departmentRemark,
                    companyId = int.Parse(companyId)
                    
                };
               if(!string.IsNullOrEmpty(collection["parentId"])  ){
                    department.parentId = int.Parse(collection["parentId"]);
                }
                bool isSucceed = this.departmentService.Create(department);

                SetCompanyList(null, false);
                msg.Success = isSucceed;
                msg.Content = isSucceed ? "添加成功" : "添加失败";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SetCompanyList(null,false);
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 编辑部门

        // GET: Department/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            var department = this.departmentService.FindById(id);

            SetCompanyList(department.companyId, false);

            SetDepartmentList(department.companyId, department.parentId);
            return View(department);
        }

        [HttpPost]
        [RoleAndDataAuthorizationAttribute]
        public JsonResult Edit(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                var department = this.departmentService.FindById(id);
                if (department != null)
                {
                    department.name = collection["name"];
                    department.remark = collection["remark"];

                    if (!string.IsNullOrEmpty(collection["parentId"]) ) {
                        department.parentId = int.Parse(collection["parentId"]);
                    }
                    bool isSucceed = this.departmentService.Update(department);
                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "编辑成功" : "编辑失败";
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                return Json(new ResultMessage() { Success = false, Content = "编辑失败" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion

        #region 删除部门

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            var department = this.departmentService.FindById(id);
            SetCompanyList(department.companyId, false);
            SetDepartmentList(department.companyId, department.parentId);

            return View(department);
        }

        [HttpPost]
        //[RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            { 

                var department = this.departmentService.FindById(id);
                if (null != department)
                {
                    bool isSucceed = this.departmentService.DeleteById(id);
                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "删除成功" : "删除失败";
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                //IStaffService staffService = new StaffService(Settings.Default.db);
                //TODO:如果该部门下有员工，则不能删除该部门

                msg.Success = false;
                msg.Content = "删除失败";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = "该部门下已有员工， 不可删除" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        public ActionResult Search([Bind(Include = "Name, CompanyId")] DepartmentSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IDepartmentService ds = new DepartmentService(Settings.Default.db);

            IPagedList<Department> departments = ds.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SetCompanyList(q.CompanyId);

            return View("Index", departments);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Import()
        {
            var ff = Request.Files[0];
            string fileName = BlueHrWeb.Helpers.FileHelper.SaveAsTmp(ff);
            DepartmentExcelHelper helper = new DepartmentExcelHelper(Settings.Default.db, fileName);
            ImportMessage msg = helper.Import();

            //添加"text/html",防止IE 自动下载json 格式返回的数据
            return Json(msg, "text/html");
        }

        [RoleAndDataAuthorization]
        [UserAuthorize]
        public ActionResult TreeShow()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DepartmentTree(int companyId)
        {
            List<Dictionary<string, string>> Result = new List<Dictionary<string, string>>();

            IDepartmentService ds = new DepartmentService(Settings.Default.db);
            List<Department> departments = ds.FindByCompanyId(companyId).ToList();

            foreach (var department in departments)
            {
                Dictionary<string, string> dp = new Dictionary<string, string>();
                dp.Add("id", department.id.ToString());
                dp.Add("name", department.name);
                dp.Add("pId", department.parentId.ToString());
                dp.Add("remark", department.remark);
                dp.Add("open", "false");
                //父节点和子节点图标不同
                if (department.parentId.HasValue)
                {
                    dp.Add("iconSkin", "bankIcon");
                }
                else
                {
                    dp.Add("iconSkin", "parentBankIcon");
                }

                Result.Add(dp);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
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

        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IDepartmentService at = new DepartmentService(Settings.Default.db);

            var Department = at.GetAllTableName();

            if (Department != null)
            {
                //获取当前记录的属性
                foreach (var property in Department[0].GetType().GetProperties())
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
