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
    public class CertificateTypeController : Controller
    {
        // GET: CertificateType
        [UserAuthorize]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            CertificateTypeSearchModel q = new CertificateTypeSearchModel();

            ICertificateTypeService ss = new CertificateTypeService(Settings.Default.db);

            IPagedList<CertificateType> certfs = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            CertificateTypeInfoModel info = ss.GetCertificateTypeInfo(q);
            ViewBag.Info = info;

            return View(certfs);
        }

        public ActionResult Search([Bind(Include = "Name")] CertificateTypeSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            ICertificateTypeService ss = new CertificateTypeService(Settings.Default.db);

            IPagedList<CertificateType> certfs = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", certfs);
        }


        // GET: CertificateType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CertificateType/Create
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: CertificateType/Create
        [HttpPost]
        public JsonResult Create([Bind(Include = "Name,remark,isNecessary")] CertificateType certf)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(certf);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);
                    //是否是系统（默认为false，用户建立的都是false，即在用户端不可见此字段）
                    certf.isSystem = false;
                    bool isSucceed = cs.Create(certf);

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

        // GET: CertificateType/Edit/5
        public ActionResult Edit(int id)
        {
            ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);

            CertificateType certf = cs.FindById(id);

            SetDropDownList(certf);

            return View(certf);
        }

        // POST: CertificateType/Edit/5
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id,Name, remark,isNecessary")] CertificateType certf)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(certf);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);
                    //是否是系统（默认为false，用户建立的都是false，即在用户端不可见此字段）
                    certf.isSystem = false;
                    bool isSucceed = cs.Update(certf);

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

        // GET: CertificateType/Delete/5
        public ActionResult Delete(int id)
        {
            ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);

            CertificateType certf = cs.FindById(id);
            SetDropDownList(certf);
            return View(certf);
        }

        // POST: CertificateType/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                //存在员工时不可删除
                ICertificateService shfSi = new CertificateService(Settings.Default.db);
                List<Certificate> shf = shfSi.FindByCertificateType(id);

                if (null != shf && shf.Count() > 0)
                {
                    msg.Success = false;
                    msg.Content = "证照类别正在使用,不能删除!";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {


                    ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);

                    //系统级别不可编辑删除
                    List<CertificateType> cers = cs.GetAll();
                    bool isSystem = cers.Where(p => p.isSystem && p.id == id).ToList().Count() > 0;

                    if (isSystem)
                    {
                        msg.Success = false;
                        msg.Content = "系统级别不可删除";

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        bool isSucceed = cs.DeleteById(id);

                        msg.Success = isSucceed;
                        msg.Content = isSucceed ? "" : "删除失败";

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private void SetDropDownList(CertificateType certf)
        {
            if (certf != null)
            {
                SetSystemCertificateTypeList(certf.systemCode);
                SetIsSystemList(certf.isSystem);
                SetIsNecessaryList(certf.isNecessary);
            }
            else
            {
                SetSystemCertificateTypeList(100);
                SetIsSystemList(null);
                SetIsNecessaryList(false);
            }
        }

        private void SetSystemCertificateTypeList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(SystemCertificateType));

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
            ViewData["systemCertificateTypeList"] = select;
        }

        private void SetIsSystemList(bool? type, bool allowBlank = true)
        {
            List<EnumItem> item = new List<EnumItem>() { new EnumItem() { Text = "是", Value = "true" }, new EnumItem() { Text = "否", Value = "false" } };
            //EnumHelper.GetList(typeof(IsOnTrail));

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
            ViewData["isSystemList"] = select;
        }

        //isNecessaryList
        private void SetIsNecessaryList(bool? type, bool allowBlank = true)
        {
            List<EnumItem> item = new List<EnumItem>() { new EnumItem() { Text = "是", Value = "true" }, new EnumItem() { Text = "否", Value = "false" } };
            //EnumHelper.GetList(typeof(IsOnTrail));

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
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
                }
            }
            ViewData["isNecessaryList"] = select;
        }

        [HttpPost]
        //（列表（分页）、新建、删除（系统级别不可编辑删除）（存在员工时不可删除）
        //）：名称（不可空），是否是系统（默认为false，用户建立的都是false，即在用户端不可见此字段），是否是必须（默认为false），备注（可空）
        //    系统自建系统级别类别：身份证（非必须）、健康证（非必须）、职业证书（非必须）

        public ResultMessage DoValidation(CertificateType model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.name))
            {
                msg.Success = false;
                msg.Content = "证照类别名称不能为空";

                return msg;
            }

            ICertificateTypeService cs = new CertificateTypeService(Settings.Default.db);
            List<CertificateType> shift = cs.GetAll();

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
                bool isSystem = shift.Where(p => p.isSystem && p.id == model.id).ToList().Count() > 0;

                if (isSystem)
                {
                    msg.Success = false;
                    msg.Content = "系统级别不可编辑";

                    return msg;
                }

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
