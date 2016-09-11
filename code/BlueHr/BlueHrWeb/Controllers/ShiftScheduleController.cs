using BlueHrLib.Data;
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
    public class ShiftScheduleController : Controller
    {
        // GET: ShiftShedule
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            ShiftScheduleSearchModel q = new ShiftScheduleSearchModel();

            IShiftScheduleService ss = new ShiftSheduleService(Settings.Default.db);

            IPagedList<ShiftSchedule> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            ShiftScheduleInfoModel info = ss.GetShiftScheduleInfo(q);
            ViewBag.Info = info;

            return View(models);
        }

        public ActionResult Search([Bind(Include = "Name")]  ShiftScheduleSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IShiftScheduleService ss = new ShiftSheduleService(Settings.Default.db);

            IPagedList<ShiftSchedule> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", models);
        }

        // GET: ShiftShedule/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShiftShedule/Create
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: ShiftShedule/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "shiftId,staffNr,scheduleAt")] ShiftSchedule model)
        {
            try
            {
                // TODO: Add insert logic here 

                IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);

                //model.absenceDate = HttpContext.Request.Form["absenceDate"];
                cs.Create(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShiftShedule/Edit/5
        public ActionResult Edit(int id)
        {
            IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);

            ShiftSchedule jt = cs.FindById(id);
            SetDropDownList(jt);
            return View(jt);
        }

        // POST: ShiftShedule/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,shiftId,staffNr,scheduleAt")] ShiftSchedule model)
        {
            try
            {
                // TODO: Add update logic here
                IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);

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

        // GET: ShiftShedule/Delete/5
        public ActionResult Delete(int id)
        {
            IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);

            ShiftSchedule cp = cs.FindById(id);
            SetDropDownList(cp);
            return View(cp);
        }

        // POST: ShiftShedule/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);
                cs.DeleteById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void SetDropDownList(ShiftSchedule model)
        {
            if (model != null)
            {
                SetShiftScheduleList(model.shiftId);
             }
            else
            {
                SetShiftScheduleList(null);
             }
        }

        private void SetShiftScheduleList(int? type, bool allowBlank = true)
        {
            IShiftService cs = new ShiftService(Settings.Default.db);

            ShiftSearchModel csm = new ShiftSearchModel();

            List<Shift> certType = cs.Search(csm).ToList();

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
            ViewData["shiftList"] = select;
        }

        //private void SetDurationTypeCodeList(int? model, bool allowBlank = true)
        //{
        //    List<EnumItem> item = EnumHelper.GetList(typeof(DurationType));

        //    List<SelectListItem> select = new List<SelectListItem>();

        //    if (allowBlank)
        //    {
        //        select.Add(new SelectListItem { Text = "", Value = "" });
        //    }

        //    foreach (var it in item)
        //    {
        //        if (model.HasValue && model.ToString().Equals(it.Value))
        //        {
        //            select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
        //        }
        //        else
        //        {
        //            select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
        //        }
        //    }
        //    ViewData["durationTypeList"] = select;
        //}
    }
}
