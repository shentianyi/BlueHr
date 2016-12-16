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
    public class LeaveRecordController : Controller
    {
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            LeaveRecordSearchModel q = new LeaveRecordSearchModel();

            ILeaveRecordService lrs = new LeaveRecordService(Settings.Default.db);

            IPagedList<LeaveRecord> leaveRecords = lrs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View(leaveRecords);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "staffNr")] LeaveRecordSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            ILeaveRecordService rrs = new LeaveRecordService(Settings.Default.db);

            IPagedList<LeaveRecord> leaveRecords = rrs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", leaveRecords);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeaveRecord/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            //SetDropDownList(null);
            return View();
        }

        // POST: LeaveRecord/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "staffNr, leaveStart, leaveEnd, remark")] LeaveRecord leaveRecord)
        {
            ResultMessage msg = new ResultMessage();

            User user = System.Web.HttpContext.Current.Session["user"] as User;
            leaveRecord.userId = user.id;

            leaveRecord.createdAt = DateTime.Now;
            leaveRecord.isDelete = false;
            try
            {
                msg = DoValidation(leaveRecord);

                ILeaveRecordService lrs = new LeaveRecordService(Settings.Default.db);
                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.DenyGet);
                }
                else
                {

                    bool isSucceed = lrs.Create(leaveRecord);

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

        // GET: LeaveRecord/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            ILeaveRecordService rrs = new LeaveRecordService(Settings.Default.db);

            return View(rrs.FindById(id));
        }

        // POST: LeaveRecord/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id, staffNr, leaveStart, leaveEnd, remark")] LeaveRecord leaveRecord)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(leaveRecord);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ILeaveRecordService cs = new LeaveRecordService(Settings.Default.db);
                    bool isSucceed = cs.Update(leaveRecord);

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

        // POST: LeaveRecord/SetIsDelete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult SetIsDelete(int id)
        {
            ResultMessage msg = new ResultMessage();
            try
            {
                ILeaveRecordService cs = new LeaveRecordService(Settings.Default.db);
                LeaveRecord leaveRecord = cs.FindById(id);
                leaveRecord.isDelete = true;
                bool isSucceed = cs.Update(leaveRecord);
                msg.Success = isSucceed;
                msg.Content = isSucceed ? "销假成功" : "销假失败";

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: LeaveRecord/Approval/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Approval([Bind(Include = "id, approvalStatus, approvalRemark")] LeaveRecord leaveRecord)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                User user = System.Web.HttpContext.Current.Session["user"] as User;
                leaveRecord.approvalAt = DateTime.Now;
                leaveRecord.approvalUserId = user.id;
                ILeaveRecordService lrs = new LeaveRecordService(Settings.Default.db);
                bool isSucceed = lrs.Update(leaveRecord);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "" : "更新失败";

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: LeaveRecord/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            ILeaveRecordService lrs = new LeaveRecordService(Settings.Default.db);

            LeaveRecord lr = lrs.FindById(id);
            //SetDropDownList(cp);
            return View(lr);
        }

        //[HttpGet]
        //public JsonResult CalculateTime(int id)
        //{
        //    ILeaveRecordService lrs = new LeaveRecordService(Settings.Default.db);
        //    DateTime caculated = lrs.CalculateTime(id);
        //    return Json(caculated, JsonRequestBehavior.AllowGet);
        //}


        // POST: LeaveRecord/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                ILeaveRecordService lrs = new LeaveRecordService(Settings.Default.db);
                bool isSucceed = lrs.DeleteById(id);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "删除成功" : "删除失败";

                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult AdvancedSearch(LeaveRecordSearchModel q)
        {
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            q.loginUser = user;
            ViewBag.Query = q;

            ILeaveRecordService ss = new LeaveRecordService(Settings.Default.db);
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IPagedList<LeaveRecord> LeaveRecords = null;
            IQueryable<LeaveRecord> LeaveRecordtemp = null;
            IQueryable<LeaveRecord> LeaveRecordtemp1 = null;
            List<LeaveRecord> Result = new List<LeaveRecord>();
            if (!string.IsNullOrEmpty(Request.Form["allTableName"]))
            {
                if (!string.IsNullOrEmpty(Request.Form["searchConditions"]))
                {
                    if (!string.IsNullOrEmpty(Request.Form.Get("searchValueFirst")))
                    {
                        string AllTableName = Request.Form["allTableName"].ToString();
                        string[] AllTableNameArray = AllTableName.Split(',');
                        string SearchConditions = Request.Form["searchConditions"];
                        string[] SearchConditionsArray = SearchConditions.Split(',');
                        string searchValueFirst = Request.Form["searchValueFirst"];
                        string[] searchValueFirstArray = searchValueFirst.Split(',');

                        try
                        {
                            LeaveRecordtemp1 = ss.AdvancedSearch(AllTableNameArray[0], SearchConditionsArray[0], searchValueFirstArray[0])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                            if (AllTableNameArray.Length > 1)
                            {
                                int i = 1;
                                LeaveRecordtemp = ss.AdvancedSearch(AllTableNameArray[i], SearchConditionsArray[i], searchValueFirstArray[i])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                foreach (var temp in LeaveRecordtemp)
                                {
                                    if (LeaveRecordtemp1.FirstOrDefault(s => s.id.Equals(temp.id)) != null) Result.Add(temp);
                                }
                                if (AllTableNameArray.Length > 2)
                                {
                                    for (var i1 = 2; i1 < AllTableNameArray.Length; i1++)
                                    {
                                        IQueryable<LeaveRecord> LeaveRecordtemp2 = null;
                                        LeaveRecordtemp2 = ss.AdvancedSearch(AllTableNameArray[i1], SearchConditionsArray[i1], searchValueFirstArray[i1])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                        foreach (var temp in Result)
                                        {
                                            if (LeaveRecordtemp2.FirstOrDefault(s => s.id.Equals(temp.id)) == null) Result.Remove(temp);
                                        }

                                    }
                                }
                            }
                            else
                            {
                                Result = LeaveRecordtemp1.ToList();
                            }
                        }
                        catch (Exception)
                        {
                            LeaveRecords = null;
                        }

                    }
                }
            }
            LeaveRecords = Result.Distinct().ToPagedList(pageIndex, Settings.Default.pageSize);

            return View("Index", LeaveRecords);
        }


        [HttpPost]
        public ResultMessage DoValidation(LeaveRecord leaveRecord)
        {
            ResultMessage msg = new ResultMessage();

            if (leaveRecord.leaveStart == null)
            {
                msg.Success = false;
                msg.Content = "请假开始时间不可为空";

                return msg;
            }
            if (leaveRecord.leaveEnd == null)
            {
                msg.Success = false;
                msg.Content = "请假结束时间不可为空";

                return msg;
            }

            if (string.IsNullOrEmpty(leaveRecord.staffNr))
            {
                msg.Success = false;
                msg.Content = "请假员工不能为空";

                return msg;
            }

            IStaffService ss = new StaffService(Settings.Default.db);
            if (ss.FindByNr(leaveRecord.staffNr) == null)
            {
                msg.Success = false;
                msg.Content = "员工号不存在";

                return msg;
            }
            return new ResultMessage() { Success = true, Content = "" };
        }



        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            ILeaveRecordService lrs = new LeaveRecordService(Settings.Default.db);
            LeaveRecordSearchModel lrsm = null;
            var LeaveRecord = lrs.Search(lrsm).ToList();

            if (LeaveRecord != null)
            {
                //获取当前记录的属性
                foreach (var property in LeaveRecord[0].GetType().GetProperties())
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
