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
    public class SysRoleAuthorizationController : Controller
    {
        // GET: SysRoleAuthorization
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {

            SetSysRoleList(false);

            int pageIndex = PagingHelper.GetPageIndex(page);

            SysRoleAuthorizationSearchModel q = new SysRoleAuthorizationSearchModel();

            ISysRoleAuthorizationService ss = new SysRoleAuthorizationService(Settings.Default.db);

            IPagedList<SysAuthorization> jobTitles = ss.SearchByRoleAndAuth(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SysAuthorizationInfoModel info = ss.GetSysAuthorizationInfo(q);
            ViewBag.Info = info;

            //bind existList

            ISysRoleService cs = new SysRoleService(Settings.Default.db);
            SysRoleSearchModel csm = new SysRoleSearchModel();
            List<SysRole> sysRoleList = cs.Search(csm).ToList();

            List<string> AuthList = new List<string>();
            string thRoleAuthList = "";

            if (sysRoleList != null && sysRoleList.Count > 0)
            {
                List<SysRoleAuthorization> roleAuths = ss.GetSysRoleAuthListByRoleName(sysRoleList.FirstOrDefault().id.ToString());

                roleAuths.ForEach(p =>
                {
                    thRoleAuthList += p.authId + ",";
                    AuthList.Add(p.authId.ToString());
                }); 
            }

            ViewBag.TheRoleAuthList = thRoleAuthList;
            ViewBag.TheAuthList = AuthList;

            return View(jobTitles);
        }

       // [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "RoleName,AuthName")] SysRoleAuthorizationSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            ISysRoleAuthorizationService ss = new SysRoleAuthorizationService(Settings.Default.db);

            IPagedList<SysAuthorization> jobTitles = ss.SearchByRoleAndAuth(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            SetSysRoleList(false);

            ViewBag.Query = q;

            //bind existList
            List<SysRoleAuthorization> roleAuths = ss.GetSysRoleAuthListByRoleName(q.RoleName);
            string thRoleAuthList = "";
            List<string> AuthList = new List<string>();

            roleAuths.ForEach(p =>
            {
                thRoleAuthList += p.authId + ",";
                AuthList.Add(p.authId.ToString());
            });

            ViewBag.TheRoleAuthList = thRoleAuthList;
            ViewBag.TheAuthList = AuthList;

            return View("Index", jobTitles);
        }

        // GET: SysRoleAuthorization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [RoleAndDataAuthorizationAttribute]

        // GET: SysRoleAuthorization/Create
        public ActionResult Create()
        {
            return View();
        }

        [RoleAndDataAuthorizationAttribute]
        // POST: SysRoleAuthorization/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [RoleAndDataAuthorizationAttribute]
        // GET: SysRoleAuthorization/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [RoleAndDataAuthorizationAttribute]
        // POST: SysRoleAuthorization/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [RoleAndDataAuthorizationAttribute]

        // GET: SysRoleAuthorization/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [RoleAndDataAuthorizationAttribute]
        // POST: SysRoleAuthorization/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void SetSysRoleList(bool allowBlank = false)
        {
            //ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);
            ISysRoleService cs = new SysRoleService(Settings.Default.db);

            SysRoleSearchModel csm = new SysRoleSearchModel();

            List<SysRole> sysRoleList = cs.Search(csm).ToList();

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            //select.Add(new SelectListItem { Text = certt.name, Value = certt.id.ToString(), Selected = false });

            foreach (var sysRole in sysRoleList)
            {
                select.Add(new SelectListItem { Text = sysRole.name, Value = sysRole.id.ToString() });
            }

            ViewData["SysRoleList"] = select;
        }

        //给角色添加或删除权限
        [HttpPost]
        public JsonResult AddOrUpdateRoleAuths(string roleId, string authList)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                //check the parameters
                if (string.IsNullOrEmpty(roleId))
                {
                    msg.Success = false;
                    msg.Content = "角色不能为空！";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }

                //if (string.IsNullOrEmpty(authList))
                //{
                //    msg.Success = false;
                //    msg.Content = "权限不能为空！";

                //    return Json(msg, JsonRequestBehavior.AllowGet);
                //}

                ISysRoleAuthorizationService cs = new SysRoleAuthorizationService(Settings.Default.db);

                //获取角色已有的权限
                List<SysRoleAuthorization> roleAuths = cs.GetSysRoleAuthListByRoleName(roleId);

                //角色重新选择的权限列表
                List<string> userRoleSel = authList.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                List<SysRoleAuthorization> sysRoleAuthList = new List<SysRoleAuthorization>();
                List<string> delRoleAuthList = new List<string>();

                //删除
                roleAuths.ForEach(m =>
                {
                    bool isDel = !authList.Contains(m.roleId.ToString());

                    delRoleAuthList.Add(m.id.ToString());
                });

                //添加
                userRoleSel.ForEach(p =>
                {
                    bool isExist = roleAuths.Where(k => k.roleId.ToString() == p).Count() > 0;

                    if (!isExist)
                    {
                        SysRoleAuthorization tmp = new SysRoleAuthorization();
                        tmp.authId = int.Parse(p);
                        tmp.remarks = "";
                        tmp.roleId = int.Parse(roleId);

                        sysRoleAuthList.Add(tmp);
                    }
                });

                delRoleAuthList.ForEach(p =>
                {
                    cs.DeleteById(int.Parse(p));
                });

                bool isSucceed = cs.Creates(sysRoleAuthList);

                msg.Success = isSucceed;
                msg.Content = "添加成功！";

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg.Success = false;
                msg.Content = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
