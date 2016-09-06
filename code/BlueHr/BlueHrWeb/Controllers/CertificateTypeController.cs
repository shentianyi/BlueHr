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
    public class CertificateTypeController : Controller
    {
        // GET: CertificateType
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            CertificateTypeSearchModel q = new CertificateTypeSearchModel();

            ICertificateTypeService ss = new CertificateTypeService(Settings.Default.db);

            IPagedList<CertificateType> certfs = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            CertificateTypeInfoModel info = ss.GetCertificateTypeInfo(q);
            ViewBag.Info = info;

            return View(certfs);
        }

        public ActionResult Search([Bind(Include = "Name")] CertificateTypeSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            ICertificateTypeService ss = new CertificateTypeService(Settings.Default.db);

            IPagedList<CertificateType> certfs = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", certfs);
        }


        // GET: CertificateType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CertificateType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CertificateType/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name, remark,isNecessary,isSystem")] CertificateType certf)
        {
            try
            {
                // TODO: Add insert logic here  
                ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);

                cs.Create(certf);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CertificateType/Edit/5
        public ActionResult Edit(int id)
        {
            ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);

            CertificateType certf = cs.FindById(id);

            return View(certf);
        }

        // POST: CertificateType/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, remark,isNecessary,isSystem")] CertificateType certf)
        {
            try
            {
                // TODO: Add update logic here
                ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);
                cs.Update(certf);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CertificateType/Delete/5
        public ActionResult Delete(int id)
        {
            ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);

            CertificateType cp = cs.FindById(id);

            return View(cp);
        }

        // POST: CertificateType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);
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
