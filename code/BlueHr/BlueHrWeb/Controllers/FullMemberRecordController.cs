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
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            FullMemberRecordSearchModel q = new FullMemberRecordSearchModel();

            IFullMemberRecordService raps = new FullMemberRecordService(Settings.Default.db);

            IPagedList<FullMemberRecord> fullMemberRecord = raps.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            FullMemberRecordInfoModel info = raps.GetFullMemberRecordInfo(q);
            ViewBag.Info = info;

            return View(fullMemberRecord);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "staffNr")] FullMemberRecordSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IFullMemberRecordService raps = new FullMemberRecordService(Settings.Default.db);

            IPagedList<FullMemberRecord> fullMemberRecord = raps.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", fullMemberRecord);
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
            //SetDropDownList(null);
            return View();
        }

        // POST: FullMemberRecord/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "staffNr,isPassCheck,checkScore,beFullAt,remark,checkAt,beFullChecker,approvalAt,approvalStatus,approvalRemark,createdAt,userId")] FullMemberRecord fullMemberRecord)
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
                    IFullMemberRecordService raps = new FullMemberRecordService(Settings.Default.db);
                    bool isSucceed = raps.Create(fullMemberRecord);
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

        // GET: FullMemberRecord/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IFullMemberRecordService cs = new FullMemberRecordService(Settings.Default.db);

            FullMemberRecord fullMemberRecord = cs.FindById(id);

            //SetDropDownList(fullMemberRecord);

            return View(fullMemberRecord);
        }

        
        // POST: FullMemberRecord/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "staffNr,isPassCheck,checkScore,beFullAt,remark,checkAt,beFullChecker,approvalAt,approvalStatus,approvalRemark,createdAt,userId")] FullMemberRecord fullMemberRecord)
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
                    IFullMemberRecordService raps = new FullMemberRecordService(Settings.Default.db);
                    bool isSucceed = raps.Update(fullMemberRecord);

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

        // GET: FullMemberRecord/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IFullMemberRecordService raps = new FullMemberRecordService(Settings.Default.db);

            FullMemberRecord fullMemberRecord = raps.FindById(id);
            //SetDropDownList(fullMemberRecord);
            return View(fullMemberRecord);
        }

        // POST: FullMemberRecord/Delete/5
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
            
            return new ResultMessage() { Success = true, Content = "" };
        }


    }
}
