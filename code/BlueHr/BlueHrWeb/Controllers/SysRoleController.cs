using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Helper;
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
        public ActionResult Index()
        {
            return View();
        }

        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult TableShow(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            SysRoleSearchModel q = new SysRoleSearchModel();

            ISysRoleService ss = new SysRoleService(Settings.Default.db);

            IPagedList<SysRole> sysRoles = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SysRoleInfoModel info = ss.GetSysRoleInfo(q);
            ViewBag.Info = info;

            return View(sysRoles);
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

            return View("TableShow", jobTitles);
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
        public JsonResult Create([Bind(Include = "name,remarks")] SysRole sysRole)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(sysRole);

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
                    //        sysRole.JobCertificate.Add(new JobCertificate()
                    //        {
                    //            certificateTypeId = int.Parse(p),
                    //            jobTitleId = sysRole.id
                    //        });
                    //    });
                    //}
                    bool isSucceed = cs.Create(sysRole);

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
        public JsonResult Edit([Bind(Include = "id, name, remarks")] SysRole sysRole)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(sysRole);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //string jobCerts = this.HttpContext.Request.Form["jobCertificateType"];

                    ISysRoleService cs = new SysRoleService(Settings.Default.db);
                    bool isSucceed = cs.Update(sysRole);

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
                //存在员工时不可删除
                ISysRoleService srs = new SysRoleService(Settings.Default.db);

                IUserService us = new UserService(Settings.Default.db);

                List<User> user = us.FindByRoleId(id);

                if (user !=null && user.Count() > 0)
                {
                    msg.Success = false;
                    msg.Content = "角色正在使用中,不能删除!";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    bool isSucceed = srs.DeleteById(id);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "删除成功" : "删除失败";

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


        [RoleAndDataAuthorizationAttribute]
        // GET: SysRole/Edit/5
        public ActionResult AssignAuth(int? id)
        {
            ISysRoleService cs = new SysRoleService(Settings.Default.db);
            if (id.HasValue)
            {
                SysRole jt = cs.FindById(Convert.ToInt32(id));
                return View(jt);
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public JsonResult SysRoleTree()
        {
            List<Dictionary<string, string>> Result = new List<Dictionary<string, string>>();

            SysRoleSearchModel q = new SysRoleSearchModel();
            ISysRoleService srs = new SysRoleService(Settings.Default.db);
            List<SysRole> sysRoles = srs.Search(q).ToList();

            foreach (var sysRole in sysRoles)
            {
                Dictionary<string, string> sr = new Dictionary<string, string>();
                sr.Add("id", sysRole.id.ToString());
                sr.Add("name", sysRole.name);
                sr.Add("remark", sysRole.remarks);
                sr.Add("open", "true");
                sr.Add("iconSkin", "sysRoleIcon");

                Result.Add(sr);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            ISysRoleService at = new SysRoleService(Settings.Default.db);

            var SysRole = at.GetAllTableName();

            if (SysRole != null)
            {
                //获取当前记录的属性
                foreach (var property in SysRole[0].GetType().GetProperties())
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
