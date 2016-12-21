using BlueHrLib.Data;
using BlueHrLib.Data.Message;
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
using BlueHrLib.Helper.Excel;

namespace BlueHrWeb.Controllers
{
    public class WorkAndRestsController : Controller
    {
        // GET: JobTitle 
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            WorkAndRestSearchModel q = new WorkAndRestSearchModel();

            IWorkAndRestService wrs = new WorkAndRestService(Settings.Default.db);

            IPagedList<WorkAndRest> workAndRests = wrs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SetWorkAndRestTypeList(null);
            SetAllTableName(null);
            SetSearchConditions(null);

            return View(workAndRests);
        }

        [RoleAndDataAuthorization]
        [UserAuthorize]
        public ActionResult TableShow(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            WorkAndRestSearchModel q = new WorkAndRestSearchModel();

            IWorkAndRestService wrs = new WorkAndRestService(Settings.Default.db);

            IPagedList<WorkAndRest> workAndRests = wrs.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SetWorkAndRestTypeList(null);
            SetAllTableName(null);
            SetSearchConditions(null);

            return View(workAndRests);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "DateAtFrom, DateAtTo, DateType")] WorkAndRestSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IWorkAndRestService ss = new WorkAndRestService(Settings.Default.db);

            IPagedList<WorkAndRest> workAndRests = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SetWorkAndRestTypeList(q.DateType);
            SetAllTableName(null);
            SetSearchConditions(null);
            return View("Index", workAndRests);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Import()
        {
            var ff = Request.Files[0];
            string fileName = Helpers.FileHelper.SaveAsTmp(ff);
            WorkAndRestExcelHelper helper = new WorkAndRestExcelHelper(Settings.Default.db, fileName);
            ImportMessage msg = helper.Import();

            //添加"text/html",防止IE 自动下载json 格式返回的数据
            return Json(msg, "text/html");
        }

        [AdminAuthorize]
        // GET: WorkAndRest/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            SetWorkAndRestTypeList(null);
            return View();
        }

        // POST: WorkAndRest/Create
        [HttpPost]
        [RoleAndDataAuthorizationAttribute]
        public JsonResult Create([Bind(Include = "dateAt,dateType,remark")] WorkAndRest workAndRest)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(workAndRest);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IWorkAndRestService wrs = new WorkAndRestService(Settings.Default.db);

                    if (!wrs.HasDateAtExist(workAndRest.dateAt))
                    {
                        msg.Success = false;
                        msg.Content = "日期已存在，不可重复添加， 请进行编辑";
                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        bool isSucceed = wrs.Create(workAndRest);

                        msg.Success = isSucceed;
                        msg.Content = isSucceed ? "添加成功" : "添加失败";

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [AdminAuthorize]
        [RoleAndDataAuthorizationAttribute]
        // GET: WorkAndRest/Edit/5
        public ActionResult Edit(int id)
        {
            IWorkAndRestService wrs = new WorkAndRestService(Settings.Default.db);

            WorkAndRest workAndRest = wrs.FindById(id);
            SetWorkAndRestTypeList(workAndRest.dateType);
            return View(workAndRest);
        }


        // POST: WorkAndRest/Edit/5
        [HttpPost]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit([Bind(Include = "id,dateAt,dateType,remark")] WorkAndRest workAndRest)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(workAndRest);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IWorkAndRestService wrs = new WorkAndRestService(Settings.Default.db);
                    bool isSucceed = wrs.Update(workAndRest);

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

        [AdminAuthorize]
        [RoleAndDataAuthorizationAttribute]
        // GET: WorkAndRest/Delete/5
        public ActionResult Delete(int id)
        {
            IWorkAndRestService wrs = new WorkAndRestService(Settings.Default.db);

            WorkAndRest workAndRest= wrs.FindById(id);
            SetWorkAndRestTypeList(workAndRest.dateType);
            return View(workAndRest);
        }

        // POST: WorkAndRest/Delete/5
        [HttpPost]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                IWorkAndRestService wrs = new WorkAndRestService(Settings.Default.db);
                bool isSucceed = wrs.DeleteById(id);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "删除成功" : "删除失败";

                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [RoleAndDataAuthorization]
        public JsonResult GetWorkAndRest(DateTime? dateAtFrom, DateTime? dateAtTo, int? dateTypeId)
        {
            List<Dictionary<string, string>> Result = new List<Dictionary<string, string>>();
            IWorkAndRestService wrs = new WorkAndRestService(Settings.Default.db);

            WorkAndRestSearchModel q = new WorkAndRestSearchModel();
            q.DateAtFrom = dateAtFrom;
            q.DateAtTo = dateAtTo;
            q.DateType = dateTypeId;

            List<WorkAndRest> workAndRests = wrs.Search(q).ToList();

            foreach(var workAndRest in workAndRests)
            {
                Dictionary<string, string> days = new Dictionary<string, string>();
                days.Add("id", workAndRest.id.ToString());
                days.Add("title", workAndRest.dateTypeDisplay);
                days.Add("allDay", "true");
                days.Add("dateAt", workAndRest.dateAt.ToString());
                days.Add("dateType", workAndRest.dateTypeDisplay);
                days.Add("dateTypeId", workAndRest.dateType.ToString());
                if(workAndRest.dateType == 200)
                {
                    days.Add("backgroundColor", "#f1c40f");
                    days.Add("textColor", "#222d32");
                }
                else if(workAndRest.dateType == 300)
                {
                    days.Add("backgroundColor", "#e74c3c");
                }else
                {
                    //days.Add("backgroundColor", "#e74c3c");
                }
                days.Add("start", workAndRest.dateAt.ToString());
                days.Add("remark", workAndRest.remark);

                Result.Add(days);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [RoleAndDataAuthorization]
        [HttpPost]
        public JsonResult SetWorkAndRestDay(DateTime beginDay, DateTime endDay)
        {
            IWorkAndRestService wrs = new WorkAndRestService(Settings.Default.db);
            bool Result = false;
            int Days = new TimeSpan(endDay.Ticks).Subtract(new TimeSpan(beginDay.Ticks)).Duration().Days;
            for(var currentDay = 0; currentDay < Days+1; currentDay++)
            {
                WorkAndRest workAndRest = new WorkAndRest();

                DateTime CurrentTime = beginDay.AddDays(currentDay);

                workAndRest.dateAt = CurrentTime;

                if (CurrentTime.Month == 1 && CurrentTime.Day >= 1 && CurrentTime.Day <= 3)
                {
                    //国庆节
                    workAndRest.dateType = 300;
                    workAndRest.remark = "元旦/节假日";
                }
                else if (CurrentTime.Month == 5 && CurrentTime.Day >= 1 && CurrentTime.Day <= 3)
                {
                    //国庆节
                    workAndRest.dateType = 300;
                    workAndRest.remark = "劳动节/节假日";
                }
                else if (CurrentTime.Month == 10 && CurrentTime.Day>=1 && CurrentTime.Day<= 7)
                {
                    //国庆节
                    workAndRest.dateType = 300;
                    workAndRest.remark = "国庆节/节假日";
                }
                else
                {
                    if (CurrentTime.DayOfWeek == DayOfWeek.Saturday || CurrentTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        //双休日
                        workAndRest.dateType = 200;
                        workAndRest.remark = "双休日/周末/公休日";
                    }
                    else
                    {
                        //工作日
                        workAndRest.dateType = 100;
                        workAndRest.remark = "工作日";
                    }
                }

                Result = wrs.Create(workAndRest);

                if (!Result)
                {
                    break;
                }
            }

            return Json(Result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [RoleAndDataAuthorization]
        public JsonResult EditWorkAndRest(int id, int dateTypeId, string remark)
        {
            IWorkAndRestService wrs = new WorkAndRestService(Settings.Default.db);

            WorkAndRest workAndRest = wrs.FindById(id);

            workAndRest.dateType = dateTypeId;
            workAndRest.remark = remark;

            bool Result = wrs.Update(workAndRest);

            return Json(Result, JsonRequestBehavior.DenyGet);
        }

        private void SetWorkAndRestTypeList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(WorkAndRestType));

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
            ViewData["workAndRestTypeList"] = select;
        }

        [HttpPost]
        // 日期不能为空， 也不能重复
        // 类型不能为空
        public ResultMessage DoValidation(WorkAndRest model)
        {
            ResultMessage msg = new ResultMessage();

            if (!model.dateAt.HasValue)
            {
                msg.Success = false;
                msg.Content = "日期不能为空";

                return msg;
            }
         
            if (!model.dateType.HasValue)
            {
                msg.Success = false;
                msg.Content = "类型不能为空";

                return msg;
            }

            return new ResultMessage() { Success = true, Content = "" };
        }

        private void SetAllTableName(string type, bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IWorkAndRestService wars = new WorkAndRestService(Settings.Default.db);

            var WorkAndRest = wars.GetAllTableName();
            if (WorkAndRest.Count == 0)
            {
                WorkAndRest wartemp = new WorkAndRest();
                WorkAndRest.Add(wartemp);
            }
            //获取当前记录的属性
            int i = 1;
            string showName = "";
            foreach (var property in WorkAndRest[0].GetType().GetProperties())
            {
                switch (i)
                {
                    case 1: showName = "ID"; break;
                    case 2: showName = "日期"; break;
                    case 3: showName = "类型"; break;
                    case 4: showName = "备注"; break;
                    default:
                        break;
                }
                if (!string.IsNullOrWhiteSpace(type) && type.Equals(property.Name)&&i<=4)
                {
                    select.Add(new SelectListItem { Text = showName, Value = property.Name, Selected = true });
                }
                else if(i<=4)
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


        public ActionResult AdvancedSearch(WorkAndRestSearchModel q)
        {
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            q.loginUser = user;
            ViewBag.Query = q;

            IWorkAndRestService ss = new WorkAndRestService(Settings.Default.db);
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IPagedList<WorkAndRest> WorkAndRests = null;
            IQueryable<WorkAndRest> WorkAndResttemp = null;
            IQueryable<WorkAndRest> WorkAndResttemp1 = null;
            List<WorkAndRest> Result = new List<WorkAndRest>();
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
                            WorkAndResttemp1 = ss.AdvancedSearch(AllTableNameArray[0], SearchConditionsArray[0], searchValueFirstArray[0])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                            if (AllTableNameArray.Length > 1)
                            {
                                int i = 1;
                                WorkAndResttemp = ss.AdvancedSearch(AllTableNameArray[i], SearchConditionsArray[i], searchValueFirstArray[i])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                foreach (var temp in WorkAndResttemp)
                                {
                                    if (WorkAndResttemp1.FirstOrDefault(s => s.id.Equals(temp.id)) != null) Result.Add(temp);
                                }
                                if (AllTableNameArray.Length > 2)
                                {
                                    for (var i1 = 2; i1 < AllTableNameArray.Length; i1++)
                                    {
                                        IQueryable<WorkAndRest> WorkAndResttemp2 = null;
                                        WorkAndResttemp2 = ss.AdvancedSearch(AllTableNameArray[i1], SearchConditionsArray[i1], searchValueFirstArray[i1])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                        List<WorkAndRest> Resulttemp = new List<WorkAndRest>();

                                        foreach (var addtemp in Result)
                                        {
                                            Resulttemp.Add(addtemp);
                                        }

                                        foreach (var temp in Result)
                                        {
                                            if (WorkAndResttemp2.FirstOrDefault(s => s.id.Equals(temp.id)) == null)
                                            {
                                                WorkAndRest removetemp = temp;
                                                Resulttemp.Remove(Resulttemp.Where(s => s.id == removetemp.id).FirstOrDefault());
                                            }
                                        }
                                        Result = Resulttemp;
                                    }
                                }
                            }
                            else
                            {
                                Result = WorkAndResttemp1.ToList();
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
                WorkAndRests = Result.Distinct().ToPagedList(pageIndex, Settings.Default.pageSize);
            }catch { WorkAndRests = null; }

            return View("Index", WorkAndRests);
        }
    }
}
