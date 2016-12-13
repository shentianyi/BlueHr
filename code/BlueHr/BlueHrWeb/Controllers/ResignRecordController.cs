﻿using BlueHrLib.Data;
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
    public class ResignRecordController : Controller
    {
        // GET: ResignType
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            ResignRecordSearchModel q = new ResignRecordSearchModel();

            IResignRecordService rrs = new ResignRecordService(Settings.Default.db);

            IPagedList<ResignRecord> resignRecords = rrs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View(resignRecords);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "staffNr")] ResignRecordSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IResignRecordService rrs = new ResignRecordService(Settings.Default.db);

            IPagedList<ResignRecord> resignTypes = rrs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", resignTypes);
        }

        // GET: ResignType/Details/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ResignType/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            //SetDropDownList(null);
            return View();
        }

        // POST: ResignType/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "staffNr, resignTypeId, resignEffectiveAt, resignReason, remark")] ResignRecord resignRecord)
        {
            ResultMessage msg = new ResultMessage();

            ResignType resignType = new ResignType();

            //对ResignType 进行处理， 返回ID
            IResignTypeService rts = new ResignTypeService(Settings.Default.db);
            resignType = rts.IsResignTypeExit(Request.Form.Get("resignTypeName").Trim());

            //填充 是谁创建的
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            resignRecord.userId = user.id;

            resignRecord.resignTypeId = resignType.id;
            resignRecord.resignAt = DateTime.Now;

            try
            {
                msg = DoValidation(resignRecord);

                IResignRecordService rrs = new ResignRecordService(Settings.Default.db);
                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.DenyGet);
                }
                else
                {

                    bool isSucceed = rrs.Create(resignRecord);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "添加成功" : "添加失败";

                    return Json(msg, JsonRequestBehavior.DenyGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: ResignType/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IResignRecordService rrs = new ResignRecordService(Settings.Default.db);

            //ResignRecord resignRecord = rrs.FindById(id);
            return View(rrs.FindById(id));
        }

        // POST: ResignType/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id, staffNr, resignEffectiveAt, resignReason, remark")] ResignRecord resignRecord)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(resignRecord);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IResignRecordService cs = new ResignRecordService(Settings.Default.db);
                    bool isSucceed = cs.Update(resignRecord);

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

        // POST: ResignType/Approval/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Approval([Bind(Include = "id, approvalStatus, approvalRemark")] ResignRecord resignRecord)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                User user = System.Web.HttpContext.Current.Session["user"] as User;
                resignRecord.approvalAt = DateTime.Now;
                resignRecord.resignCheckUserId = user.id;
                IUserService us = new UserService(Settings.Default.db);
                resignRecord.resignChecker = us.FindById(user.id).name;
                IResignRecordService cs = new ResignRecordService(Settings.Default.db);
                bool isSucceed = cs.Update(resignRecord);
                if(isSucceed==true)
                {
                    // 创建离职记录##User##
                    try
                    {
                        IMessageRecordService mrs = new MessageRecordService(Settings.Default.db);
                        mrs.CreateStaffResignMessage(resignRecord.staffNr, (Session["user"] as User).id);
                    }
                    catch { }
                    if(resignRecord.approvalStatus== "通过审批")
                    {
                        IStaffService ss = new StaffService(Settings.Default.db);
                        Staff staff = ss.FindByNrThis(resignRecord.staffNr);
                        staff.workStatus = 200;
                        ss.Update(staff);
                    }
                }
                msg.Success = isSucceed;
                msg.Content = isSucceed ? "" : "更新失败";

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: ResignType/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IResignRecordService rrs = new ResignRecordService(Settings.Default.db);

            ResignRecord rr = rrs.FindById(id);
            //SetDropDownList(cp);
            return View(rr);
        }

        // POST: ResignType/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                //存在员工时不可删除
                IResignRecordService shfSi = new ResignRecordService(Settings.Default.db);
                List<ResignRecord> shf = shfSi.FindByResignType(id);

                if (null != shf && shf.Count() > 0)
                {
                    msg.Success = false;
                    msg.Content = "离职类型正在使用,不能删除!";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IResignTypeService cs = new ResignTypeService(Settings.Default.db);
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
        public ResultMessage DoValidation(ResignRecord resignRecord)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrWhiteSpace(resignRecord.resignTypeId.ToString()))
            {
                msg.Success = false;
                msg.Content = "离职类型不能为空";

                return msg;
            }
            IStaffService ss = new StaffService(Settings.Default.db);
            if (ss.FindByNr(resignRecord.staffNr) == null)
            {
                msg.Success = false;
                msg.Content = "员工号不存在";

                return msg;
            }

            IResignRecordService rrs = new ResignRecordService(Settings.Default.db);
            if (rrs.FindByNr(resignRecord.staffNr) != null)
            {
                msg.Success = false;
                msg.Content = "该员工已经递交申请";

                return msg;
            }

            if (string.IsNullOrEmpty(resignRecord.staffNr))
            {
                msg.Success = false;
                msg.Content = "离职员工不能为空";

                return msg;
            }

            return new ResultMessage() { Success = true, Content = "" };
        }


        [HttpPost]
        public JsonResult IsResignTypeExit(string resignType)
        {

            IResignTypeService rts = new ResignTypeService(Settings.Default.db);
            ResignType getResignType = rts.IsResignTypeExit(resignType);

            Dictionary<string, string> rt = new Dictionary<string, string>();

            rt.Add("ID", getResignType.id.ToString());
            rt.Add("Code", getResignType.code);
            rt.Add("Name", getResignType.name);
            rt.Add("Remark", getResignType.remark);

            return Json(rt, JsonRequestBehavior.DenyGet);
        }

        
        [HttpGet]
        public JsonResult GetResignType()
        {
            IResignTypeService rts = new ResignTypeService(Settings.Default.db);
            ResignTypeSearchModel csm = new ResignTypeSearchModel();

            List<ResignType> certType = rts.Search(csm).ToList();

            Dictionary<string, string> Result = new Dictionary<string, string>();

            foreach(var resignType in certType)
            {
                Result.Add(resignType.id.ToString(), resignType.name);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IResignTypeService at = new ResignTypeService(Settings.Default.db);

            var ResignType = at.GetAllTableName();

            if (ResignType != null)
            {
                //获取当前记录的属性
                foreach (var property in ResignType[0].GetType().GetProperties())
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
