using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.CustomAttributes;
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
    public class SysAuthorizationController : Controller
    {
        // GET: SystemAuthorization
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            SysAuthorizationSearchModel q = new SysAuthorizationSearchModel();

            ISysAuthorizationService ss = new SysAuthorizationService(Settings.Default.db);

            IPagedList<SysAuthorization> jobTitles = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SysAuthorizationInfoModel info = ss.GetSysAuthorizationInfo(q);
            ViewBag.Info = info;

            return View(jobTitles);
        }

        [RoleAndDataAuthorizationAttribute]

        public ActionResult Search([Bind(Include = "Name,funCode,controlName,actionName")] SysAuthorizationSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            ISysAuthorizationService ss = new SysAuthorizationService(Settings.Default.db);

            IPagedList<SysAuthorization> jobTitles = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", jobTitles);
        }

        // GET: SystemAuthorization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [RoleAndDataAuthorizationAttribute]
        // GET: SystemAuthorization/Create
        public ActionResult Create()
        {
            return View();
        }

        [RoleAndDataAuthorizationAttribute]
        // POST: SystemAuthorization/Create
        [HttpPost]
        public JsonResult Create([Bind(Include = "name,controlName,actionName,isDelete,parentId,funCode,remarks")] SysAuthorization jobTitle)
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
                    ISysAuthorizationService cs = new SysAuthorizationService(Settings.Default.db);

                    //if (!string.IsNullOrEmpty(Request.Form["jobCertificateType"]))
                    //{
                    //    //拼接Job Certificate Type
                    //    string jobCerts = this.HttpContext.Request.Form["jobCertificateType"];

                    //    jobCerts.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(p =>
                    //    {
                    //        jobTitle.JobCertificate.Add(new JobCertificate()
                    //        {
                    //            certificateTypeId = int.Parse(p),
                    //            jobTitleId = jobTitle.id
                    //        });
                    //    });
                    //}

                    jobTitle.isDelete = 0;

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

        // GET: SystemAuthorization/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            ISysAuthorizationService cs = new SysAuthorizationService(Settings.Default.db);
            SysAuthorization jt = cs.FindById(id);
            return View(jt);
        }

        // POST: SystemAuthorization/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id,name,controlName,actionName,isDelete,parentId,funCode,remarks")] SysAuthorization jobTitle)
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
                    //string jobCerts = this.HttpContext.Request.Form["jobCertificateType"];

                    ISysAuthorizationService cs = new SysAuthorizationService(Settings.Default.db);
                    bool isSucceed = cs.Update(jobTitle);

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

        // GET: SystemAuthorization/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            ISysAuthorizationService cs = new SysAuthorizationService(Settings.Default.db);
            SysAuthorization cp = cs.FindById(id);
            return View(cp);
        }

        // POST: SystemAuthorization/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                ////存在员工时不可删除
                //IStaffService shfSi = new StaffService(Settings.Default.db);
                //List<Staff> shf = shfSi.FindByJobTitleId(id);

                //if (null != shf && shf.Count() > 0)
                //{
                //    msg.Success = false;
                //    msg.Content = "职位信息正在使用,不能删除!";

                //    return Json(msg, JsonRequestBehavior.AllowGet);
                //}
                //else
                {
                    ISysAuthorizationService cs = new SysAuthorizationService(Settings.Default.db);
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

        public ResultMessage DoValidation(SysAuthorization model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.name))
            {
                msg.Success = false;
                msg.Content = "权限名称不能为空";

                return msg;
            }

            ISysAuthorizationService cs = new SysAuthorizationService(Settings.Default.db);
            List<SysAuthorization> shift = cs.GetAll();

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
    }
}
