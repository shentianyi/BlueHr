using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
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

namespace BlueHrWeb.Controllers
{
    public class ExtraWorkRecordController : Controller
    {
        // GET: ExtraWorkRecord
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            ExtraWorkRecordSearchModel q = new ExtraWorkRecordSearchModel();

            IExtraWorkRecordService ss = new ExtraWorkRecordService(Settings.Default.db);

            IPagedList<ExtraWorkRecord> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            ExtraWorkRecordInfoModel info = ss.GetExtraWorkRecordInfo(q);
            ViewBag.Info = info;

            return View(models);
        }

        public ActionResult Search([Bind(Include = "Name")] ExtraWorkRecordSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IExtraWorkRecordService ss = new ExtraWorkRecordService(Settings.Default.db);

            IPagedList<ExtraWorkRecord> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", models);
        }

        // GET: ExtraWorkRecord/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExtraWorkRecord/Create
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: ExtraWorkRecord/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "extraWorkTypeId,staffNr, duration,durationType,otReason")] ExtraWorkRecord model)
        {
            try
            {
                // TODO: Add insert logic here 

                IExtraWorkRecordService cs = new ExtraWorkRecordService(Settings.Default.db);
                 
                //model.absenceDate = HttpContext.Request.Form["absenceDate"];
                cs.Create(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: ExtraWorkRecord/Edit/5
        public ActionResult Edit(int id)
        {
            IExtraWorkRecordService cs = new ExtraWorkRecordService(Settings.Default.db);

            ExtraWorkRecord jt = cs.FindById(id);
            SetDropDownList(jt);
            return View(jt);
        }

        // POST: ExtraWorkRecord/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, extraWorkTypeId,staffNr, duration,durationType,otReason")] ExtraWorkRecord model)
        {
            try
            {
                // TODO: Add update logic here
                IExtraWorkRecordService cs = new ExtraWorkRecordService(Settings.Default.db);

                bool updateResult = cs.Update(model);
                if (!updateResult)
                {
                    SetDropDownList(model);
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: ExtraWorkRecord/Delete/5
        public ActionResult Delete(int id)
        {
            IExtraWorkRecordService cs = new ExtraWorkRecordService(Settings.Default.db);

            ExtraWorkRecord cp = cs.FindById(id);
            SetDropDownList(cp);
            return View(cp);
        }

        // POST: ExtraWorkRecord/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                IExtraWorkRecordService cs = new ExtraWorkRecordService(Settings.Default.db);
                cs.DeleteById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void SetDropDownList(ExtraWorkRecord model)
        {
            if (model != null)
            {
                SetExtraWorkTypeList(model.extraWorkTypeId);
                SetDurationTypeCodeList(model.durationType);
            }
            else
            {
                SetExtraWorkTypeList(null);
                SetDurationTypeCodeList(null);
            }
        }

        private void SetExtraWorkTypeList(int? type, bool allowBlank = true)
        {
            IExtraWorkTypeService cs = new ExtraWorkTypeService(Settings.Default.db);

           
            List<ExtraWorkType> certType = cs.All();

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var certt in certType)
            {
                if (type.HasValue && type.ToString().Equals(certt.id))
                {
                    select.Add(new SelectListItem { Text = certt.name, Value = certt.id.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = certt.name, Value = certt.id.ToString(), Selected = false });
                }
            }
            ViewData["extraWorkTypeList"] = select;
        }

        private void SetDurationTypeCodeList(int? model, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(DurationType));

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var it in item)
            {
                if (model.HasValue && model.ToString().Equals(it.Value))
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
                }
            }
            ViewData["durationTypeList"] = select;
        }
    }
}
