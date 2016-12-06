using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Message;
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
    public class UserController : Controller
    {
        // GET: User
        //
        [AdminAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            UserSearchModel q = new UserSearchModel();

            IUserService ss = new UserService(Settings.Default.db);

            IPagedList<User> users = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);
            
            users.ToList().ForEach(p =>
            {
                Tuple<string, string, string, string, string> cmpDep = GetAuthCompanyAndDepartment(p);

                string dep = cmpDep.Item3.TrimEnd(',');
                if (dep.Split(',').Count() > 5)
                {
                    dep = string.Join(",", dep.Split(',').Take(5).ToArray()) + " ...";
                }
                p.roleStr = cmpDep.Item1.TrimEnd(',');
                p.AuthCompany = cmpDep.Item2.TrimEnd(',');
                p.AuthDepartment = dep;
            });

            ViewBag.Query = q;
            SetAllTableName(null);
            SetSearchConditions(null);
            return View(users);
        }


        [RoleAndDataAuthorizationAttribute]
        public ActionResult UserMsg()
        {
            return View();
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Log()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserDetail(int id)
        {
            Dictionary<string, string> Result = new Dictionary<string, string>();
            IUserService us = new UserService(Settings.Default.db);
            User detail = us.FindById(id);
            Result.Add("用户名",detail.name);
            Result.Add("邮箱",detail.email);
            Result.Add("是否锁定",detail.isLockedStr);
            Result.Add("角色类型",detail.roleStr);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        [AdminAuthorize]
        [RoleAndDataAuthorizationAttribute]
        // GET: User/Create
        public ActionResult Create()
        {
            SetSysRoleList(false);
            ViewBag.TheCmpIds = "";
            ViewBag.TheDepIds = "";
            return View();
        }

        // POST: User/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "name,email,pwd,role")] User user)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(user);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IUserService cs = new UserService(Settings.Default.db);

                    //string authCmp = HttpContext.Request.Form["selCompanys"];
                    //string authDep = HttpContext.Request.Form["selDeparts"];
                    string authCmpDep = HttpContext.Request.Form["selCmpDepIds"];
                    string theRoleId = HttpContext.Request.Form["role"];

                    user.role = !string.IsNullOrEmpty(theRoleId) ? int.Parse(theRoleId) : -1;
                    user.isLocked = false;

                    bool isSucceed = cs.Create(user);

                    //add auth company and department

                    ISysUserDataAuthService si = new SysUserDataAuthService(Settings.Default.db);

                    List<SysUserDataAuth> userDataAuth = new List<SysUserDataAuth>();

                    authCmpDep.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(p =>
                    {
                        List<string> xk = p.Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                        SysUserDataAuth tmp = new SysUserDataAuth();
                        tmp.cmpId = int.Parse(xk[0].ToString());
                        tmp.userId = user.id;
                        tmp.departId = xk.Count > 1 && xk[1] != null ? xk[1].ToString() : "";

                        userDataAuth.Add(tmp);
                    });

                    //authCmp.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(p =>
                    //{
                    //    authDep.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(k =>
                    //    {
                    //        string[] tp2 = k.Split('|');

                    //        if (tp2[0] == p)
                    //        {
                    //            SysUserDataAuth tmp = new SysUserDataAuth();
                    //            tmp.cmpId = int.Parse(p);
                    //            tmp.userId = user.id;
                    //            tmp.departId = int.Parse(tp2[1].ToString());

                    //            userDataAuth.Add(tmp);
                    //        }
                    //    });
                    //});

                    si.Creates(userDataAuth);

                    //bool isSucceed = cs.Create(user);

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

        [AdminAuthorize]
        [RoleAndDataAuthorizationAttribute]
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            IUserService cs = new UserService(Settings.Default.db);
            User user = cs.FindById(id);

            SetSysRoleList(false);
            SetCmpList(false);

            Tuple<string, string, string, string, string> cmpDep = GetAuthCompanyAndDepartment(user);
            string dep = cmpDep.Item3.TrimEnd(',');
            if (dep.Split(',').Count() > 5)
            {
                dep = string.Join(",", dep.Split(',').Take(5).ToArray())+ " ...";
            }
            user.roleStr = cmpDep.Item1.TrimEnd(',');
            user.AuthCompany = cmpDep.Item2.TrimEnd(',');
            user.AuthDepartment =dep;
            ViewBag.TheCmpDepIds = cmpDep.Item4;
            ViewBag.TheSelCmpDepNames = cmpDep.Item5;

            return View(user);
        }


        // POST: User/Edit/5
        [HttpPost]
        //[RoleAndDataAuthorizationAttribute]
        public ActionResult Edit([Bind(Include = "id,name,email,role")] User user)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(user);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IUserService cs = new UserService(Settings.Default.db);

                    //string authCmp = HttpContext.Request.Form["selCompanys"];
                    //string authDep = HttpContext.Request.Form["selDeparts"];
                    string authCmpDep = HttpContext.Request.Form["selCmpDepIds"];
                    string theRoleId = HttpContext.Request.Form["role"];

                    user.role = !string.IsNullOrEmpty(theRoleId) ? int.Parse(theRoleId) : -1;
                    user.isLocked = false;

                    bool isSucceed = cs.Update(user);

                    //add auth company and department

                    ISysUserDataAuthService si = new SysUserDataAuthService(Settings.Default.db);

                    List<SysUserDataAuth> allAuthList = si.GetAll().Where(p => p.userId == user.id).ToList();

                    //先全部删除然后再加
                    allAuthList.ForEach(kk =>
                    {
                        si.DeleteById(kk.id);
                    });

                    List<SysUserDataAuth> userDataAuth = new List<SysUserDataAuth>();

                    authCmpDep.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(p =>
                    {
                        List<string> xk = p.Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                        SysUserDataAuth tmp = new SysUserDataAuth();
                        tmp.cmpId = int.Parse(xk[0].ToString());
                        tmp.userId = user.id;
                        tmp.departId = xk.Count > 1 && xk[1] != null ? xk[1].ToString() : "";

                        userDataAuth.Add(tmp);
                    });

                    //authCmp.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(p =>
                    //{
                    //    authDep.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(k =>
                    //    {
                    //        string[] tp2 = k.Split('|');

                    //        if (tp2[0] == p)
                    //        {
                    //            SysUserDataAuth tmp = new SysUserDataAuth();
                    //            tmp.cmpId = int.Parse(p);
                    //            tmp.userId = user.id;
                    //            tmp.departId = int.Parse(tp2[1].ToString());

                    //            userDataAuth.Add(tmp);
                    //        }
                    //    });
                    //});

                    si.Creates(userDataAuth);

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

        [AdminAuthorize]
        [RoleAndDataAuthorizationAttribute]
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            IUserService cs = new UserService(Settings.Default.db);

            User user = cs.FindById(id);

           


            SetRoleList(user.role);
            SetSysRoleList(false);
            SetCmpList(false);

            Tuple<string, string, string, string, string> cmpDep = GetAuthCompanyAndDepartment(user);

            string dep = cmpDep.Item3.TrimEnd(',');
            if (dep.Split(',').Count() > 5)
            {
                dep = string.Join(",", dep.Split(',').Take(5).ToArray()) + " ...";
            }
            user.roleStr = cmpDep.Item1.TrimEnd(',');
            user.AuthCompany = cmpDep.Item2.TrimEnd(',');
            user.AuthDepartment = dep;


            ViewBag.TheCmpDepIds = cmpDep.Item4;
            ViewBag.TheSelCmpDepNames = cmpDep.Item5;


            //SetSysRoleList();
            //SetCmpList();
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {

                IUserService cs = new UserService(Settings.Default.db);
                bool isSucceed = cs.DeleteById(id);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "" : "删除失败";

                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [AdminAuthorize]
        [RoleAndDataAuthorizationAttribute]
        // GET: User/Create
        public ActionResult ChangePwd(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.id = id;

                return View();
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }

        [AdminAuthorize]
        [RoleAndDataAuthorizationAttribute]
        // GET: User/Create
        [HttpPost]
        public ActionResult ChangePwd(int id, string pwd,string pwdcompare)
        {
            ViewBag.id = id;
             IUserService cs = new UserService(Settings.Default.db);
            if (string.IsNullOrEmpty(pwd.Trim()))
            {
                ViewBag.error = "密码不可为空";
                return View("ChangePwd");
            }
            else
            {
                if (pwd == pwdcompare)
                {
                    try
                    {
                        bool b = cs.ChangePwd(id, pwd);
                        ViewBag.error = "修改成功";
                        return View("ChangePwd");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.error = ex.Message;
                        return View("ChangePwd");
                    }
                }
                else
                {
                    ViewBag.error = "密码不一致";
                    return View("ChangePwd");
                }
            }
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult LockUnLock(int id)
        {
            ResultMessage msg = new ResultMessage();

            try
            {

                IUserService cs = new UserService(Settings.Default.db);
                bool isSucceed = cs.LockUnLock(id);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "" : "操作失败";

                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public Tuple<string, string, string, string, string> GetAuthCompanyAndDepartment(User user)
        {
            ISysRoleService rSi = new SysRoleService(Settings.Default.db);
            SysRole sRole = rSi.FindById(user.role ?? -1);

            string roleStr = sRole != null ? sRole.name : "";

            //set 权限公司 AuthCompany 权限部门 AuthDepartment
            ISysUserDataAuthService userDataSi = new SysUserDataAuthService(Settings.Default.db);
            List<SysUserDataAuth> allDataAuth = userDataSi.GetAll();

            List<string> cmpIds = new List<string>();
            List<string> departMentIds = new List<string>();

            string bindCmpDepIds = "";
            string bindCmpDepIdsWithName = "";

            allDataAuth.Where(m => m.userId.ToString() == user.id.ToString()).ToList().ForEach(k =>
            {
                //company section
                cmpIds.Add(k.cmpId.ToString());
                string depStr = k.departId;

                //bind id with names
                ICompanyService icmpSi = new CompanyService(Settings.Default.db);
                Company tMode = icmpSi.FindById(k.cmpId ?? -1);
                string tmpCmp = k.cmpId + "&" + tMode.name;

                string aTmpDepsList = "";
                //department section
                depStr.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(m =>
                {
                    departMentIds.Add(m.ToString());

                    IDepartmentService idepSi = new DepartmentService(Settings.Default.db);
                    Department dMode = idepSi.FindById(int.Parse(m));

                    string tmpDep = m + "&" + dMode.name;
                    aTmpDepsList += tmpDep + ",";
                });

                string tmp = k.cmpId.ToString() + "|";
                tmp += k.departId;

                bindCmpDepIds += tmp + ";";


                string withNameTmmp = "";
                withNameTmmp += tmpCmp + "|" + aTmpDepsList;

                bindCmpDepIdsWithName += withNameTmmp + ";";

            });

            ICompanyService cmpSi = new CompanyService(Settings.Default.db);

            string AuthCompany = cmpSi.FindByIds(cmpIds);

            IDepartmentService depSi = new DepartmentService(Settings.Default.db);
            string AuthDepartment = depSi.FindByIds(departMentIds);

            Tuple<string, string, string, string, string> cmpDep = new Tuple<string, string, string, string, string>(roleStr, AuthCompany, AuthDepartment, bindCmpDepIds, bindCmpDepIdsWithName);

            return cmpDep;
        }


        private void SetRoleList(int? type, bool allowBlank = false)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(RoleType));

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
            ViewData["role"] = select;
        }

        [HttpPost]
        //4.5	保险类别管理
        //（列表（分页）、新建、编辑、删除（存在员工时不可删除）
        //）：名称（不可空），备注（可空）
        public ResultMessage DoValidation(User model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.name))
            {
                msg.Success = false;
                msg.Content = "姓名不能为空";

                return msg;
            }

            if (string.IsNullOrEmpty(model.email))
            {
                msg.Success = false;
                msg.Content = "邮箱不能为空";

                return msg;
            }

            //if (string.IsNullOrEmpty(model.pwd))
            //{
            //    msg.Success = false;
            //    msg.Content = "密码不能为空";

            //    return msg;
            //}

            string selCompanys = HttpContext.Request.Form["selCompanys"];
            string selDeparts = HttpContext.Request.Form["selDeparts"];
            string theRoleId = HttpContext.Request.Form["role"];
            string roleStr = HttpContext.Request.Form["roleStr"];
            string authCompany = HttpContext.Request.Form["authCompany"];
            string authDep = HttpContext.Request.Form["authDep"];

            //if (!model.role.HasValue)
            //{
            //    msg.Success = false;
            //    msg.Content = "角色不能为空";

            //    return msg;
            //}

            //if (string.IsNullOrEmpty(authCompany))
            //{
            //    if (string.IsNullOrEmpty(selCompanys))
            //    {
            //        msg.Success = false;
            //        msg.Content = "公司权限不能为空";
            //        return msg;
            //    }
            //}

            //if (string.IsNullOrEmpty(authDep))
            //{
            //    if (string.IsNullOrEmpty(selDeparts))
            //    {
            //        msg.Success = false;
            //        msg.Content = "部门权限不能为空";
            //        return msg;
            //    }
            //}

            //if (string.IsNullOrEmpty(roleStr))
            //{
            //    if (string.IsNullOrEmpty(theRoleId))
            //    {
            //        msg.Success = false;
            //        msg.Content = "角色不能为空";
            //        return msg;
            //    }
            //}

            IUserService cs = new UserService(Settings.Default.db);

            if (cs.FindByEmail(model.email) != null && model.id <= 0)
            {
                msg.Success = false;
                msg.Content = "邮箱已存在不可重复添加";

                return msg;
            }


            return new ResultMessage() { Success = true, Content = "" };
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

        private void SetCmpList(bool allowBlank = false)
        {
            //ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);
            ICompanyService cs = new CompanyService(Settings.Default.db);

            CompanySearchModel csm = new CompanySearchModel();

            List<Company> sysRoleList = cs.Search(csm).ToList();

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

            ViewData["CompanyList"] = select;
        }

        [HttpPost]
        public JsonResult AsignRole(string userId, string roleId)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                //check user

                if (string.IsNullOrEmpty(userId))
                {
                    msg.Success = false;
                    msg.Content = "用户错误！";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }

                if (string.IsNullOrEmpty(roleId))
                {
                    msg.Success = false;
                    msg.Content = "请选择角色！";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }

                //AbsenceRecordApproval absApproval = new AbsenceRecordApproval();
                //absApproval.absRecordId = !string.IsNullOrEmpty(absRecordId) ? int.Parse(absRecordId) : -1;
                //absApproval.approvalStatus = approvalStatus;
                //absApproval.approvalTime = DateTime.Now;
                //absApproval.remarks = approvalRemarks;

                //if (Session["user"] != null)
                //{
                //    User user = Session["user"] as User;
                //    absApproval.userId = user.id;
                //}

                //IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);
                //bool isSucceed = cs.ApprovalTheRecord(absApproval);

                //msg.Success = isSucceed;
                //msg.Content = "审批成功！";

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg.Success = false;
                msg.Content = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetCompanys()
        {
            ICompanyService cs = new CompanyService(Settings.Default.db);

            CompanySearchModel csm = new CompanySearchModel();

            List<Company> cmps = cs.Search(csm).ToList();

            List<DepartTree> dpTrees = new List<DepartTree>();

            cmps.ForEach(p =>
            {
                DepartTree itm = new DepartTree();
                itm.id = p.id.ToString();
                itm.text = p.name;

                dpTrees.Add(itm);
            });

            return Json(dpTrees, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDepartmentsByCompany(string companyId)
        {
            //ResultMessage msg = new ResultMessage();

            try
            {
                IDepartmentService depSi = new DepartmentService(Settings.Default.db);

                List<Department> deps = depSi.FindByCompanyId(int.Parse(companyId)).ToList();

                List<DepartTree> dpTrees = new List<DepartTree>();

                //get all parents
                deps.Where(p => string.IsNullOrEmpty(p.parentId.ToString())).ToList().ForEach(k =>
                {
                    DepartTree item = new DepartTree();
                    item.text = k.name;
                    item.id = k.id.ToString();

                    dpTrees.Add(item);
                });

                //get all childs 5 circle
                dpTrees.ForEach(p =>
                {
                    List<DepItem> allList = new List<DepItem>();

                    deps.Where(k => k.parentId.ToString() == p.id).ToList().ForEach(m =>
                    {
                        DepItem im = new DepItem();
                        im.text = m.name;
                        im.id = m.id.ToString();

                        allList.Add(im);

                        List<DepItem> callList = new List<DepItem>();
                        List<Department> cdeps = depSi.FindByParentId(m.id).ToList();
                        cdeps.ForEach(cm =>
                        {
                            DepItem cim = new DepItem();
                            cim.text = cm.name;
                            cim.id = cm.id.ToString();

                            callList.Add(cim);



                            List<DepItem> ccallList = new List<DepItem>();
                            List<Department> ccdeps = depSi.FindByParentId(cm.id).ToList();
                            ccdeps.ForEach(ccm =>
                            {
                                DepItem ccim = new DepItem();
                                ccim.text = ccm.name;
                                ccim.id = ccm.id.ToString();

                                ccallList.Add(ccim);


                            });
                            if (ccallList.Count > 0)
                            {
                                cim.nodes = ccallList;
                            }
                        });
                        if (callList.Count > 0)
                        {
                            im.nodes = callList;
                        }
                    });

                    p.nodes = allList;
                });



                if (dpTrees.Count > 0)
                {
                    return Json(dpTrees, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("empty_result", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //msg.Success = false;
                //msg.Content = ex.Message;
                return Json("empty_result", JsonRequestBehavior.AllowGet);
            }
        }

        public class DepItem
        {
            public string text { get; set; }
            public string id { get; set; }
            public List<DepItem> nodes { get; set; }
        }

        public class DepartTree
        {
            //          {
            //  text: "Parent 1",
            //  nodes: [
            //    {
            //      text: "Child 1",
            //      nodes: [
            //        {
            //          text: "Grandchild 1"
            //        },
            //        {
            //          text: "Grandchild 2"
            //        }
            //      ]
            //    },
            //    {
            //      text: "Child 2"
            //    }
            //  ]
            //},
            //{
            //  text: "Parent 2"
            //},

            public string text { get; set; }
            public string id { get; set; }
            public List<DepItem> nodes { get; set; }
        }

        private void SetAllTableName(string type, bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IUserService us = new UserService(Settings.Default.db);

            var User = us.GetAllTableName();

            if (User != null)
            {
                //获取当前记录的属性
                foreach (var property in User[0].GetType().GetProperties())
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


        public ActionResult AdvancedSearch(UserSearchModel q)
        {
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            q.loginUser = user;
            ViewBag.Query = q;

            IUserService us = new UserService(Settings.Default.db);
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IPagedList<User> users = null;

            string AllTableName = null;
            string SearchConditions = null;
            string SearchValueFirst = null;
            string SearchValueSecond = null;

            if (!string.IsNullOrEmpty(Request.Form["allTableName"]))
            {
                AllTableName = Request.Form["allTableName"].ToString();

                SetAllTableName(AllTableName);

                if (!string.IsNullOrEmpty(Request.Form["searchConditions"]))
                {
                    SearchConditions = Request.Form.Get("searchConditions");

                    SetSearchConditions(Convert.ToInt32(SearchConditions));

                    if (!string.IsNullOrEmpty(Request.Form.Get("searchValueFirst")))
                    {
                        SearchValueFirst = Request.Form.Get("searchValueFirst").ToString();

                        ViewBag.searchValueFirst = SearchValueFirst;

                        SearchValueSecond = Request.Form.Get("searchValueSecond").ToString();
                        ViewBag.searchValueSecond = SearchValueSecond;

                        //有两个值， 需要进行两个值的查询
                        users = us.AdvancedSearch(AllTableName, SearchConditions, SearchValueFirst, SearchValueSecond).ToPagedList(pageIndex, Settings.Default.pageSize);

                    }
                    else
                    {
                        //不能进行查询
                    }
                }
            }


            return View("Index", users);
        }
    }

}
