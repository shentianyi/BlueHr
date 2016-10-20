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
    public class SysRoleController : Controller
    {
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        // GET: SysRole
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            SysRoleSearchModel q = new SysRoleSearchModel();

            ISysRoleService ss = new SysRoleService(Settings.Default.db);

            IPagedList<SysRole> jobTitles = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SysRoleInfoModel info = ss.GetSysRoleInfo(q);
            ViewBag.Info = info;

            return View(jobTitles);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "Name")] SysRoleSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            ISysRoleService ss = new SysRoleService(Settings.Default.db);

            IPagedList<SysRole> jobTitles = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", jobTitles);
        }

        [RoleAndDataAuthorizationAttribute]
        // GET: SysRole/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [RoleAndDataAuthorizationAttribute]
        // GET: SysRole/Create
        public ActionResult Create()
        {
            return View();
        }

        [RoleAndDataAuthorizationAttribute]
        // POST: SysRole/Create
        [HttpPost]
        public JsonResult Create([Bind(Include = "name,remarks")] SysRole jobTitle)
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
                    ISysRoleService cs = new SysRoleService(Settings.Default.db);

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

        [RoleAndDataAuthorizationAttribute]
        // GET: SysRole/Edit/5
        public ActionResult Edit(int id)
        {
            ISysRoleService cs = new SysRoleService(Settings.Default.db);
            SysRole jt = cs.FindById(id);
            return View(jt);
        }

        [RoleAndDataAuthorizationAttribute]
        // POST: SysRole/Edit/5
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id, name, remarks")] SysRole jobTitle)
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

                    ISysRoleService cs = new SysRoleService(Settings.Default.db);
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

        [RoleAndDataAuthorizationAttribute]
        // GET: SysRole/Delete/5
        public ActionResult Delete(int id)
        {
            ISysRoleService cs = new SysRoleService(Settings.Default.db);
            SysRole cp = cs.FindById(id);
            return View(cp);
        }

        [RoleAndDataAuthorizationAttribute]
        // POST: SysRole/Delete/5
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
                    ISysRoleService cs = new SysRoleService(Settings.Default.db);
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

        public ResultMessage DoValidation(SysRole model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.name))
            {
                msg.Success = false;
                msg.Content = "角色名称不能为空";

                return msg;
            }

            ISysRoleService cs = new SysRoleService(Settings.Default.db);
            List<SysRole> shift = cs.GetAll();

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
