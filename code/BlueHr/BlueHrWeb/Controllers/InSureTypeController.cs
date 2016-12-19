using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.PageViewModel;
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
    public class InSureTypeController : Controller
    {
        // GET: InSureType
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            InSureTypeSearchModel q = new InSureTypeSearchModel();

            IInSureTypeService ss = new InSureTypeService(Settings.Default.db);

            IPagedList<InsureType> insureTypes = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            InSureTypeInfoModel info = ss.GetInsureTypeInfo(q);
            ViewBag.Info = info;

            return View(insureTypes);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "Name")] InSureTypeSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IInSureTypeService ss = new InSureTypeService(Settings.Default.db);

            IPagedList<InsureType> insureTypes = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", insureTypes);
        }

        // GET: InSureType/Details/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InSureType/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            return View();
        }

        // POST: InSureType/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "Name, remark")] InsureType insureType)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(insureType);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IInSureTypeService cs = new InSureTypeService(Settings.Default.db);
                    bool isSucceed = cs.Create(insureType);

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

        // GET: InSureType/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IInSureTypeService cs = new InSureTypeService(Settings.Default.db);

            InsureType jt = cs.FindById(id);
            return View(jt);
        }

        // POST: InSureType/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id, name, remark")] InsureType insureType)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(insureType);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IInSureTypeService cs = new InSureTypeService(Settings.Default.db);
                    bool isSucceed = cs.Update(insureType);

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

        // GET: InSureType/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IInSureTypeService cs = new InSureTypeService(Settings.Default.db);

            InsureType cp = cs.FindById(id);
            return View(cp);
        }

        // POST: InSureType/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                //存在员工时不可删除
                IStaffService shfSi = new StaffService(Settings.Default.db);
                List<Staff> shf = shfSi.FindByInsureType(id);

                if (null != shf && shf.Count() > 0)
                {
                    msg.Success = false;
                    msg.Content = "保险类别正在使用,不能删除!";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IInSureTypeService cs = new InSureTypeService(Settings.Default.db);
                    bool isSucceed = cs.DeleteById(id);

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


        [HttpPost]
        //4.5	保险类别管理
        //（列表（分页）、新建、编辑、删除（存在员工时不可删除）
        //）：名称（不可空），备注（可空）
        public ResultMessage DoValidation(InsureType model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.name))
            {
                msg.Success = false;
                msg.Content = "保险类别名称不能为空";

                return msg;
            }

            IInSureTypeService cs = new InSureTypeService(Settings.Default.db);
            List<InsureType> shift = cs.GetAll();

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

        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IInSureTypeService at = new InSureTypeService(Settings.Default.db);

            var InSureType = at.GetAllTableName();

            if (InSureType != null)
            {
                //获取当前记录的属性
                foreach (var property in InSureType[0].GetType().GetProperties())
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
