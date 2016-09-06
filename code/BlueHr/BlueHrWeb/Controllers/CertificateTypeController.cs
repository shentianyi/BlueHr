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
            SetDropDownList(null);
            return View();
        }

        // POST: CertificateType/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,remark,isSystem,isNecessary,systemCode")] CertificateType certf)
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

            SetDropDownList(certf);

            return View(certf);
        }

        // POST: CertificateType/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,Name, remark,isSystem,isNecessary,systemCode")] CertificateType certf)
        {
            try
            {
                // TODO: Add update logic here
                ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);
                bool updateResult = cs.Update(certf);
                if (!updateResult)
                {
                    SetDropDownList(certf);
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
             }
            catch(Exception ex)
            {
                SetDropDownList(null);
                return View();
            }
        }

        // GET: CertificateType/Delete/5
        public ActionResult Delete(int id)
        {
            ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);

            CertificateType certf = cs.FindById(id);
            SetDropDownList(certf);
            return View(certf);
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

        private void SetDropDownList(CertificateType certf)
        {
            if (certf != null)
            {
                SetSystemCertificateTypeList(certf.systemCode);
                SetIsSystemList(certf.isSystem);
                SetIsNecessaryList(certf.isNecessary);
            }
            else
            {
                //SetIsOnTrialList(false);
                //SetSexList(null);
                //SetJobTitleList(null);
                //SetCompanyList(null);
                //SetDepartmentList(null, null);
                //SetStaffTypeList(null);
                //SetDegreeTypeList(null);
                //SetInSureTypeList(null);
                //SetIsPayCPFList(false);
                //SetResidenceTypeList(0);
                //SetWorkStatusList(100);
                SetSystemCertificateTypeList(100);
                SetIsSystemList(null);
                SetIsNecessaryList(null);
            }
        }

        private void SetSystemCertificateTypeList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(SystemCertificateType));

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
            ViewData["systemCertificateTypeList"] = select;
        }

        private void SetIsSystemList(bool? type, bool allowBlank = true)
        {
            List<EnumItem> item = new List<EnumItem>() { new EnumItem() { Text = "是", Value = "true" }, new EnumItem() { Text = "否", Value = "false" } };
            //EnumHelper.GetList(typeof(IsOnTrail));

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
            ViewData["isSystemList"] = select;
        }

        //isNecessaryList
        private void SetIsNecessaryList(bool? type, bool allowBlank = true)
        {
            List<EnumItem> item = new List<EnumItem>() { new EnumItem() { Text = "是", Value = "true" }, new EnumItem() { Text = "否", Value = "false" } };
            //EnumHelper.GetList(typeof(IsOnTrail));

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
            ViewData["isNecessaryList"] = select;
        }
    }
}
