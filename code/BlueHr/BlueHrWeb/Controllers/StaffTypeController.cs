using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
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

namespace BlueHrWeb.Controllers
{
    public class StaffTypeController : Controller
    {
        // GET: StaffType
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            StaffTypeSearchModel q = new StaffTypeSearchModel();

            IStaffTypeService ss = new StaffTypeService(Settings.Default.db);

            IPagedList<StaffType> staffTypes = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            StaffTypeInfoModel info = ss.GetStaffTypeInfo(q);
            ViewBag.Info = info;

            return View(staffTypes);
        }

        public ActionResult Search([Bind(Include = "Name")] StaffTypeSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IStaffTypeService ss = new StaffTypeService(Settings.Default.db);

            IPagedList<StaffType> staffTypes = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", staffTypes);
        }

        // GET: StaffType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StaffType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffType/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name, remark")] StaffType staffType)
        {
            try
            {
                // TODO: Add insert logic here
                IStaffTypeService cs = new StaffTypeService(Settings.Default.db);

                cs.Create(staffType);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StaffType/Edit/5
        public ActionResult Edit(int id)
        {
            IStaffTypeService cs = new StaffTypeService(Settings.Default.db);

            StaffType dgt = cs.FindById(id);

            return View(dgt);
        }

        // POST: StaffType/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, name, remark")] StaffType staffType)
        {
            try
            {
                // TODO: Add update logic here
                IStaffTypeService cs = new StaffTypeService(Settings.Default.db);
                cs.Update(staffType);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StaffType/Delete/5
        public ActionResult Delete(int id)
        {
            IStaffTypeService cs = new StaffTypeService(Settings.Default.db);

            StaffType dgt = cs.FindById(id);

            return View(dgt);
        }

        // POST: StaffType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                IStaffTypeService cs = new StaffTypeService(Settings.Default.db);
                cs.DeleteById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
