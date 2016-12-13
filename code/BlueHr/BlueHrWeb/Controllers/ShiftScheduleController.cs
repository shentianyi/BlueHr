using BlueHrLib.Data;
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
using BlueHrLib.Helper.Excel;

namespace BlueHrWeb.Controllers
{
    public class ShiftScheduleController : Controller
    {
        // GET: ShiftShedule
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            ShiftScheduleSearchModel q = new ShiftScheduleSearchModel();

            //在员工管理-员工列表、排班管理-排班管理、缺勤管理、加班管理的列表中，用户如果有权限查看列表，那么只可以查看他所管理部门中的所有员工(员工中已有部门、公司)
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            q.lgUser = user;

            IShiftScheduleService ss = new ShiftSheduleService(Settings.Default.db);

            IPagedList<ShiftSchedule> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            ShiftScheduleInfoModel info = ss.GetShiftScheduleInfo(q);
            ViewBag.Info = info;

            SetDropDownList(null);

            return View(models);
        }

        public ActionResult Search([Bind(Include = "StaffNr, StaffNrAct, ScheduleAtFrom, ScheduleAtEnd")]  ShiftScheduleSearchModel q)
        {
            //在员工管理-员工列表、排班管理-排班管理、缺勤管理、加班管理的列表中，用户如果有权限查看列表，那么只可以查看他所管理部门中的所有员工(员工中已有部门、公司)
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            q.lgUser = user;

            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IShiftScheduleService ss = new ShiftSheduleService(Settings.Default.db);

            IPagedList<ShiftSchedule> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SetDropDownList(null);

            return View("Search", models);
        }

