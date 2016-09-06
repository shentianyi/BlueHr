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
    public class JobTitleController : Controller
    {
        // GET: JobTitle 
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            JobTitleSearchModel q = new JobTitleSearchModel();

            IJobTitleService ss = new JobTitleService(Settings.Default.db);

            IPagedList<JobTitle> jobTitles = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            JobTitleInfoModel info = ss.GetJobTitleInfo(q);
            ViewBag.Info = info;

            return View(jobTitles);
        }

        public ActionResult Search([Bind(Include = "Name")] JobTitleSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IJobTitleService ss = new JobTitleService(Settings.Default.db);

            IPagedList<JobTitle> jobTitles = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", jobTitles);
        }

        // GET: JobTitle/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JobTitle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobTitle/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name, remark")] JobTitle jobTitle)
        {
            try
            {
                // TODO: Add insert logic here 

                IJobTitleService cs = new JobTitleService(Settings.Default.db);

                cs.Create(jobTitle);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JobTitle/Edit/5
        public ActionResult Edit(int id)
        {
            IJobTitleService cs = new JobTitleService(Settings.Default.db);

            JobTitle jt = cs.FindById(id);

            return View(jt);
        }

        // POST: JobTitle/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, name, remark")] JobTitle jobTitle)
        {
            try
            {
                // TODO: Add update logic here
                IJobTitleService cs = new JobTitleService(Settings.Default.db);
                cs.Update(jobTitle);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JobTitle/Delete/5
        public ActionResult Delete(int id)
        {
            IJobTitleService cs = new JobTitleService(Settings.Default.db);

            JobTitle cp = cs.FindById(id);

            return View(cp);
        }

        // POST: JobTitle/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                IJobTitleService cs = new JobTitleService(Settings.Default.db);
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
