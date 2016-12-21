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
    public class ShiftController : Controller
    {
        // GET: Shift
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            ShiftSearchModel q = new ShiftSearchModel();

            IShiftService ss = new ShiftService(Settings.Default.db);

            IPagedList<Shift> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            ShiftInfoModel info = ss.GetShiftInfo(q);
            ViewBag.Info = info;
            SetAllTableName(null);
            SetSearchConditions(null);
            return View(models);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "Name")]  ShiftSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IShiftService ss = new ShiftService(Settings.Default.db);

            IPagedList<Shift> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;
            SetAllTableName(null);
            SetSearchConditions(null);
            return View("Index", models);
        }


        // GET: Shift/Details/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Shift/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: Shift/Create 
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "code,name,startAt,endAt,shiftType,remark")] Shift model)
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
                    IShiftService cs = new ShiftService(Settings.Default.db);
                    bool isSucceed = cs.Create(model);

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

        // GET: Shift/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IShiftService cs = new ShiftService(Settings.Default.db);

            Shift jt = cs.FindById(id);
            SetDropDownList(jt);
            return View(jt);
        }

        // POST: Shift/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id,code,name,startAt,endAt,shiftType,remark")] Shift model)
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
                    IShiftService cs = new ShiftService(Settings.Default.db);
                    bool isSucceed = cs.Update(model);

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

        // GET: Shift/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IShiftService cs = new ShiftService(Settings.Default.db);

            Shift cp = cs.FindById(id);
            SetDropDownList(cp);
            return View(cp);
        }

        // POST: Shift/Delete/5
        [HttpPost]
        //如果存在员工排班，则不可删除
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                IShiftScheduleService shfSi = new ShiftSheduleService(Settings.Default.db);
                ShiftSchedule shf = shfSi.FindShiftScheduleByShiftId(id);

                if (null != shf && shf.id > 0)
                {
                    msg.Success = false;
                    msg.Content = "班次信息正在使用,不能删除!";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IShiftService cs = new ShiftService(Settings.Default.db);
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
        //•	名称（输入，不可空，唯一）
        //•	代码（输入，不可空，唯一）
        //•	开始时间（选择，时间，不可空，如06:00）
        //•	班次类型（选择，不可空，选项包含：今日/次日，默认今日）
        //•	截止时间（选择，时间，不可空，如06:00）
        //•	备注（输入，可空） 
        public ResultMessage DoValidation(Shift model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.name))
            {
                msg.Success = false;
                msg.Content = "名称不能为空";

                return msg;
            }

            if (string.IsNullOrEmpty(model.code))
            {
                msg.Success = false;
                msg.Content = "代码不能为空";

                return msg;
            }

            if (model.startAt.ToString() == "00:00:00")
            {
                msg.Success = false;
                msg.Content = "开始时间不能为空";

                return msg;
            }

            if (model.endAt.ToString() == "00:00:00")
            {
                msg.Success = false;
                msg.Content = "截止时间不能为空";

                return msg;
            }

            if (string.IsNullOrEmpty(model.shiftType.ToString()) || model.shiftType == 0)
            {
                msg.Success = false;
                msg.Content = "班次类型不能为空";

                return msg;
            }

            if (model.shiftType == 100 && 
                Convert.ToInt32(model.endAt.Hours.ToString()) <= Convert.ToInt32(model.startAt.Hours.ToString()) && 
                Convert.ToInt32(model.endAt.Minutes.ToString()) <= Convert.ToInt32(model.startAt.Minutes.ToString()))
            {
                msg.Success = false;
                msg.Content = "上班时间错误，请检查";

                return msg;
            }

            IShiftService cs = new ShiftService(Settings.Default.db);
            List<Shift> shift = cs.All();

            if (model.id <= 0)
            {
                bool isRecordExists = shift.Where(p => p.code == model.code && p.name == model.name).ToList().Count() > 0;

                if (isRecordExists)
                {
                    msg.Success = false;
                    msg.Content = "数据已经存在!";

                    return msg;
                }
            }
            else
            {
                bool isRecordExists = shift.Where(p => p.code == model.code && p.name == model.name && p.id != model.id).ToList().Count() > 0;

                if (isRecordExists)
                {
                    msg.Success = false;
                    msg.Content = "数据已经存在!";

                    return msg;
                }
            }

            return new ResultMessage() { Success = true, Content = "" };
        }

        private void SetDropDownList(Shift model)
        {
            if (model != null)
            {
                SetShiftList(model.shiftType);
            }
            else
            {
                SetShiftList(null);
            }

            SetAllTableName(null);
            SetSearchConditions(null);
        }

        private void SetShiftList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(ShiftType));

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
            ViewData["shiftTypeList"] = select;
        }

        private void SetAllTableName(string type, bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IShiftService ss = new ShiftService(Settings.Default.db);

            var Shifts = ss.GetAllTableName();
            if (Shifts.Count == 0)
            {
                Shift tempShift = new Shift();
                Shifts.Add(tempShift);
            }
            //获取当前记录的属性
            int i = 1;
            string showName = "";
            foreach (var property in Shifts[0].GetType().GetProperties())
            {
                switch (i)
                {
                    case 1: showName = "ID"; break;
                    case 2: showName = "代码"; break;
                    case 3: showName = "名称"; break;
                    case 4: showName = "开始时间"; break;
                    case 5: showName = "结束时间"; break;
                    case 6: showName = "班次类型"; break;
                    case 7: showName = "备注"; break;
                    default:
                        break;
                }
                if (!string.IsNullOrWhiteSpace(type) && type.Equals(property.Name)&&i<=7)
                {
                    select.Add(new SelectListItem { Text = showName, Value = property.Name, Selected = true });
                }
                else if (i<=7)
                {
                    select.Add(new SelectListItem { Text = showName, Value = property.Name, Selected = false });
                }
                i++;
            }

            ViewData["getAllTableNameList"] = select;
        }

        private void SetSearchConditions(int? type, bool allowBlank = false)
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
        public ActionResult AdvancedSearch(ShiftSearchModel q)
        {
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            q.loginUser = user;
            ViewBag.Query = q;

            IShiftService ss = new ShiftService(Settings.Default.db);
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IPagedList<Shift> Shifts = null;
            IQueryable<Shift> Shifttemp = null;
            IQueryable<Shift> Shifttemp1 = null;
            List<Shift> Result = new List<Shift>();
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
                            Shifttemp1 = ss.AdvancedSearch(AllTableNameArray[0], SearchConditionsArray[0], searchValueFirstArray[0])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                            if (AllTableNameArray.Length > 1)
                            {
                                int i = 1;
                                Shifttemp = ss.AdvancedSearch(AllTableNameArray[i], SearchConditionsArray[i], searchValueFirstArray[i])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                foreach (var temp in Shifttemp)
                                {
                                    if (Shifttemp1.FirstOrDefault(s => s.id.Equals(temp.id)) != null) Result.Add(temp);
                                }
                                if (AllTableNameArray.Length > 2)
                                {
                                    for (var i1 = 2; i1 < AllTableNameArray.Length; i1++)
                                    {
                                        IQueryable<Shift> Shifttemp2 = null;
                                        Shifttemp2 = ss.AdvancedSearch(AllTableNameArray[i1], SearchConditionsArray[i1], searchValueFirstArray[i1])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                        List<Shift> Resulttemp = new List<Shift>();

                                        foreach (var addtemp in Result)
                                        {
                                            Resulttemp.Add(addtemp);
                                        }

                                        foreach (var temp in Result)
                                        {
                                            if (Shifttemp2.FirstOrDefault(s => s.id.Equals(temp.id)) == null)
                                            {
                                                Shift removetemp = temp;
                                                Resulttemp.Remove(Resulttemp.Where(s => s.id == removetemp.id).FirstOrDefault());
                                            }
                                        }
                                        Result = Resulttemp;
                                    }
                                }
                            }
                            else
                            {
                                Result = Shifttemp1.ToList();
                            }
                        }
                        catch (Exception)
                        {
                            Result = null;
                        }

                    }
                }
            }
            try
            {
                Shifts = Result.Distinct().ToPagedList(pageIndex, Settings.Default.pageSize);
            }catch { Shifts = null; }
            SetDropDownList(null);

            return View("Index", Shifts);
        }
    }
}
