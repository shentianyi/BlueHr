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
    public class AbsenceTypeController : Controller
    {
        // GET: AbsenceType
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            AbsenceTypeSearchModel q = new AbsenceTypeSearchModel();

            IAbsenceTypeService ss = new AbsenceTypeService(Settings.Default.db);

            IPagedList<AbsenceType> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            AbsenceTypeInfoModel info = ss.GetAbsenceTypeInfo(q);
            ViewBag.Info = info;

            return View(models);
        }

        public ActionResult Search([Bind(Include = "Name")] AbsenceTypeSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IAbsenceTypeService ss = new AbsenceTypeService(Settings.Default.db);

            IPagedList<AbsenceType> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", models);
        }

        // GET: AbsenceType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AbsenceType/Create
        public ActionResult Create()
        {
            //SetDropDownList(null);
            return View();
        }

        // POST: AbsenceType/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,code, remark")] AbsenceType model)
        {
            try
            {
                // TODO: Add insert logic here 

                IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);

                cs.Create(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AbsenceType/Edit/5
        public ActionResult Edit(int id)
        {
            IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);

            AbsenceType jt = cs.FindById(id);
            //SetDropDownList(jt);
            return View(jt);
        }

        // POST: AbsenceType/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, name,code, remark")] AbsenceType model)
        {
            try
            {
                // TODO: Add update logic here
                IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);

                bool updateResult = cs.Update(model);
                if (!updateResult)
                {
                    //SetDropDownList(model);
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

        // GET: AbsenceType/Delete/5
        public ActionResult Delete(int id)
        {
            IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);

            AbsenceType cp = cs.FindById(id);
            //SetDropDownList(cp);
            return View(cp);
        }

        // POST: AbsenceType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);
                cs.DeleteById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        //private void SetDropDownList(AbsenceType model)
        //{
        //    if (model != null)
        //    {
        //        SetAbsenceTypeCodeList(model.code);
        //    }
        //    else
        //    {
        //        SetAbsenceTypeCodeList(null);
        //    }
        //}

        //private void SetAbsenceTypeCodeList(string model, bool allowBlank = true)
        //{
        //    List<EnumItem> item = EnumHelper.GetList(typeof(SystemCertificateType));

        //    List<SelectListItem> select = new List<SelectListItem>();

        //    if (allowBlank)
        //    {
        //        select.Add(new SelectListItem { Text = "", Value = "" });
        //    }

        //    foreach (var it in item)
        //    {
        //        if (!string.IsNullOrEmpty(model) && model.ToString().Equals(it.Value))
        //        {
        //            select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
        //        }
        //        else
        //        {
        //            select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
        //        }
        //    }
        //    ViewData["absenceTypeCodeList"] = select;
        //}

    }
}
