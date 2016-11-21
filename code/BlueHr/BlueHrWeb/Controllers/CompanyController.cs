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
    }
}
