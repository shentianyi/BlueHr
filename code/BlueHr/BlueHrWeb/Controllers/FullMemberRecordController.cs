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
            GetAllOnTrailStaff();
            return View();
        }

        // POST: FullMemberRecord/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "staffNr, isPassCheck, checkScore, beFullAt, remark")] FullMemberRecord fullMemberRecord)
        {
            ResultMessage msg = new ResultMessage();
            
            //填充 是谁创建的
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            fullMemberRecord.userId = user.id;
            fullMemberRecord.createdAt = DateTime.Now;

            try
            {
                //是否通过了数据验证
                msg = DoValidation(fullMemberRecord);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    IFullMemberRecordService fmrs = new FullMemberRecordService(Settings.Default.db);

                    if(fmrs.FindByNr(fullMemberRecord.staffNr) != null && !fmrs.FindByNr(fullMemberRecord.staffNr).approvalUserId.HasValue)
                    {
                        //员工已经递交申请， 但是还没通过审批， 不可以重复递交 申请
                        msg.Success = false;
                        msg.Content = "该员工已经递交申请, 请等待审核";

                        return Json(msg, JsonRequestBehavior.DenyGet);
                    }

                    bool isSucceed = fmrs.Create(fullMemberRecord);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "转正申请提交成功" : "转正申请提交失败，请检查";

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
            GetAllOnTrailStaff();
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

                IFullMemberRecordService rrs = new FullMemberRecordService(Settings.Default.db);
                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    IFullMemberRecordService cs = new FullMemberRecordService(Settings.Default.db);
                    bool isSucceed = cs.Update(fullMemberRecord);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "转正申请更新成功" : "转正申请更新失败，请检查";

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
        public JsonResult Approval([Bind(Include = "id, approvalStatus, approvalRemark")] FullMemberRecord fullMemberRecord)
        {
            ResultMessage msg = new ResultMessage();

            User user = System.Web.HttpContext.Current.Session["user"] as User;

            try
            {
                IFullMemberRecordService fmrs = new FullMemberRecordService(Settings.Default.db);

                //先通过ID找到对应的数据， 进行更新
                FullMemberRecord fullMemRecord = fmrs.FindById(fullMemberRecord.id);

                fullMemRecord.approvalAt = System.DateTime.Now;
                fullMemRecord.approvalUserId = user.id;
                fullMemRecord.approvalStatus = fullMemberRecord.approvalStatus;
                fullMemRecord.approvalRemark = fullMemberRecord.approvalRemark;

                bool isSucceed = fmrs.Update(fullMemRecord);

                //判断是否通过， 如果通过审批，将变为正式员工， 如果不通过审批，还是试用期员工
                IStaffService ss = new StaffService(Settings.Default.db);
                Staff toFullMemberStaff = ss.FindByNr(fullMemRecord.staffNr);

                if (fullMemRecord.approvalStatus == "通过审批")
                {
                    //变为正式员工
                    toFullMemberStaff.isOnTrial = false;
                    isSucceed = ss.Update(toFullMemberStaff);
                }
               
                // 创建转正记录##User##
                IMessageRecordService mrs = new MessageRecordService(Settings.Default.db);

                string content = "审批人：" + user.name + "在" + DateTime.Now + "对" + toFullMemberStaff.name + "(" + toFullMemberStaff.nr + ")" +
                    "进行了转正审批操作， 转正审批状态： " + fullMemRecord.approvalStatus + ", 转正审批备注： " + fullMemRecord.approvalRemark;

                mrs.CreateStaffFullMemeberMessage(toFullMemberStaff.nr, user.id, content);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "转正申请审批成功" : "转正申请审批失败";

                return Json(msg, JsonRequestBehavior.AllowGet);
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
            GetAllOnTrailStaff();
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
                msg.Content = isSucceed ? "转正记录删除成功" : "转正记录删除失败";
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

            IStaffService ss = new StaffService(Settings.Default.db);
            if (ss.FindByNr(model.staffNr) == null)
            {
                msg.Success = false;
                msg.Content = "员工号不存在";

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

        private void GetAllOnTrailStaff(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IStaffService ss = new StaffService(Settings.Default.db);
            StaffSearchModel q = new StaffSearchModel();

            var OnTrialStaff = ss.Search(q).Where(c=>c.isOnTrial);

            if (OnTrialStaff != null)
            {
                //获取当前记录的属性
                foreach (var onTrailStaff in OnTrialStaff)
                {
                    select.Add(new SelectListItem { Text = onTrailStaff.nr +" -> "+ onTrailStaff.name, Value = onTrailStaff.nr });
                }
            }

            ViewData["onTrialStaff"] = select;
        }

    }
}
