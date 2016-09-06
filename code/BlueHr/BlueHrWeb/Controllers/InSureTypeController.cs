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
    public class InSureTypeController : Controller
    {
        // GET: InSureType
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            InSureTypeSearchModel q = new InSureTypeSearchModel();

            IInSureTypeService ss = new InSureTypeService(Settings.Default.db);

            IPagedList<InsureType> insureTypes = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            InSureTypeInfoModel info = ss.GetInsureTypeInfo(q);
            ViewBag.Info = info;

            return View(insureTypes);
        }

        public ActionResult Search([Bind(Include = "Name")] InSureTypeSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IInSureTypeService ss = new InSureTypeService(Settings.Default.db);

            IPagedList<InsureType> insureTypes = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", insureTypes);
        }

        // GET: InSureType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InSureType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InSureType/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name, remark")] InsureType insureType)
        {
            try
            {
                // TODO: Add insert logic here 

                IInSureTypeService cs = new InSureTypeService(Settings.Default.db);

                cs.Create(insureType);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: InSureType/Edit/5
        public ActionResult Edit(int id)
        {
            IInSureTypeService cs = new InSureTypeService(Settings.Default.db);

            InsureType jt = cs.FindById(id);
            return View(jt);
        }

        // POST: InSureType/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, name, remark")] InsureType insureType)
        {
            try
            {
                // TODO: Add update logic here
                IInSureTypeService cs = new InSureTypeService(Settings.Default.db);
                cs.Update(insureType);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: InSureType/Delete/5
        public ActionResult Delete(int id)
        {
            IInSureTypeService cs = new InSureTypeService(Settings.Default.db);

            InsureType cp = cs.FindById(id);
            return View(cp);
        }

        // POST: InSureType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                IInSureTypeService cs = new InSureTypeService(Settings.Default.db);
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