        // GET: ShiftShedule/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShiftShedule/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: ShiftShedule/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public ActionResult Create([Bind(Include = "shiftId,staffNr,scheduleAt")] ShiftSchedule model)
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
                    IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);
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
            //try
            //{
            //    // TODO: Add insert logic here 

            //    IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);

            //    //model.absenceDate = HttpContext.Request.Form["absenceDate"];
            //    cs.Create(model);
            //   return RedirectToAction("Index"); 
            //}
            //catch
            //{
            //    return View();
            //}
        }
        // POST: ShiftSchedule/EasyCreate
        [HttpPost]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult EasyCreate([Bind(Include = "shiftId,staffNr,scheduleAt")] ShiftSchedule model, DateTime startTime, DateTime endTime)
        {
            ResultMessage msg = new ResultMessage();
            try
            {
                if (Convert.IsDBNull(startTime))
                {
                    msg.Content = "起始日期 不能为空";
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                if (Convert.IsDBNull(endTime))
                {
                    msg.Content = "截止日期 不能为空";
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);


                bool isSucceed = cs.EasyCreate(model, startTime, endTime);
                msg.Success = isSucceed;
                msg.Content = isSucceed ? "添加成功" : "添加失败";
                //for (int i = 0; i < model.Length; i++)
                //{
                //    bool isSucceed = cs.EasyCreate(model[i], startTime, endTime);
                //    msg.Success = isSucceed;
                //    msg.Content += isSucceed ? "第" + i + "条添加成功  " : "第" + i + "条添加失败  ";
                //}  此段注释请不要删除！！！！！！！！！！！！！
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg.Content = "添加失败,失败原因：" + ex;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: ShiftSchedule/MoreEasyCreate
        [HttpPost]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult MoreEasyCreate(string[] staffNrs,int shiftId, DateTime startTime, DateTime endTime)
        {
            ResultMessage msg = new ResultMessage();
            try
            {
                if (Convert.IsDBNull(startTime))
                {
                    msg.Content = "起始日期 不能为空";
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                if (Convert.IsDBNull(endTime))
                {
                    msg.Content = "截止日期 不能为空";
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);
                for (int i = 0; i < staffNrs.Length; i++)
                {
                    ShiftSchedule shift = new ShiftSchedule();
                    shift.staffNr = staffNrs[i];
                    shift.shiftId = shiftId;
                    shift.scheduleAt = DateTime.Now;
                    bool isSucceed = cs.EasyCreate(shift, startTime, endTime);
                    msg.Success = isSucceed;
                    msg.Content += isSucceed ? "" : "第" + i+1 + "条添加失败  ";
                }
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg.Content = "添加失败,失败原因：" + ex;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ChangeShiftSchedule(int id)
        {
            IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);

            ShiftSchedule jt = cs.FindById(id);

            SetDropDownList(jt);
            
            return View(jt);
        } 

        // GET: ShiftShedule/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {

            IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);

            ShiftSchedule jt = cs.FindById(id);
            SetDropDownList(jt);
            return View(jt);
        }

        // POST: ShiftShedule/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,shiftId,staffNr,scheduleAt")] ShiftSchedule model)
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
                    IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);
                    bool isSucceed = cs.Update(model);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "添加失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            //try
            //{
            //    // TODO: Add update logic here
            //    IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);

            //    bool updateResult = cs.Update(model);
            //    if (!updateResult)
            //    {
            //        SetDropDownList(model);
            //        return View();
            //    }
            //    else
            //    {
            //        return RedirectToAction("Index");
            //    }

            //}
            //catch
            //{
            //    return View();
            //}
        }

        //// POST: ShiftShedule/Edit/5
        //[RoleAndDataAuthorizationAttribute]
        //[HttpPost]
        //public ActionResult EasyEdit(string[] staffNrs,DateTime scheduleAt, int shiftId)
        //{
        //    try
        //    {
        //        IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);
        //        ResultMessage msg = new ResultMessage();
        //        int i;
        //        for (i = 0; i <= staffNrs.Length; i++)
        //        {
        //            ShiftSchedule shift = cs.FindForEdit(staffNrs[i], scheduleAt, shiftId);
        //            shift.shiftId = shiftId;
        //            shift.scheduleAt = scheduleAt;
        //            bool isSucceed = cs.Update(shift);
        //            msg.Success = isSucceed;
        //            msg.Content += isSucceed ? "" : "第" + i + 1 + "条更新失败  ";
        //        }
        //        return Json(msg, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}


        // GET: ShiftShedule/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);

            ShiftSchedule cp = cs.FindById(id);
            SetDropDownList(cp);
            return View(cp);
        }


        // POST: ShiftShedule/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();
            try
            {
                // TODO: Add delete logic here
                IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);
                msg.Success = cs.DeleteById(id);
                return Json(msg, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                msg.Content = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: ShiftShedule/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult DeleteSchedule(int id)
        {
            ResultMessage msg = new ResultMessage();
            try
            {
                // TODO: Add delete logic here
                IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);
                msg.Success= cs.DeleteById(id);
                return Json(msg, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                msg.Content = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Import()
        {
            var ff = Request.Files[0];
            string fileName = BlueHrWeb.Helpers.FileHelper.SaveAsTmp(ff);
            ShiftScheduleExcelHelper helper = new ShiftScheduleExcelHelper(Settings.Default.db, fileName);
            ImportMessage msg = helper.Import();

            //添加"text/html",防止IE 自动下载json 格式返回的数据
            return Json(msg, "text/html");
        }

        [RoleAndDataAuthorizationAttribute]
        [HttpGet]
        public JsonResult GetAllShiftSchedule()
        {
            List<Dictionary<string, object>> Result = new List<Dictionary<string, object>>();

            IShiftScheduleService sss = new ShiftSheduleService(Settings.Default.db);
            IShiftService ss = new ShiftService(Settings.Default.db);

            ShiftScheduleSearchModel q = new ShiftScheduleSearchModel();

            List<ShiftScheduleView> shiftScheduleViews = sss.GetAllShiftSchedule();

            //可以进行分组显示
            var shiftScheduleGroups = shiftScheduleViews.GroupBy(c=>new {c.name, c.fullStartAt, c.endAt });

            foreach(var shiftScheduleGroup in shiftScheduleGroups)
            {
                Dictionary<string, object> calendar = new Dictionary<string, object>();

                calendar.Add("id", shiftScheduleGroup.Key.name + "_" + shiftScheduleGroup.Key.fullStartAt + "_" + shiftScheduleGroup.Key.endAt + "_" + shiftScheduleGroup.Count());
                calendar.Add("title", "~ " + shiftScheduleGroup.Key.endAt.Hours + ":" + shiftScheduleGroup.Key.endAt.Minutes + " -> " + shiftScheduleGroup.Key.name + "(共" + shiftScheduleGroup.Count() + "人)");
                calendar.Add("start", shiftScheduleGroup.Key.fullStartAt.ToString());

                List<Dictionary<string, string>> Childs = new List<Dictionary<string, string>>();

                foreach (var shiftSchedule in shiftScheduleGroup)
                {
                    Dictionary<string, string> sfs = new Dictionary<string, string>();

                    sfs.Add("parentId", shiftScheduleGroup.Key.name + "_" + shiftScheduleGroup.Key.fullStartAt + "_" + shiftScheduleGroup.Key.endAt + "_" + shiftScheduleGroup.Count());
                    sfs.Add("id", shiftSchedule.id.ToString());
                    sfs.Add("staffNr", shiftSchedule.staffNr);
                    sfs.Add("scheduleAt", shiftSchedule.scheduleAt.ToString("yyyy-MM-dd"));
                    sfs.Add("shiftId", shiftSchedule.shiftId.ToString());
                    sfs.Add("code", shiftSchedule.code);
                    sfs.Add("name", shiftSchedule.name);
                    sfs.Add("shiftType", shiftSchedule.shiftType.ToString());
                    sfs.Add("title", shiftSchedule.staffNr + " -> " + shiftSchedule.name);
                    sfs.Add("start", shiftSchedule.fullStartAt.Value.ToString("yyyy-MM-dd hh:mm:ss"));
                    sfs.Add("end", shiftSchedule.fullEndAt.Value.ToString("yyyy-MM-dd hh:mm:ss"));

                    Childs.Add(sfs);
                }

                calendar.Add("childrens", Childs);

                Result.Add(calendar);
            }

            ViewData["Shift_Schedule_Data"] = Result;

            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ResultMessage DoValidation(ShiftSchedule model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.staffNr))
            {
                msg.Success = false;
                msg.Content = "员工不能为空";

                return msg;
            }

            if (model.scheduleAt == null || model.scheduleAt == DateTime.MinValue)
            {
                msg.Success = false;
                msg.Content = "日期不能为空";

                return msg;
            }

            if (model.shiftId == 0)
            {
                msg.Success = false;
                msg.Content = "班次不能为空";

                return msg;
            }

            IStaffService ss = new StaffService(Settings.Default.db);
            if (ss.FindByNr(model.staffNr) == null)
            {
                msg.Success = false;
                msg.Content = "员工号不存在";

                return msg;
            }

            IShiftScheduleService cs = new ShiftSheduleService(Settings.Default.db);
            if (cs.IsDup(model))
            {
                msg.Success = false;
                msg.Content = "排班已存在，不可重复排班";

                return msg;
            }

            return new ResultMessage() { Success = true, Content = "" };
        }

        private void SetDropDownList(ShiftSchedule model)
        {
            if (model != null)
            {
                SetShiftScheduleList(model.shiftId);
                GetAllShifts(null);
             }
            else
            {
                SetShiftScheduleList(null);
                GetAllShifts(null);
             }
        }

        private void SetShiftScheduleList(int? type, bool allowBlank = false)
        {
            IShiftService cs = new ShiftService(Settings.Default.db);

            ShiftSearchModel csm = new ShiftSearchModel();

            List<Shift> certType = cs.Search(csm).ToList();

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var certt in certType)
            {
                if (type.HasValue && type.ToString().Equals(certt.id))
                {
                    select.Add(new SelectListItem { Text = certt.name, Value = certt.id.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = certt.name, Value = certt.id.ToString(), Selected = false });
                }
            }
            ViewData["shiftList"] = select;
        }

        private void GetAllShifts(int? type, bool allowBlank = false)
        {
            IShiftService cs = new ShiftService(Settings.Default.db);

            ShiftSearchModel shiftSearchModel= new ShiftSearchModel();

            List<Shift> shifts = cs.Search(shiftSearchModel).ToList();

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var shift in shifts)
            {
                if (type.HasValue && type.ToString().Equals(shift.id))
                {
                    select.Add(new SelectListItem { Text = shift.name + " (" + shift.startAt + "-" + shift.endAt + ")", Value = shift.id.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = shift.name + " (" + shift.startAt + "-" + shift.endAt + ")", Value = shift.id.ToString(), Selected = false });
                }
            }
            ViewData["shiftsList"] = select;
        }

        public JsonResult GetAllStaff()
        {
            List<Dictionary<string, object>> Result = new List<Dictionary<string, object>>();

            IStaffService ss = new StaffService(Settings.Default.db);

            StaffSearchModel staffSearchModel = new StaffSearchModel();

            var staffViews = ss.SearchView(staffSearchModel).GroupBy(c=> new { c.departmentName , c.companyName}).ToList();

            foreach (var staffView in staffViews)
            {
                Dictionary<string, object> staffGroup = new Dictionary<string, object>();
                staffGroup.Add("label", staffView.Key.companyName + "-" + staffView.Key.departmentName);
                staffGroup.Add("isParent", "true");

                List<Dictionary<string, string>> Childs = new List<Dictionary<string, string>>();

                foreach (var staffs in staffView)
                {
                    Dictionary<string, string> staff = new Dictionary<string, string>();

                    staff.Add("nr", staffs.nr);
                    staff.Add("name", staffs.name);
                    staff.Add("department", staffs.departmentName);
                    staff.Add("parentId", staffView.Key.companyName + "-" + staffView.Key.departmentName);

                    Childs.Add(staff);
                }

                staffGroup.Add("childrens", Childs);

                Result.Add(staffGroup);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);

        }
    }
}
