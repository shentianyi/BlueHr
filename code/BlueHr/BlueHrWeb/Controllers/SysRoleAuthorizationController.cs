﻿using BlueHrLib.Data;
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
            SetAllTableName(null);
            SetSearchConditions(null);
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
            SetAllTableName(null);
            SetSearchConditions(null);
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

        /// <summary>
        /// 通过视图进行 判断显示
        /// </summary>
        /// <param name="sysRoleId">角色ID</param>
        /// <returns></returns>
        //[RoleAndDataAuthorizationAttribute]
        //[HttpGet]
        //public JsonResult SysAuthorizationTree(int sysRoleId, int showType)
        //{
        //    Dictionary<string, List<Dictionary<string, string>>> Result = new Dictionary<string, List<Dictionary<string, string>>>();

        //    List<Dictionary<string, string>> Users = new List<Dictionary<string, string>>();
        //    List<Dictionary<string, string>> SysRoleAuths = new List<Dictionary<string, string>>();

        //    ISysRoleAuthViewService sras = new SysRoleAuthViewService(Settings.Default.db);

        //    List<IGrouping<string, SysRoleAuthView>> sysRoleAuthViews = new List<IGrouping<string, SysRoleAuthView>>();

        //    //进行分组显示
        //    if (showType == 1)
        //    {
        //        sysRoleAuthViews = sras.SysRoleAuthViewTree().GroupBy(c => c.actionName).ToList();
        //    }else
        //    {
        //        sysRoleAuthViews = sras.SysRoleAuthViewTree().GroupBy(c => c.funCode).ToList();
        //    }


        //    foreach (var sysRoleAuthView in sysRoleAuthViews)
        //    {
        //        Dictionary<string, string> srav = new Dictionary<string, string>();

        //        if (showType == 1)
        //        {
        //            //可以考虑 进行分组
        //            string NameShow;
        //            if (sysRoleAuthView.Key == "Index")
        //            {
        //                NameShow = "【查】查看权限";
        //            }
        //            else if (sysRoleAuthView.Key == "Create")
        //            {
        //                NameShow = "【增】添加权限";
        //            }
        //            else if (sysRoleAuthView.Key == "Edit")
        //            {
        //                NameShow = "【改】编辑权限";
        //            }
        //            else if (sysRoleAuthView.Key == "Delete")
        //            {
        //                NameShow = "【删】删除权限";
        //            }else if (sysRoleAuthView.Key == "Import")
        //            {
        //                NameShow = "【导】导入权限";
        //            }else if (sysRoleAuthView.Key == "ExceptionList")
        //            {
        //                NameShow = "【异】异常权限";
        //            }else if(sysRoleAuthView.Key == "ApprovalAbsenceRecord")
        //            {
        //                NameShow = "【缺】缺勤审批权限";
        //            }else if(sysRoleAuthView.Key == "ApprovalExtraWorkRecord")
        //            {
        //                NameShow = "【加】加班审批权限";
        //            }
        //            else
        //            {
        //                NameShow = "【他】其他权限";
        //            }

        //            srav.Add("id", sysRoleAuthView.Key + sysRoleAuthView.Count());
        //            srav.Add("name", NameShow + " (共 " + sysRoleAuthView.Count() + " 项)");
        //            srav.Add("Count", sysRoleAuthView.Count().ToString());
        //            srav.Add("iconSkin", "parentSysRoleAuthIcon");
        //            SysRoleAuths.Add(srav);
        //        }
        //        else
        //        {
        //            //可以考虑 进行分组
        //            srav.Add("id", sysRoleAuthView.Key + sysRoleAuthView.Count());
        //            srav.Add("name", sysRoleAuthView.Key + " (共 " + sysRoleAuthView.Count() + " 项)");
        //            srav.Add("Count", sysRoleAuthView.Count().ToString());
        //            srav.Add("iconSkin", "parentSysRoleAuthIcon");
        //            SysRoleAuths.Add(srav);
        //        }

        //        var ssraIndex = 1;
        //        foreach (var ssra in sysRoleAuthView)
        //        {
        //            Dictionary<string, string> tempSRAV = new Dictionary<string, string>();

        //            //如果没值，就是不具备的权限
        //            //如果有值，并且需要 roleId 相等，才可以。
        //            if (ssra.roleId.HasValue)
        //            {
        //                if(ssra.roleId == sysRoleId)
        //                {
        //                    tempSRAV.Add("id", ssra.SysAuthId.ToString());
        //                    tempSRAV.Add("name", "[" + ssraIndex + "] " + ssra.SysAuthName);
        //                    tempSRAV.Add("controlName", ssra.controlName);
        //                    tempSRAV.Add("actionName", ssra.actionName);
        //                    //如果为空的话， 就是最上层？
        //                    tempSRAV.Add("pId", ssra.SysAuthParentId.HasValue ? ssra.SysAuthParentId.ToString() : sysRoleAuthView.Key + sysRoleAuthView.Count());
        //                    tempSRAV.Add("funCode", ssra.funCode);
        //                    tempSRAV.Add("isDelete", ssra.isDelete.ToString());
        //                    tempSRAV.Add("remarks", ssra.SysAuthRemarks);
        //                    tempSRAV.Add("open", "false");
        //                    tempSRAV.Add("iconSkin", "sysRoleAuthIcon");

        //                    if (ssra.roleId == sysRoleId)
        //                    {
        //                        tempSRAV.Add("checked", "true");
        //                    }
        //                    ssraIndex++;
        //                    SysRoleAuths.Add(tempSRAV);
        //                }else
        //                {
        //                    tempSRAV.Add("id", ssra.SysAuthId.ToString());
        //                    tempSRAV.Add("name", "[" + ssraIndex + "] " + ssra.SysAuthName);
        //                    tempSRAV.Add("controlName", ssra.controlName);
        //                    tempSRAV.Add("actionName", ssra.actionName);
        //                    //如果为空的话， 就是最上层？
        //                    tempSRAV.Add("pId", ssra.SysAuthParentId.HasValue ? ssra.SysAuthParentId.ToString() : sysRoleAuthView.Key + sysRoleAuthView.Count());
        //                    tempSRAV.Add("funCode", ssra.funCode);
        //                    tempSRAV.Add("isDelete", ssra.isDelete.ToString());
        //                    tempSRAV.Add("remarks", ssra.SysAuthRemarks);
        //                    tempSRAV.Add("open", "false");
        //                    tempSRAV.Add("iconSkin", "sysRoleAuthIcon");

        //                    if (ssra.roleId == sysRoleId)
        //                    {
        //                        tempSRAV.Add("checked", "true");
        //                    }
        //                    ssraIndex++;
        //                    SysRoleAuths.Add(tempSRAV);
        //                }
        //            }else
        //            {
        //                tempSRAV.Add("id", ssra.SysAuthId.ToString());
        //                tempSRAV.Add("name", "[" + ssraIndex + "] " + ssra.SysAuthName);
        //                tempSRAV.Add("controlName", ssra.controlName);
        //                tempSRAV.Add("actionName", ssra.actionName);
        //                //如果为空的话， 就是最上层？
        //                tempSRAV.Add("pId", ssra.SysAuthParentId.HasValue ? ssra.SysAuthParentId.ToString() : sysRoleAuthView.Key + sysRoleAuthView.Count());
        //                tempSRAV.Add("funCode", ssra.funCode);
        //                tempSRAV.Add("isDelete", ssra.isDelete.ToString());
        //                tempSRAV.Add("remarks", ssra.SysAuthRemarks);
        //                tempSRAV.Add("open", "false");
        //                tempSRAV.Add("iconSkin", "sysRoleAuthIcon");

        //                if (ssra.roleId == sysRoleId)
        //                {
        //                    tempSRAV.Add("checked", "true");
        //                }

        //                ssraIndex++;

        //                SysRoleAuths.Add(tempSRAV);
        //            }
        //        }
        //    }

        //    IUserService us = new UserService(Settings.Default.db);
        //    List<User> users = us.FindByRoleId(sysRoleId);

        //    foreach(var user in users)
        //    {
        //        Dictionary<string, string> urs = new Dictionary<string, string>();
        //        urs.Add("id", user.id.ToString());
        //        urs.Add("name", "[" + user.name + "] [ " + user.email + "]");
        //        urs.Add("email", user.email);
        //        urs.Add("isLockedStr", user.isLockedStr);
        //        urs.Add("role", user.role.ToString());
        //        urs.Add("open", "false");
        //        urs.Add("iconSkin", "userIcon");
        //        Users.Add(urs);
        //    }

        //    Result.Add("Users", Users);
        //    Result.Add("SysRoleAuths", SysRoleAuths);

        //    return Json(Result, JsonRequestBehavior.AllowGet);
        //}

        //[RoleAndDataAuthorizationAttribute]
        //[HttpPost]
        //public JsonResult AssignAuthsToRole(int sysRoleId, List<Dictionary<string, string>> roleAuthArray)
        //{
        //    bool result = false;
        //    try
        //    {
        //        ISysRoleAuthorizationService sras = new SysRoleAuthorizationService(Settings.Default.db);
        //        //先删除全部权限，再新建
        //        if (sras.DeleteByRoleId(sysRoleId))
        //        {
        //            if(roleAuthArray==null)
        //            {
        //                result = true;
        //                return Json(result, JsonRequestBehavior.DenyGet);
        //            }

        //            foreach (var roleAuth in roleAuthArray)
        //            {
        //                //通过判断有没有Count字段 就可以断定 是否是自定义字段 - 头字段  funCode
        //                string HasCount;
        //                roleAuth.TryGetValue("Count", out HasCount);

        //                if (string.IsNullOrWhiteSpace(HasCount))
        //                {
        //                    string authId;
        //                    roleAuth.TryGetValue("id", out authId);

        //                    SysRoleAuthorization sysRoleAuthorization = new SysRoleAuthorization();
        //                    sysRoleAuthorization.roleId = sysRoleId;
        //                    sysRoleAuthorization.authId = Convert.ToInt16(authId);
        //                    sysRoleAuthorization.remarks = "创建时间:" + DateTime.Now;

        //                    result = sras.Create(sysRoleAuthorization);
        //                }
        //            }
        //        }
        //        return Json(result, JsonRequestBehavior.DenyGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(result, JsonRequestBehavior.DenyGet);
        //    }
        //}

        [RoleAndDataAuthorizationAttribute]
        [HttpGet]
        public JsonResult SysAuthorizationTree(int sysRoleId, int showType)
        {
            Dictionary<string, List<Dictionary<string, string>>> Result = new Dictionary<string, List<Dictionary<string, string>>>();

            List<Dictionary<string, string>> Users = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> SysRoleAuths = new List<Dictionary<string, string>>();

            ISysAuthorizationService sas = new SysAuthorizationService(Settings.Default.db);
            SysAuthorizationSearchModel saSearchModel = new SysAuthorizationSearchModel();

            List<IGrouping<string, SysAuthorization>> sysAuthorizations = new List<IGrouping<string, SysAuthorization>>();

            //查询出所有的权限， 进行分组显示
            if (showType == 1)
            {
                sysAuthorizations = sas.Search(saSearchModel).GroupBy(c => c.actionName).ToList();
            }
            else
            {
                sysAuthorizations = sas.Search(saSearchModel).GroupBy(c => c.funCode).ToList();
            }


            foreach (var sysAuthorization in sysAuthorizations)
            {
                Dictionary<string, string> sysA = new Dictionary<string, string>();

                if (showType == 1)
                {
                    //可以考虑 进行分组
                    string NameShow;
                    if (sysAuthorization.Key == "Index")
                    {
                        NameShow = "【查】查看权限";
                    }
                    else if (sysAuthorization.Key == "Create")
                    {
                        NameShow = "【增】添加权限";
                    }
                    else if (sysAuthorization.Key == "Edit")
                    {
                        NameShow = "【改】编辑权限";
                    }
                    else if (sysAuthorization.Key == "Delete")
                    {
                        NameShow = "【删】删除权限";
                    }
                    else if (sysAuthorization.Key == "Import")
                    {
                        NameShow = "【导】导入权限";
                    }
                    else if (sysAuthorization.Key == "ExceptionList")
                    {
                        NameShow = "【异】异常权限";
                    }
                    else if (sysAuthorization.Key == "ApprovalAbsenceRecord")
                    {
                        NameShow = "【缺】缺勤审批权限";
                    }
                    else if (sysAuthorization.Key == "ApprovalExtraWorkRecord")
                    {
                        NameShow = "【加】加班审批权限";
                    }
                    else
                    {
                        NameShow = "【他】其他权限";
                    }

                    sysA.Add("id", sysAuthorization.Key + sysAuthorization.Count());
                    sysA.Add("name", NameShow + " (共 " + sysAuthorization.Count() + " 项)");
                    sysA.Add("Count", sysAuthorization.Count().ToString());
                    sysA.Add("iconSkin", "parentSysRoleAuthIcon");
                    SysRoleAuths.Add(sysA);
                }
                else
                {
                    //可以考虑 进行分组
                    sysA.Add("id", sysAuthorization.Key + sysAuthorization.Count());
                    sysA.Add("name", sysAuthorization.Key + " (共 " + sysAuthorization.Count() + " 项)");
                    sysA.Add("Count", sysAuthorization.Count().ToString());
                    sysA.Add("iconSkin", "parentSysRoleAuthIcon");
                    SysRoleAuths.Add(sysA);
                }

                var ssraIndex = 1;
                foreach (var sysAuth in sysAuthorization)
                {
                    Dictionary<string, string> tempSRAV = new Dictionary<string, string>();

                    //通过roleID进行查询 authId， 如果找到相同的， 跳出循环。

                    ISysRoleAuthorizationService sras = new SysRoleAuthorizationService(Settings.Default.db);

                    List<SysRoleAuthorization> sysRoleAuths = sras.FindByRoleId(sysRoleId);

                    bool IsSelected = false;

                    foreach(var sysRoleAuth in sysRoleAuths)
                    {
                        if (sysAuth.id == sysRoleAuth.authId)
                        {
                            IsSelected = true;
                            break;
                        }
                    }

                    tempSRAV.Add("id", sysAuth.id.ToString());
                    tempSRAV.Add("name", "[" + ssraIndex + "] " + sysAuth.name);
                    tempSRAV.Add("controlName", sysAuth.controlName);
                    tempSRAV.Add("actionName", sysAuth.actionName);
                    //如果为空的话， 就是最上层？
                    tempSRAV.Add("pId", sysAuth.parentId.HasValue ? sysAuth.parentId.ToString() : sysAuthorization.Key + sysAuthorization.Count());
                    tempSRAV.Add("funCode", sysAuth.funCode);
                    tempSRAV.Add("isDelete", sysAuth.isDelete.ToString());
                    tempSRAV.Add("remarks", sysAuth.remarks);
                    tempSRAV.Add("open", "false");
                    tempSRAV.Add("iconSkin", "sysRoleAuthIcon");

                    if (IsSelected)
                    {
                        tempSRAV.Add("checked", "true");
                    }

                    SysRoleAuths.Add(tempSRAV);

                    ssraIndex++;
                }
            }

            IUserService us = new UserService(Settings.Default.db);
            List<User> users = us.FindByRoleId(sysRoleId);

            foreach (var user in users)
            {
                Dictionary<string, string> urs = new Dictionary<string, string>();
                urs.Add("id", user.id.ToString());
                urs.Add("name", "[" + user.name + "] [ " + user.email + "]");
                urs.Add("email", user.email);
                urs.Add("isLockedStr", user.isLockedStr);
                urs.Add("role", user.role.ToString());
                urs.Add("open", "false");
                urs.Add("iconSkin", "userIcon");
                Users.Add(urs);
            }

            Result.Add("Users", Users);
            Result.Add("SysRoleAuths", SysRoleAuths);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult AssignAuthsToRole(int sysRoleId, List<Dictionary<string, string>> roleAuthArray)
        {
            bool result = false;
            try
            {
                ISysRoleAuthorizationService sras = new SysRoleAuthorizationService(Settings.Default.db);
                //先删除全部权限，再新建
                if (sras.DeleteByRoleId(sysRoleId))
                {
                    if (roleAuthArray == null)
                    {
                        result = true;
                        return Json(result, JsonRequestBehavior.DenyGet);
                    }

                    foreach (var roleAuth in roleAuthArray)
                    {
                        //通过判断有没有Count字段 就可以断定 是否是自定义字段 - 头字段  funCode
                        string HasCount;
                        roleAuth.TryGetValue("Count", out HasCount);

                        if (string.IsNullOrWhiteSpace(HasCount))
                        {
                            string authId;
                            roleAuth.TryGetValue("id", out authId);

                            SysRoleAuthorization sysRoleAuthorization = new SysRoleAuthorization();
                            sysRoleAuthorization.roleId = sysRoleId;
                            sysRoleAuthorization.authId = Convert.ToInt16(authId);
                            sysRoleAuthorization.remarks = "创建时间:" + DateTime.Now;

                            result = sras.Create(sysRoleAuthorization);
                        }
                    }
                }
                return Json(result, JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(result, JsonRequestBehavior.DenyGet);
            }
        }


        private void SetAllTableName(string type, bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            ISysRoleAuthorizationService sras = new SysRoleAuthorizationService(Settings.Default.db);

            var SysRoleAuthorization = sras.GetAllTableName();
            if (SysRoleAuthorization.Count == 0)
            {
                SysRoleAuthorization tempSysRoleAuthorization = new SysRoleAuthorization();
                SysRoleAuthorization.Add(tempSysRoleAuthorization);
            }
            //获取当前记录的属性
            foreach (var property in SysRoleAuthorization[0].GetType().GetProperties())
            {
                if (!string.IsNullOrWhiteSpace(type) && type.Equals(property.Name))
                {
                    select.Add(new SelectListItem { Text = property.Name, Value = property.Name, Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = property.Name, Value = property.Name, Selected = false });
                }

            }

            ViewData["getAllTableNameList"] = select;
        }

        private void SetSearchConditions(int? type, bool allowBlank = false)
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


        public ActionResult AdvancedSearch(SysRoleAuthorizationSearchModel q)
        {
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            q.loginUser = user;
            ViewBag.Query = q;

            ISysRoleAuthorizationService ewrs = new SysRoleAuthorizationService(Settings.Default.db);
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IPagedList<SysRoleAuthorization> SysRoleAuthorizations = null;
            IQueryable<SysRoleAuthorization> SysRoleAuthorizationtemp = null;
            IQueryable<SysRoleAuthorization> SysRoleAuthorizationtemp1 = null;
            List<SysRoleAuthorization> Result = new List<SysRoleAuthorization>();
            if (!string.IsNullOrEmpty(Request.Form["allTableName"]))
            {
                if (!string.IsNullOrEmpty(Request.Form["searchConditions"]))
                {
                    if (!string.IsNullOrEmpty(Request.Form.Get("searchValueFirst")))
                    {
                        string AllTableName = Request.Form["allTableName"].ToString();
                        string[] AllTableNameArray = AllTableName.Split(',');
                        string SearchConditions = Request.Form["searchConditions"];
                        string[] SearchConditionsArray = SearchConditions.Split(',');
                        string searchValueFirst = Request.Form["searchValueFirst"];
                        string[] searchValueFirstArray = searchValueFirst.Split(',');

                        try
                        {
                            SysRoleAuthorizationtemp1 = ewrs.AdvancedSearch(AllTableNameArray[0], SearchConditionsArray[0], searchValueFirstArray[0])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                            if (AllTableNameArray.Length > 1)
                            {
                                int i = 1;
                                SysRoleAuthorizationtemp = ewrs.AdvancedSearch(AllTableNameArray[i], SearchConditionsArray[i], searchValueFirstArray[i])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                foreach (var temp in SysRoleAuthorizationtemp)
                                {
                                    if (SysRoleAuthorizationtemp1.FirstOrDefault(s => s.id.Equals(temp.id)) != null) Result.Add(temp);
                                }
                                if (AllTableNameArray.Length > 2)
                                {
                                    for (var i1 = 2; i1 < AllTableNameArray.Length; i1++)
                                    {
                                        IQueryable<SysRoleAuthorization> SysRoleAuthorizationtemp2 = null;
                                        SysRoleAuthorizationtemp2 = ewrs.AdvancedSearch(AllTableNameArray[i1], SearchConditionsArray[i1], searchValueFirstArray[i1])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                        List<SysRoleAuthorization> Resulttemp = new List<SysRoleAuthorization>();

                                        foreach (var addtemp in Result)
                                        {
                                            Resulttemp.Add(addtemp);
                                        }

                                        foreach (var temp in Result)
                                        {
                                            if (SysRoleAuthorizationtemp2.FirstOrDefault(s => s.id.Equals(temp.id)) == null)
                                            {
                                                SysRoleAuthorization removetemp = temp;
                                                Resulttemp.Remove(Resulttemp.Where(s => s.id == removetemp.id).FirstOrDefault());
                                            }
                                        }
                                        Result = Resulttemp;
                                    }
                                }
                            }
                            else
                            {
                                Result = SysRoleAuthorizationtemp1.ToList();
                            }
                        }
                        catch (Exception)
                        {
                            Result = null;
                        }

                    }
                }
            }
            try
            {
                SysRoleAuthorizations = Result.Distinct().ToPagedList(pageIndex, Settings.Default.pageSize);
            }
            catch
            {
                SysRoleAuthorizations = null;
            }

            return View("Index", SysRoleAuthorizations);
        }
    }
}
