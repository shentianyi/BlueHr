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
    public class ShiftController : Controller
    {
        // GET: Shift
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            ShiftSearchModel q = new ShiftSearchModel();

            IShiftService ss = new ShiftService(Settings.Default.db);

            IPagedList<Shift> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            ShiftInfoModel info = ss.GetShiftInfo(q);
            ViewBag.Info = info;

            return View(models);
        }

        public ActionResult Search([Bind(Include = "Name")]  ShiftSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IShiftService ss = new ShiftService(Settings.Default.db);

            IPagedList<Shift> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", models);
        }


        // GET: Shift/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Shift/Create
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: Shift/Create 
        [HttpPost]
        public ActionResult Create([Bind(Include = "code,name,startAt,endAt,shiftType,remark")] Shift model)
        {
            try
            {
                // TODO: Add insert logic here 

                IShiftService cs = new ShiftService(Settings.Default.db);

                //model.absenceDate = HttpContext.Request.Form["absenceDate"];
                cs.Create(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shift/Edit/5
        public ActionResult Edit(int id)
        {
            IShiftService cs = new ShiftService(Settings.Default.db);

            Shift jt = cs.FindById(id);
            SetDropDownList(jt);
            return View(jt);
        }

        // POST: Shift/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,code,name,startAt,endAt,shiftType,remark")] Shift model)
        {
            try
            {
                // TODO: Add update logic here
                IShiftService cs = new ShiftService(Settings.Default.db);

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

        // GET: Shift/Delete/5
        public ActionResult Delete(int id)
        {
            IShiftService cs = new ShiftService(Settings.Default.db);

            Shift cp = cs.FindById(id);
            SetDropDownList(cp);
            return View(cp);
        }

        // POST: Shift/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                IShiftService cs = new ShiftService(Settings.Default.db);
                cs.DeleteById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void SetDropDownList(Shift model)
        {
            if (model != null)
            {
                SetShiftList(model.shiftType);
            }
            else
            {
                SetShiftList(null);
            }
        }

        private void SetShiftList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(ShiftType));

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var it in item)
            {
                if (type.HasValue && type.ToString().Equals(it.Value))
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
                }
            }
            ViewData["shiftTypeList"] = select;
        }
    }
}
