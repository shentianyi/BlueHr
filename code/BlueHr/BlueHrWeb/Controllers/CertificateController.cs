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
using BlueHrWeb.CustomAttributes;

namespace BlueHrWeb.Controllers
{
    public class CertificateController : Controller
    {
        // GET: Certificate
        [UserAuthorize]
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
                        model.Attachments.Add(new Attachment()
                        {
                            attachmentAbleId = null,
                            attachmentType = -1,
                            certificateId = model.id,
                            attachmentAbleType = "",
                            name = p.Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[0],
                            path = "/UploadCertificate/" + model.staffNr + "/" + p.Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[1]
                        });
                    });

                    bool isSucceed = cs.Create(model);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "添加失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
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
                        model.Attachments.Add(new Attachment()
                        {
                            attachmentAbleId = null,
                            attachmentType = -1,
                            certificateId = model.id,
                            attachmentAbleType = "",
                            name = p.Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[0],
                            path = "/UploadCertificate/" + model.staffNr + "/" + p.Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[1]
                        });
                    });

                    bool isSucceed = cs.Update(model);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "更新失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
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
        public JsonResult Delete(int id, FormCollection collection)
        {  
            ResultMessage msg = new ResultMessage();

            try
            {

                //IAbsenceRecordService shfSi = new AbsenceRecordService(Settings.Default.db);
                //List<AbsenceRecrod> shf = shfSi.FindByAbsenceType(id);

                //if (null != shf && shf.Count() > 0)
                //{
                //    msg.Success = false;
                //    msg.Content = "缺勤类型正在使用,不能删除!";

                //    return Json(msg, JsonRequestBehavior.AllowGet);
                //}
                //else
                {
                    ICertificateService cs = new CertificateService(Settings.Default.db);
                    bool isSucceed = cs.DeleteById(id);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "删除失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
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





            //AddAttachmentRecord(staffNr, fileName);

            //防止IE直接下载json数据
            return Json(msg, "text/html");
        }

        [HttpPost]
        //[staffNr]--编号
        //[certificateTypeId]--•	证照类别（选择，不可空）
        //[certiLevel]--•	级别（输入，可空）
        //[effectiveFrom]--•	开始有效期（选择，日期，可空）
        //[effectiveEnd]--•	截止有效期（选择，日期，可空）
        //[institution]--•	发证单位（输入，可空），
        //[remark]--•	备注（输入，可空）
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

        //public bool AddAttachmentRecord(string staffNr, string fileName)
        //{
        //    Attachment attach = new Attachment();
        //    attach.attachmentAbleId = null;
        //    attach.attachmentAbleType = "";
        //    attach.attachmentType = 1;
        //    attach.certificateId = -1;
        //    attach.name = fileName;
        //    attach.path = staffNr + fileName;

        //    //IAttachmentService si = new AttachmentService(Settings.Default.db);

        //    //bool isSucceed = si.Create(attach);

        //    return isSucceed;
        //}

        //public Certificate CreateTempRecord(string staffNr)
        //{
        //    Certificate ct = new Certificate();
        //    ct.certificateTypeId = -1;
        //    ct.certiLevel = 1;
        //    ct.effectiveEnd = DateTime.Now;
        //    ct.effectiveFrom = DateTime.Now;
        //    ct.institution = "";
        //    ct.remark = "";
        //    ct.staffNr = staffNr;
        //}
    }
}
