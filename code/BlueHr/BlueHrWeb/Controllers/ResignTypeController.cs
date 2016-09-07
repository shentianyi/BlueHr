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
    public class ResignTypeController : Controller
    {
        // GET: ResignType
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            ResignTypeSearchModel q = new ResignTypeSearchModel();

            IResignTypeService ss = new ResignTypeService(Settings.Default.db);

            IPagedList<ResignType> resignTypes = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            ResignTypeInfoModel info = ss.GetResignTypeInfo(q);
            ViewBag.Info = info;

            return View(resignTypes);
        }

        public ActionResult Search([Bind(Include = "Name")] ResignTypeSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IResignTypeService ss = new ResignTypeService(Settings.Default.db);

            IPagedList<ResignType> resignTypes = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", resignTypes);
        }

        // GET: ResignType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ResignType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResignType/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,code, remark")] ResignType resignType)
        {
            try
            {
                // TODO: Add insert logic here 

                IResignTypeService cs = new ResignTypeService(Settings.Default.db);

                cs.Create(resignType);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ResignType/Edit/5
        public ActionResult Edit(int id)
        {
            IResignTypeService cs = new ResignTypeService(Settings.Default.db);

            ResignType jt = cs.FindById(id);

            return View(jt);
        }

        // POST: ResignType/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, name,code, remark")] ResignType resignType)
        {
            try
            {
                // TODO: Add update logic here
                IResignTypeService cs = new ResignTypeService(Settings.Default.db);
                cs.Update(resignType);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ResignType/Delete/5
        public ActionResult Delete(int id)
        {
            IResignTypeService cs = new ResignTypeService(Settings.Default.db);

            ResignType cp = cs.FindById(id);

            return View(cp);
        }

        // POST: ResignType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                IResignTypeService cs = new ResignTypeService(Settings.Default.db);
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
