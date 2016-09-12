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
        [AdminAuthorize]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            UserSearchModel q = new UserSearchModel();

            IUserService ss = new UserService(Settings.Default.db);

            IPagedList<User> users = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q; 

            return View(users);
        }


        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [AdminAuthorize]
        // GET: User/Create
        public ActionResult Create()
        {
            SetRoleList(null);
            return View();
        }

        // POST: User/Create
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
                    bool isSucceed = cs.Create(user);

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
        // GET: User/Edit/5
        public ActionResult   Edit(int id)
        {
            IUserService cs = new UserService(Settings.Default.db);

            User user = cs.FindById(id);
            SetRoleList(user.role);
            return View(user);
        }


        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,name,email,pwd,role")] User user)
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
                    bool isSucceed = cs.Update(user);

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
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            IUserService cs = new UserService(Settings.Default.db);

            User user = cs.FindById(id);
            SetRoleList(user.role);
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
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



        // POST: User/Delete/5
        [HttpPost]
        public ActionResult LockUnLock(int id)
        {
            ResultMessage msg = new ResultMessage();

            try
            {

                IUserService cs = new UserService(Settings.Default.db);
                bool isSucceed = cs.LockUnLock (id);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "" : "操作失败";

                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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

            if (string.IsNullOrEmpty(model.pwd))
            {
                msg.Success = false;
                msg.Content = "密码不能为空";

                return msg;
            }

            if (!model.role.HasValue)
            {
                msg.Success = false;
                msg.Content = "角色不能为空";

                return msg;
            }

            IUserService cs = new UserService(Settings.Default.db);

            if (cs.FindByEmail(model.email)!=null && model.id<=0)
            {
                msg.Success = false;
                msg.Content = "邮箱已存在不可重复添加";

                return msg;
            }
 

            return new ResultMessage() { Success = true, Content = "" };
        }
    }
}
