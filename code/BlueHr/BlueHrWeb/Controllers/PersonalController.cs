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
using BlueHrLib.Helper;
using BlueHrLib.Data.Enum;

namespace BlueHrWeb.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Application
        // 我的申请 
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Application()
        {
            //可以使用ViewData进行传值
            User user = System.Web.HttpContext.Current.Session["user"] as User;

            IExtraWorkRecordService rwrs = new ExtraWorkRecordService(Settings.Default.db);
            ExtraWorkRecordSearchModel ewrsSearchModel = new ExtraWorkRecordSearchModel();

            List<ExtraWorkRecord> extraWorkRecord = rwrs.Search(ewrsSearchModel).Where(c=>c.userId.Equals(user.id)).ToList();

            //Dictionary<string, string> extraWorks = new Dictionary<string, string>();

            //foreach(var extraWork in extraWorkRecord)
            //{
            //    extraWorks.Add("ID", extraWork.id.ToString());
            //    extraWorks.Add("ExtraWorkType", extraWork.extraWorkTypeId.ToString());
            //    extraWorks.Add("ID", extraWork.id.ToString());
            //    extraWorks.Add("ID", extraWork.id.ToString());
            //    extraWorks.Add("ID", extraWork.id.ToString());
            //    extraWorks.Add("ID", extraWork.id.ToString());
            //    extraWorks.Add("ID", extraWork.id.ToString());
            //    extraWorks.Add("ID", extraWork.id.ToString());
            //    extraWorks.Add("ID", extraWork.id.ToString());
            //}

            ViewData["ExtraWork"] = extraWorkRecord;

            //IFull
            //ViewData["FullMember"] = 


            return View();
        }

        // GET: Approval 
        // 我的审核
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Approval()
        {
            return View();
        }


        // GET: Schedule 
        // 日程安排
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Schedule()
        {
            return View();
        }

        // GET: Note 
        // 便笺本
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Note()
        {
            return View();
        }


        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        [HttpGet]
        public ActionResult GetAll()
        {
            List<Dictionary<string, string>> Result = new List<Dictionary<string, string>>();

            IJobTitleService ss = new JobTitleService(Settings.Default.db);

            foreach (var a in ss.GetAll())
            {
                Dictionary<string, string> one = new Dictionary<string, string>();
                one.Add("id", a.id.ToString());
                one.Add("name", a.name);
                one.Add("remark", a.remark);
                one.Add("IsRevoked", a.IsRevoked.ToString());
                Result.Add(one);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
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

        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IJobTitleService at = new JobTitleService(Settings.Default.db);

            var JobTitle = at.GetAllTableName();

            if (JobTitle != null)
            {
                //获取当前记录的属性
                foreach (var property in JobTitle[0].GetType().GetProperties())
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
