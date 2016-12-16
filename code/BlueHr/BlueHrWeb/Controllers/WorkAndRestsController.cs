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

        [RoleAndDataAuthorizationAttribute]

        public ActionResult Search([Bind(Include = "DateAtFrom,DateAtTo, DateType")] WorkAndRestSearchModel q)
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
                        msg.Content = isSucceed ? "" : "添加失败";

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
                    msg.Content = isSucceed ? "" : "更新失败";

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
                msg.Content = isSucceed ? "" : "删除失败";

                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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
            foreach (var property in WorkAndRest[0].GetType().GetProperties())
            {
                if (!string.IsNullOrWhiteSpace(type) && type.Equals(property.Name))
                {
                    select.Add(new SelectListItem { Text = property.Name, Value = property.Name, Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = property.Name, Value = property.Name, Selected = false });
                }

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
                                        foreach (var temp in Result)
                                        {
                                            if (WorkAndResttemp2.FirstOrDefault(s => s.id.Equals(temp.id)) == null) Result.Remove(temp);
                                        }

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
                            WorkAndRests = null;
                        }

                    }
                }
            }
            WorkAndRests = Result.Distinct().ToPagedList(pageIndex, Settings.Default.pageSize);

            return View("Index", WorkAndRests);
        }
    }
}
