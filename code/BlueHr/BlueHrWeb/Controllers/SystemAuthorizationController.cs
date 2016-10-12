using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueHrWeb.Controllers
{
    public class SystemAuthorizationController : Controller
    {
        // GET: SystemAuthorization
        public ActionResult Index()
        {
            return View();
        }

        // GET: SystemAuthorization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SystemAuthorization/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SystemAuthorization/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SystemAuthorization/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SystemAuthorization/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SystemAuthorization/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SystemAuthorization/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
