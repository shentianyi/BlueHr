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
    public class RecruitController : Controller
    {
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            RecruitSearchModel q = new RecruitSearchModel();

            IRecruitService rs = new RecruitService(Settings.Default.db);

            IPagedList<Recruit> Recruit = rs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            RecruitInfoModel info = rs.GetRecruitInfo(q);
            ViewBag.Info = info;

            return View(Recruit);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "requirement,companyId,departmentId,amount")] RecruitSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IRecruitService raps = new RecruitService(Settings.Default.db);

            IPagedList<Recruit> Recruit = raps.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", Recruit);
        }


        // GET: Recruit/Details/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Recruit/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: Recruit/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "requirement,companyId,departmentId,amount,createAt,requirementAt,requirementMan,status,auditRecord,auditView")] Recruit Recruit)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(Recruit);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IRecruitService raps = new RecruitService(Settings.Default.db);
                    bool isSucceed = raps.Create(Recruit);
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

        // GET: Recruit/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IRecruitService cs = new RecruitService(Settings.Default.db);

            Recruit Recruit = cs.FindById(id);

            SetDropDownList(Recruit);

            return View(Recruit);
        }

        // POST: Recruit/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "requirement,companyId,departmentId,amount,createAt,requirementAt,requirementMan,status,auditRecord,auditView")] Recruit Recruit)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(Recruit);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IRecruitService raps = new RecruitService(Settings.Default.db);
                    bool isSucceed = raps.Update(Recruit);

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

        // GET: Recruit/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IRecruitService raps = new RecruitService(Settings.Default.db);

            Recruit Recruit = raps.FindById(id);
            SetDropDownList(Recruit);
            return View(Recruit);
        }

        // POST: Recruit/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();
            try
            {
                IRecruitService raps = new RecruitService(Settings.Default.db);
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

        private void SetDropDownList(Recruit Recruit)
        {
            if (Recruit != null)
            {
                SetCompanyList(Recruit.companyId);
                SetDepartmentList(Recruit.companyId, Recruit.departmentId);
            }
            else
            {
                SetCompanyList(null);
                SetDepartmentList(null, null);
            }
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

        public ResultMessage DoValidation(Recruit model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.requirement))
            {
                msg.Success = false;
                msg.Content = "需求不能为空";
                return msg;
            }
            
            return new ResultMessage() { Success = true, Content = "" };
        }


    }
}
