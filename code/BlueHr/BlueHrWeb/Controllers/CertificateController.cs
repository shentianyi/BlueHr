﻿using BlueHrLib.Data;
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
using BlueHrWeb.CustomAttributes;
using System.IO;
using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;

namespace BlueHrWeb.Controllers
{
    public class CertificateController : Controller
    {
        // GET: Certificate
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            CertificateSearchModel q = new CertificateSearchModel();

            ICertificateService ss = new CertificateService(Settings.Default.db);
            q.StaffActNr = this.Request.QueryString["nr"];
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

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "StaffActNr")] CertificateSearchModel q)
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
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            ViewBag.staffNr = Request.QueryString["nr"];
            SetDropDownList(null);
            return View();
        }

        // POST: Certificate/Create
        [HttpPost]
        [RoleAndDataAuthorizationAttribute]
        public JsonResult Create([Bind(Include = "staffNr,certificateTypeId, certiLevel,effectiveFrom,effectiveEnd,institution,remark")] Certificate model)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(model);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ICertificateService cs = new CertificateService(Settings.Default.db);

                    //上传文件路径列表|| ;
                    string theAttachments = this.HttpContext.Request.Form["athment"];

                    theAttachments.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(p =>
                    {
                        var tmpNames = p.Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        if (tmpNames.Length > 0)
                        {
                            model.Attachments.Add(new Attachment()
                            {
                                attachmentAbleId = null,
                                attachmentType = -1,
                                certificateId = model.id,
                                attachmentAbleType = "", 
                                name = tmpNames[0], 
                                path = "/UploadCertificate/" + model.staffNr + "/" + tmpNames[1]
                            });
                        }
                    });

                    bool isSucceed = cs.Create(model);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "添加成功" : "添加失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Certificate/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            ViewBag.staffNr = Request.QueryString["nr"];
            ICertificateService cs = new CertificateService(Settings.Default.db);

            Certificate jt = cs.FindById(id);
            SetDropDownList(jt);
            return View(jt);
        }

        // POST: Certificate/Edit/5
        [HttpPost]
        [RoleAndDataAuthorizationAttribute]
        public JsonResult Edit([Bind(Include = "id,staffNr,certificateTypeId, certiLevel,effectiveFrom,effectiveEnd,institution,remark")] Certificate model)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(model);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ICertificateService cs = new CertificateService(Settings.Default.db);

                    //上传文件路径列表|| ;
                    string theAttachments = this.HttpContext.Request.Form["athment"];

                    theAttachments.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(p =>
                    {
                        var tmpNames = p.Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); 

                        if (tmpNames.Length > 0)
                        {
                            model.Attachments.Add(new Attachment()
                            {
                                attachmentAbleId = null,
                                attachmentType = -1,
                                certificateId = model.id,
                                attachmentAbleType = "",
                                name = tmpNames[0],
                                path = "/UploadCertificate/" + model.staffNr + "/" + tmpNames[1]
                            });
                        }
                    });

                    //删除文件
                    string atchDelIds = this.HttpContext.Request.Form["atchDelIds"];

                    bool isSucceed = cs.Update(model, atchDelIds);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "更新成功" : "更新失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Certificate/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            ICertificateService cs = new CertificateService(Settings.Default.db);

            Certificate cp = cs.FindById(id);
            SetDropDownList(cp);
            return View(cp);
        }

        // POST: Certificate/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                ICertificateService cs = new CertificateService(Settings.Default.db);
                bool isSucceed = cs.DeleteById(id);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "删除成功" : "删除失败";

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [RoleAndDataAuthorization]
        [UserAuthorize]
        [HttpGet]
        public JsonResult GetCertificateByStaffNr(string staffNr)
        {
            List<Dictionary<string, string>> Result = new List<Dictionary<string, string>>();
            try
            {
                ICertificateService cs = new CertificateService(Settings.Default.db);
                List<Certificate> certificates = cs.FindByStaffNr(staffNr);

                foreach(var certificate in certificates)
                {
                    Dictionary<string, string> ctf = new Dictionary<string, string>();
                    ctf.Add("id", certificate.id.ToString());
                    ctf.Add("staffNr", certificate.staffNr);
                    ctf.Add("certificateName", certificate.CertificateType.name);
                    ctf.Add("certiLevel", certificate.certiLevel);
                    ctf.Add("effectiveFrom", certificate.effectiveFrom.Value.ToString("yyyy-MM-dd"));
                    ctf.Add("effectiveEnd", certificate.effectiveEnd.Value.ToString("yyyy-MM-dd"));
                    ctf.Add("institution", certificate.institution);
                    ctf.Add("remark", certificate.remark);

                    Result.Add(ctf);
                }

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(Result, JsonRequestBehavior.AllowGet);
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
            string staffNr = Request.Form["staffNr"];
            var ff = Request.Files[0];
            string orginFileName = ff.FileName;

            string fileName = Helpers.FileHelper.SaveUploadCertificate(ff, staffNr);
            ResultMessage msg = new ResultMessage() { Success = true };
            msg.Content = orginFileName + "|" + fileName;

            //防止IE直接下载json数据
            return Json(msg, "text/html");
        }

        public ActionResult DownFile(string fileName, string filePath)
        {
            filePath = Server.MapPath(filePath);
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/octet-stream";

            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileName));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            return new EmptyResult();

        }

        [HttpPost]
        //证照类别（选择，不可空）
        public ResultMessage DoValidation(Certificate model)
        {
            ResultMessage msg = new ResultMessage();

            if (model.certificateTypeId <= 0)
            {
                msg.Success = false;
                msg.Content = "证照类别不能为空";

                return msg;
            }

            //ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);
            //List<CertificateType> shift = cs.GetAll();

            //if (model.id <= 0)
            //{
            //    bool isRecordExists = shift.Where(p => p.name == model.name).ToList().Count() > 0;

            //    if (isRecordExists)
            //    {
            //        msg.Success = false;
            //        msg.Content = "数据已经存在!";

            //        return msg;
            //    }
            //}
            //else
            //{
            //    bool isSystem = shift.Where(p => p.isSystem && p.id == model.id).ToList().Count() > 0;

            //    if (isSystem)
            //    {
            //        msg.Success = false;
            //        msg.Content = "系统级别不可编辑";

            //        return msg;
            //    }

            //    bool isRecordExists = shift.Where(p => p.name == model.name && p.id != model.id).ToList().Count() > 0;

            //    if (isRecordExists)
            //    {
            //        msg.Success = false;
            //        msg.Content = "数据已经存在!";

            //        return msg;
            //    }
            //}

            return new ResultMessage() { Success = true, Content = "" };
        }

        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            ICertificateService at = new CertificateService(Settings.Default.db);

            var Certificate = at.GetAllTableName();

            if (Certificate != null)
            {
                //获取当前记录的属性
                foreach (var property in Certificate[0].GetType().GetProperties())
                {
                    select.Add(new SelectListItem { Text = property.Name, Value = property.Name });
                }
            }

            ViewData["getAllTableNameList"] = select;
        }

        private void SetSearchConditions(bool? type, bool allowBlank = false)
        {
            var item = EnumHelper.GetList(typeof(SearchConditions));

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
            ViewData["searchConditionsList"] = select;
        }
    }
}
