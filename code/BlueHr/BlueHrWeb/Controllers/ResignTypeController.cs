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
            SetDropDownList(null);
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
            SetDropDownList(jt);
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
               

                bool updateResult = cs.Update(resignType);
                if (!updateResult)
                {
                    SetDropDownList(resignType);
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

        // GET: ResignType/Delete/5
        public ActionResult Delete(int id)
        {
            IResignTypeService cs = new ResignTypeService(Settings.Default.db);

            ResignType cp = cs.FindById(id);
            SetDropDownList(cp);
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

        private void SetDropDownList(ResignType model)
        {
            if (model != null)
            {
                SetResignTypeCodeList(model.code);
            }
            else
            {
                SetResignTypeCodeList(null);
            }
        }

        private void SetResignTypeCodeList(string model, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(SystemCertificateType));

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var it in item)
            {
                if (!string.IsNullOrEmpty(model) && model.ToString().Equals(it.Value))
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
                }
            }
            ViewData["resignTypeCodeList"] = select;
        }
    }
}
