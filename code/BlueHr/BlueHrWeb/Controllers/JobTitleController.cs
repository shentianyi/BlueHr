using BlueHrLib.Data;
using BlueHrLib.Data.Message;
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
    public class JobTitleController : Controller
    {
        // GET: JobTitle 
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            JobTitleSearchModel q = new JobTitleSearchModel();

            IJobTitleService ss = new JobTitleService(Settings.Default.db);

            IPagedList<JobTitle> jobTitles = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            JobTitleInfoModel info = ss.GetJobTitleInfo(q);
            ViewBag.Info = info;

            return View(jobTitles);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "Name")] JobTitleSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IJobTitleService ss = new JobTitleService(Settings.Default.db);

            IPagedList<JobTitle> jobTitles = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", jobTitles);
        }

        // GET: JobTitle/Details/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JobTitle/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: JobTitle/Create
        [HttpPost]
        [RoleAndDataAuthorizationAttribute]
        public JsonResult Create([Bind(Include = "Name,remark,jobCertificateType")] JobTitle jobTitle)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(jobTitle);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IJobTitleService cs = new JobTitleService(Settings.Default.db);

                    if (!string.IsNullOrEmpty(Request.Form["jobCertificateType"]))
                    {
                        //拼接Job Certificate Type
                        string jobCerts = this.HttpContext.Request.Form["jobCertificateType"];

                        jobCerts.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(p =>
                         {
                             jobTitle.JobCertificate.Add(new JobCertificate()
                             {
                                 certificateTypeId = int.Parse(p),
                                 jobTitleId = jobTitle.id
                             });
                         });
                    }
                    bool isSucceed = cs.Create(jobTitle);

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

        // GET: JobTitle/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IJobTitleService cs = new JobTitleService(Settings.Default.db);
            JobTitle jt = cs.FindById(id);
            SetDropDownList(jt);
            return View(jt);
        }

        // POST: JobTitle/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id, name, remark,jobCertificateType")] JobTitle jobTitle)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(jobTitle);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string jobCerts = this.HttpContext.Request.Form["jobCertificateType"];

                    IJobTitleService cs = new JobTitleService(Settings.Default.db);
                    bool isSucceed = cs.Update(jobTitle, jobCerts);

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

        // GET: JobTitle/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IJobTitleService cs = new JobTitleService(Settings.Default.db);
            JobTitle cp = cs.FindById(id);
            SetDropDownList(cp);
            return View(cp);
        }

        // POST: JobTitle/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                //存在员工时不可删除
                IStaffService shfSi = new StaffService(Settings.Default.db);
                List<Staff> shf = shfSi.FindByJobTitleId(id);

                if (null != shf && shf.Count() > 0)
                {
                    msg.Success = false;
                    msg.Content = "职位信息正在使用,不能删除!";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IJobTitleService cs = new JobTitleService(Settings.Default.db);
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

        [HttpPost]
        //4.1	职位管理
        //（列表（分页）、新建、编辑、删除（存在员工时不可删除）
        //）：名称（不可空），备注（可空），职位需要的证照类别（多个，可空）

        public ResultMessage DoValidation(JobTitle model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.name))
            {
                msg.Success = false;
                msg.Content = "职位名称不能为空";

                return msg;
            }

            IJobTitleService cs = new JobTitleService(Settings.Default.db);
            List<JobTitle> shift = cs.GetAll();

            if (model.id <= 0)
            {
                bool isRecordExists = shift.Where(p => p.name == model.name).ToList().Count() > 0;

                if (isRecordExists)
                {
                    msg.Success = false;
                    msg.Content = "数据已经存在!";

                    return msg;
                }
            }
            else
            {
                bool isRecordExists = shift.Where(p => p.name == model.name && p.id != model.id).ToList().Count() > 0;

                if (isRecordExists)
                {
                    msg.Success = false;
                    msg.Content = "数据已经存在!";

                    return msg;
                }
            }

            return new ResultMessage() { Success = true, Content = "" };
        }


        private void SetDropDownList(JobTitle model)
        {
            if (model != null)
            {

                SetJobCertificateTypeList(model.JobCertificate.ToList());
            }
            else
            {
                SetJobCertificateTypeList(null);
            }
        }

        private void SetJobCertificateTypeList(List<JobCertificate> jobCertis, bool allowBlank = false)
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
                if (jobCertis != null)
                {
                    bool hasSelected = jobCertis.Where(k => k.certificateTypeId == certt.id).ToList().Count() > 0;

                    if (hasSelected)
                    {
                        select.Add(new SelectListItem { Text = certt.name, Value = certt.id.ToString(), Selected = true });
                    }
                    else
                    {
                        select.Add(new SelectListItem { Text = certt.name, Value = certt.id.ToString(), Selected = false });
                    }
                }
                else
                {
                    select.Add(new SelectListItem { Text = certt.name, Value = certt.id.ToString(), Selected = false });
                }
            }

            ViewData["jobCertificateTypeList"] = select;
        }
    }
}
