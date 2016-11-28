using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Helper;
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
    public class AbsenceTypeController : Controller
    {
        // GET: AbsenceType
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            AbsenceTypeSearchModel q = new AbsenceTypeSearchModel();

            IAbsenceTypeService ss = new AbsenceTypeService(Settings.Default.db);

            IPagedList<AbsenceType> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            AbsenceTypeInfoModel info = ss.GetAbsenceTypeInfo(q);
            ViewBag.Info = info;

            return View(models);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "Name")] AbsenceTypeSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IAbsenceTypeService ss = new AbsenceTypeService(Settings.Default.db);

            IPagedList<AbsenceType> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", models);
        }

        // GET: AbsenceType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AbsenceType/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            //SetDropDownList(null);
            return View();
        }

        // POST: AbsenceType/Create
        [HttpPost]
        [RoleAndDataAuthorizationAttribute]
        public JsonResult Create([Bind(Include = "Name,code, remark")] AbsenceType model)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(model);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);
                    bool isSucceed = cs.Create(model);

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

        // GET: AbsenceType/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);

            AbsenceType jt = cs.FindById(id);
            //SetDropDownList(jt);
            return View(jt);
        }

        // POST: AbsenceType/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, name,code, remark")] AbsenceType model)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(model);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);
                    bool isSucceed = cs.Update(model);

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

        // GET: AbsenceType/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);

            AbsenceType cp = cs.FindById(id);
            //SetDropDownList(cp);
            return View(cp);
        }

        // POST: AbsenceType/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                //存在员工时不可删除
                IAbsenceRecordService shfSi = new AbsenceRecordService(Settings.Default.db);
                List<AbsenceRecrod> shf = shfSi.FindByAbsenceType(id);

                if (null != shf && shf.Count() > 0)
                {
                    msg.Success = false;
                    msg.Content = "缺勤类型正在使用,不能删除!";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);
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
        //4.7	缺勤类别管理
        //（列表（分页）、新建、编辑、删除（存在员工时不可删除）
        //）：代码（不可空，唯一性），名称（不可空），备注（可空）
        //        数据例子如：A；小病假；小病假的备注

        public ResultMessage DoValidation(AbsenceType model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.code))
            {
                msg.Success = false;
                msg.Content = "编码不能为空";

                return msg;
            }

            if (string.IsNullOrEmpty(model.name))
            {
                msg.Success = false;
                msg.Content = "名称不能为空";

                return msg;
            }

            IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);
            List<AbsenceType> shift = cs.GetAll();

            if (model.id <= 0)
            {
                bool isRecordExists = shift.Where(p => p.name == model.name || p.code == model.code).ToList().Count() > 0;

                if (isRecordExists)
                {
                    msg.Success = false;
                    msg.Content = "数据已经存在!";

                    return msg;
                }
            }
            else
            {
                bool isRecordExists = shift.Where(p => (p.name == model.name || p.code == model.code) && p.id != model.id).ToList().Count() > 0;

                if (isRecordExists)
                {
                    msg.Success = false;
                    msg.Content = "数据已经存在!";

                    return msg;
                }
            }

            return new ResultMessage() { Success = true, Content = "" };
        }
        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IAbsenceTypeService at = new AbsenceTypeService(Settings.Default.db);

            var AbsenceType = at.GetAllTableName();

            if (AbsenceType != null)
            {
                //获取当前记录的属性
                foreach (var property in AbsenceType[0].GetType().GetProperties())
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
