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

namespace BlueHrWeb.Controllers
{
    public class DegreeTypeController : Controller
    {
        // GET: DegreeType
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            DegreeTypeSearchModel q = new DegreeTypeSearchModel();

            IDegreeTypeService ss = new DegreeTypeService(Settings.Default.db);

            IPagedList<DegreeType> degreeTypes = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            DegreeTypeInfoModel info = ss.GetDegreeTypeInfo(q);
            ViewBag.Info = info;

            return View(degreeTypes);
        }

        public ActionResult Search([Bind(Include = "Name")] DegreeTypeSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IDegreeTypeService ss = new DegreeTypeService(Settings.Default.db);

            IPagedList<DegreeType> degreeTypes = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", degreeTypes);
        }

        // GET: DegreeType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DegreeType/Create
        public ActionResult Create()
        {
            return View();
        } 
         
        // POST: DegreeType/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name, remark")] DegreeType degreeType)
        {
            try
            {
                // TODO: Add insert logic here
                IDegreeTypeService cs = new DegreeTypeService(Settings.Default.db);

                cs.Create(degreeType);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DegreeType/Edit/5
        public ActionResult Edit(int id)
        {
            IDegreeTypeService cs = new DegreeTypeService(Settings.Default.db);

            DegreeType dgt = cs.FindById(id);

            return View(dgt);
        }
        
        // POST: DegreeType/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, name, remark")] DegreeType degreeType)
        {
            try
            {
                // TODO: Add update logic here
                IDegreeTypeService cs = new DegreeTypeService(Settings.Default.db);
                cs.Update(degreeType);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DegreeType/Delete/5
        public ActionResult Delete(int id)
        {
            IDegreeTypeService cs = new DegreeTypeService(Settings.Default.db);

            DegreeType dgt = cs.FindById(id);

            return View(dgt);
         }

        // POST: DegreeType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                IDegreeTypeService cs = new DegreeTypeService(Settings.Default.db);
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
