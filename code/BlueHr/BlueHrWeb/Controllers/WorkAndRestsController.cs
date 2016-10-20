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
                    }

                    bool isSucceed = wrs.Create(workAndRest);

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
    }
}
