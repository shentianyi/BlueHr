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
    public class RewardsAndPenaltyController : Controller
    {
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            RewardsAndPenaltySearchModel q = new RewardsAndPenaltySearchModel();

            IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);

            IPagedList<RewardsAndPenalty> rewardsAndPenalties = raps.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            RewardsAndPenaltyInfoModel info = raps.GetRewardsAndPenaltyInfo(q);
            ViewBag.Info = info;

            return View(rewardsAndPenalties);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "Name")] RewardsAndPenaltySearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);

            IPagedList<RewardsAndPenalty> rewardsAndPenalties = raps.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", rewardsAndPenalties);
        }


        // GET: RewardsAndPenalty/Details/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RewardsAndPenalty/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: RewardsAndPenalty/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "staffNr,staffName,staffSex,companyId,departmentId,type,project,description,createAt,userId")] RewardsAndPenalty rewardsAndPenalties)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(rewardsAndPenalties);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);
                    bool isSucceed = raps.Create(rewardsAndPenalties);
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

        // GET: RewardsAndPenalty/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IRewardsAndPenaltyService cs = new RewardsAndPenaltyService(Settings.Default.db);

            RewardsAndPenalty rewardsAndPenalties = cs.FindById(id);

            SetDropDownList(rewardsAndPenalties);

            return View(rewardsAndPenalties);
        }

        // POST: RewardsAndPenalty/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "nr,name,sex,companyId,departmentId,type,project,description,datetime,approver")] RewardsAndPenalty rewardsAndPenalties)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(rewardsAndPenalties);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);
                    bool isSucceed = raps.Update(rewardsAndPenalties);

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

        // GET: RewardsAndPenalty/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);

            RewardsAndPenalty rewardsAndPenalties = raps.FindById(id);
            SetDropDownList(rewardsAndPenalties);
            return View(rewardsAndPenalties);
        }

        // POST: RewardsAndPenalty/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();
            try
            {
                IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);
                bool isSucceed = raps.DeleteById(id);
                msg.Success = isSucceed;
                msg.Content = isSucceed ? "" : "删除失败";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private void SetDropDownList(RewardsAndPenalty rewardsAndPenalties)
        {
            if (rewardsAndPenalties != null)
            {
                SetSexList(rewardsAndPenalties.staffSex);
                SetCompanyList(rewardsAndPenalties.companyId);
                SetDepartmentList(rewardsAndPenalties.companyId, rewardsAndPenalties.departmentId);
                SetTypeList(rewardsAndPenalties.type);
            }
            else
            {
                SetSexList(null);
                SetCompanyList(null);
                SetDepartmentList(null, null);
                SetTypeList(null);
            }
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
        private void SetTypeList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(RewardsAndPenaltyType));

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
            ViewData["rewardsAndPenaltiesTypeList"] = select;
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
        [HttpPost]

        public ResultMessage DoValidation(RewardsAndPenalty model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.staffNr))
            {
                msg.Success = false;
                msg.Content = "员工号不能为空";
                return msg;
            }
            if (string.IsNullOrEmpty(model.staffName))
            { 
                msg.Success = false;
                msg.Content = "员工姓名不能为空";
                return msg;
            }
            if (model.staffSex==null)
            {
                msg.Success = false;
                msg.Content = "员工性别不能为空";
                return msg;
            }
            if (model.type == null)
            {
                msg.Success = false;
                msg.Content = "奖惩类别不能为空";
                return msg;
            }
            return new ResultMessage() { Success = true, Content = "" };
        }

       
    }
}
