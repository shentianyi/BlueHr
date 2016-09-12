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

using BlueHrWeb.CustomAttributes;
namespace BlueHrWeb.Controllers
{
    public class AbsenceRecrodController : Controller
    {
        // GET: AbsenceRecrod
        [UserAuthorize]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            AbsenceRecrodSearchModel q = new AbsenceRecrodSearchModel();

            IAbsenceRecordService ss = new AbsenceRecordService(Settings.Default.db);

            IPagedList<AbsenceRecrod> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            AbsenceRecrodInfoModel info = ss.GetAbsenceRecrodInfo(q);
            ViewBag.Info = info;

            return View(models);
        }

        public ActionResult Search([Bind(Include = "Name")] AbsenceRecrodSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IAbsenceRecordService ss = new AbsenceRecordService(Settings.Default.db);

            IPagedList<AbsenceRecrod> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", models);
        }

        // GET: AbsenceRecrod/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AbsenceRecrod/Create
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: AbsenceRecrod/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "absenceTypeId,staffNr, duration,durationType,remark,absenceDate")] AbsenceRecrod model)
        {
            try
            {
                // TODO: Add insert logic here 

                IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);
              
                //model.absenceDate = HttpContext.Request.Form["absenceDate"];
                cs.Create(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AbsenceRecrod/Edit/5
        public ActionResult Edit(int id)
        {
            IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);

            AbsenceRecrod jt = cs.FindById(id);
            SetDropDownList(jt);
            return View(jt);
        }

        // POST: AbsenceRecrod/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, absenceTypeId,staffNr, duration,durationType,remark,absenceDate")] AbsenceRecrod model)
        {
            try
            {
                // TODO: Add update logic here
                IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);

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

        // GET: AbsenceRecrod/Delete/5
        public ActionResult Delete(int id)
        {
            IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);

            AbsenceRecrod cp = cs.FindById(id);
            SetDropDownList(cp);
            return View(cp);
        }

        // POST: AbsenceRecrod/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);
                cs.DeleteById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void SetDropDownList(AbsenceRecrod model)
        {
            if (model != null)
            {
                SetAbsenceTypeList(model.absenceTypeId);
                SetDurationTypeCodeList(model.durationType);
            }
            else
            {
                SetAbsenceTypeList(null);
                SetDurationTypeCodeList(null);
            }
        }

        private void SetAbsenceTypeList(int? type, bool allowBlank = true)
        {
            IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);

            AbsenceTypeSearchModel csm = new AbsenceTypeSearchModel();

            List<AbsenceType> certType = cs.Search(csm).ToList();

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
            ViewData["absenceTypeList"] = select;
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
 
