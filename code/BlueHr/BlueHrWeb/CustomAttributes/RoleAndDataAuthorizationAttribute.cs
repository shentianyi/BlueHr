using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Message;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.Models;
using BlueHrWeb.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BlueHrWeb.CustomAttributes
{
    public class RoleAndDataAuthorizationAttribute : System.Web.Mvc.FilterAttribute, System.Web.Mvc.IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            User user = System.Web.HttpContext.Current.Session["user"] as User;

            if (user == null || !user.isLocked.HasValue || user.isLocked.Value)
            {
                System.Web.HttpContext.Current.Session["user"] = null;
                filterContext.Result =  new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary {{ "controller", "Account" },
                                              { "action", "Login" },
                                             { "returnUrl",    filterContext.HttpContext.Request.RawUrl } });

                return;
            }

            //最高权限用户
            string sysAdminMail = System.Configuration.ConfigurationManager.AppSettings["SysAdministrator"];

            if (user.email.Trim() != sysAdminMail.Trim())
            {
                string userId = user.id.ToString();
                string roldId = user.role.ToString();

                //根据用户角色获取所有权限
                ISysAuthorizationService authSi = new SysAuthorizationService(Settings.Default.db);
                List<SysAuthorization> allAuths = authSi.GetSysAuthByRoleId(roldId);

                string theRequestUrl = filterContext.HttpContext.Request.Path.ToLower();
                if (theRequestUrl.IndexOf("search") >= 0)
                { return; }
                //if (theRequestUrl.IndexOf("index") <= 0)
                //{
                //    theRequestUrl += "/index";
                //}

                List<string> authUrls = new List<string>();
                List<string> dataActions = new List<string>();

                allAuths.ForEach(p =>
                {
                    if (p.actionName.ToLower() == "index")
                    {
                        string authUrl = "/" + p.controlName + "/" + p.actionName;

                        authUrls.Add(authUrl.ToLower());
                    }
                    else
                    {
                        string authAction = "/" + p.controlName + "/" + p.actionName;
                        dataActions.Add(authAction.ToLower());
                    }

                    //更新页面跳转回index页面 只有controllerName 拼接/index 
                    //fix 分页没权限

                    if ("/" + p.controlName.ToLower() == theRequestUrl )
                    {
                        string tmp = "/" + p.controlName.ToLower(); 

                        theRequestUrl += "/index";
                        //theRequestUrl +=;
                    }
                });


                if (theRequestUrl.IndexOf("index") != -1)
                {
                    //菜单访问权限 (index)
                    bool hasViewAcess = authUrls.Contains(theRequestUrl);

                    if (!hasViewAcess)
                    {
                        filterContext.Result = new RedirectResult("/Home/NoAuthPage/1");
                    }
                }

                else
                {
                    //操作访问权限 (create,update,delete)
                    int hasActionAccess = 0;

                    dataActions.ForEach(p =>
                    {
                        if (theRequestUrl.IndexOf(p) != -1)
                        {
                            hasActionAccess++;
                        }
                    });

                    //if (hasActionAccess <= 0)
                    //{
                    //    if (filterContext.RequestContext.HttpContext.Request.ContentType == "application/x-www-form-urlencoded; charset=UTF-8")
                    //    {
                    //        //throw new Exception("无权操作！");
                    //        var msg = new ResultMessage() { Content = "无权操作！" };
                    //        filterContext.Result = new JsonResult() { Data=msg};
                    //    }
                    //    else {
                    //        filterContext.Result = new RedirectResult("/Home/NoAuthPage/2");
                    //    }
                    //}
                }
                //3. 数据查询权限
            }
        }
    }
}