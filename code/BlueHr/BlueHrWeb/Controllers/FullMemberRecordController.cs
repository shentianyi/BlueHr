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
    public class FullMemberRecordController : Controller
    {
        // GET: FullMemberRecord
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            FullMemberRecordSearchModel q = new FullMemberRecordSearchModel();

            IFullMemberRecordService rrs = new FullMemberRecordService(Settings.Default.db);

            IPagedList<FullMemberRecord> FullMemberRecordRecords = rrs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View(FullMemberRecordRecords);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "StaffNr")] FullMemberRecordSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IFullMemberRecordService rrs = new FullMemberRecordService(Settings.Default.db);

            IPagedList<FullMemberRecord> FullMemberRecordRecords = rrs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", FullMemberRecordRecords);
        }

        // GET: FullMemberRecord/Details/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FullMemberRecord/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            return View();
        }

        // POST: FullMemberRecord/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "staffNr, isPassCheck, checkScore, beFullAt, remark")] FullMemberRecord FullMemberRecordRecord)
        {
            ResultMessage msg = new ResultMessage();
            
            //填充 是谁创建的
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            FullMemberRecordRecord.userId = user.id;
            FullMemberRecordRecord.createdAt = DateTime.Now;

            try
            {
                msg = DoValidation(FullMemberRecordRecord);

                IFullMemberRecordService rrs = new FullMemberRecordService(Settings.Default.db);
                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    bool isSucceed = rrs.Create(FullMemberRecordRecord);

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

        // GET: FullMemberRecord/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IFullMemberRecordService cs = new FullMemberRecordService(Settings.Default.db);

            FullMemberRecord fmr = cs.FindById(id);
            return View(fmr);
        }

        // POST: FullMemberRecord/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id, staffNr, isPassCheck, checkScore, beFullAt, remark")] FullMemberRecord fullMemberRecord)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(fullMemberRecord);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IFullMemberRecordService cs = new FullMemberRecordService(Settings.Default.db);
                    bool isSucceed = cs.Update(fullMemberRecord);

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

        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Approval([Bind(Include = "id,checkScore,isPassCheck,approvalStatus,approvalRemark")] FullMemberRecord fullMemberRecord)
        {
            ResultMessage msg = new ResultMessage();
            fullMemberRecord.approvalAt = System.DateTime.Now;
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            fullMemberRecord.approvalUserId = user.id;
            try
            {
                msg = DoValidation(fullMemberRecord);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IFullMemberRecordService raps = new FullMemberRecordService(Settings.Default.db);
                    bool isSucceed = raps.Update(fullMemberRecord);

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
        // GET: FullMemberRecordRecord/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IFullMemberRecordService raps = new FullMemberRecordService(Settings.Default.db);

            FullMemberRecord FullMemberRecordRecord = raps.FindById(id);
            //SetDropDownList(FullMemberRecordRecord);
            return View(FullMemberRecordRecord);
        }

        // POST: FullMemberRecordRecord/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();
            try
            {
                IFullMemberRecordService mrs = new FullMemberRecordService(Settings.Default.db);
                bool isSucceed = mrs.DeleteById(id);
                msg.Success = isSucceed;
                msg.Content = isSucceed ? "" : "删除失败";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]

        public ResultMessage DoValidation(FullMemberRecord model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.staffNr))
            {
                msg.Success = false;
                msg.Content = "员工号不能为空";
                return msg;
            }

            if (string.IsNullOrWhiteSpace(model.isPassCheck.ToString()))
            {
                msg.Success = false;
                msg.Content = "请填写是否通过";
                return msg;
            }

            return new ResultMessage() { Success = true, Content = "" };
        }
    }
}
