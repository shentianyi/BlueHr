using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.PageViewModel;
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
    public class CertificateController : Controller
    {
        // GET: Certificate
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            CertificateSearchModel q = new CertificateSearchModel();

            ICertificateService ss = new CertificateService(Settings.Default.db);

            IPagedList<Certificate> certfs = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            CertificateInfoModel info = ss.GetCertificateInfo(q);
            ViewBag.Info = info;

            //get staff info
            string staffNr = this.Request.QueryString["nr"];
            IStaffService sts = new StaffService(Settings.Default.db);

            Staff stfModel = sts.FindByNr(staffNr);

            ViewBag.StfModel = stfModel;


            return View(certfs);
        }

        public ActionResult Search([Bind(Include = "staffNr")] CertificateSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            ICertificateService ss = new CertificateService(Settings.Default.db);

            IPagedList<Certificate> certfs = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", certfs);
        }

        // GET: Certificate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Certificate/Create
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: Certificate/Create
        [HttpPost]
        //[staffNr]--编号
        //[certificateTypeId]--•	证照类别（选择，不可空）
        //[certiLevel]--•	级别（输入，可空）
        //[effectiveFrom]--•	开始有效期（选择，日期，可空）
        //[effectiveEnd]--•	截止有效期（选择，日期，可空）
        //[institution]--•	发证单位（输入，可空），
        //[remark]--•	备注（输入，可空）
        public ActionResult Create([Bind(Include = "staffNr,certificateTypeId, certiLevel,effectiveFrom,effectiveEnd,institution,remark")] Certificate model)
        {
            try
            {
                // TODO: Add insert logic here 

                ICertificateService cs = new CertificateService(Settings.Default.db);

                cs.Create(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Certificate/Edit/5
        public ActionResult Edit(int id)
        {
            ICertificateService cs = new CertificateService(Settings.Default.db);

            Certificate jt = cs.FindById(id);
            SetDropDownList(jt);
            return View(jt);
        }

        // POST: Certificate/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,staffNr,certificateTypeId, certiLevel,effectiveFrom,effectiveEnd,institution,remark")] Certificate model)
        {
            try
            {
                // TODO: Add update logic here
                ICertificateService cs = new CertificateService(Settings.Default.db);

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

        // GET: Certificate/Delete/5
        public ActionResult Delete(int id)
        {
            ICertificateService cs = new CertificateService(Settings.Default.db);

            Certificate cp = cs.FindById(id);
            SetDropDownList(cp);
            return View(cp);
        }

        // POST: Certificate/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                ICertificateService cs = new CertificateService(Settings.Default.db);
                cs.DeleteById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void SetDropDownList(Certificate model)
        {
            if (model != null)
            {
                SetCertificateTypeList(model.id);
            }
            else
            {
                SetCertificateTypeList(null);
            }
        }

        //bind CertificateType
        private void SetCertificateTypeList(int? type, bool allowBlank = true)
        {
            ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);

            CertificateTypeSearchModel csm = new CertificateTypeSearchModel();

            List<CertificateType> certType = cs.Search(csm).ToList();

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
            ViewData["certificateTypeList"] = select;
        }

        //上传图片
        public ActionResult uploadImage()
        {
            var ff = Request.Files[0];

            string fileName = Helpers.FileHelper.SaveUploadImage(ff);
            ResultMessage msg = new ResultMessage() { Success = true };
            msg.Content = fileName;
            //防止IE直接下载json数据
            return Json(msg, "text/html");
            // return Json(fileName, JsonRequestBehavior.DenyGet);
        }

    }
}
