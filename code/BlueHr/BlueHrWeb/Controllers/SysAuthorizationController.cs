using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Helper;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.CustomAttributes;
using BlueHrWeb.Helpers;
using BlueHrWeb.Properties;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueHrWeb.Controllers
{
    public class SysAuthorizationController : Controller
    {
        // GET: SystemAuthorization
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            SysAuthorizationSearchModel q = new SysAuthorizationSearchModel();

            ISysAuthorizationService ss = new SysAuthorizationService(Settings.Default.db);

            IPagedList<SysAuthorization> jobTitles = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SysAuthorizationInfoModel info = ss.GetSysAuthorizationInfo(q);
            ViewBag.Info = info;
            SetAllTableName(null);
            SetSearchConditions(null);
            return View(jobTitles);
        }

        [RoleAndDataAuthorizationAttribute]

        public ActionResult Search([Bind(Include = "Name,funCode,controlName,actionName")] SysAuthorizationSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            ISysAuthorizationService ss = new SysAuthorizationService(Settings.Default.db);

            IPagedList<SysAuthorization> jobTitles = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;
            SetAllTableName(null);
            SetSearchConditions(null);
            return View("Index", jobTitles);
        }

        // GET: SystemAuthorization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [RoleAndDataAuthorizationAttribute]
        // GET: SystemAuthorization/Create
        public ActionResult Create()
        {
            return View();
        }

        [RoleAndDataAuthorizationAttribute]
        // POST: SystemAuthorization/Create
        [HttpPost]
        public JsonResult Create([Bind(Include = "name,controlName,actionName,isDelete,parentId,funCode,remarks")] SysAuthorization jobTitle)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(jobTitle);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ISysAuthorizationService cs = new SysAuthorizationService(Settings.Default.db);

                    //if (!string.IsNullOrEmpty(Request.Form["jobCertificateType"]))
                    //{
                    //    //拼接Job Certificate Type
                    //    string jobCerts = this.HttpContext.Request.Form["jobCertificateType"];

                    //    jobCerts.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(p =>
                    //    {
                    //        jobTitle.JobCertificate.Add(new JobCertificate()
                    //        {
                    //            certificateTypeId = int.Parse(p),
                    //            jobTitleId = jobTitle.id
                    //        });
                    //    });
                    //}

                    jobTitle.isDelete = 0;

                    bool isSucceed = cs.Create(jobTitle);

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

        // GET: SystemAuthorization/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            ISysAuthorizationService cs = new SysAuthorizationService(Settings.Default.db);
            SysAuthorization jt = cs.FindById(id);
            return View(jt);
        }

        // POST: SystemAuthorization/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id,name,controlName,actionName,isDelete,parentId,funCode,remarks")] SysAuthorization jobTitle)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(jobTitle);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //string jobCerts = this.HttpContext.Request.Form["jobCertificateType"];

                    ISysAuthorizationService cs = new SysAuthorizationService(Settings.Default.db);
                    bool isSucceed = cs.Update(jobTitle);

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

        // GET: SystemAuthorization/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            ISysAuthorizationService cs = new SysAuthorizationService(Settings.Default.db);
            SysAuthorization cp = cs.FindById(id);
            return View(cp);
        }

        // POST: SystemAuthorization/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                ////存在员工时不可删除
                //IStaffService shfSi = new StaffService(Settings.Default.db);
                //List<Staff> shf = shfSi.FindByJobTitleId(id);

                //if (null != shf && shf.Count() > 0)
                //{
                //    msg.Success = false;
                //    msg.Content = "职位信息正在使用,不能删除!";

                //    return Json(msg, JsonRequestBehavior.AllowGet);
                //}
                //else
                {
                    ISysAuthorizationService cs = new SysAuthorizationService(Settings.Default.db);
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

        public ResultMessage DoValidation(SysAuthorization model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.name))
            {
                msg.Success = false;
                msg.Content = "权限名称不能为空";

                return msg;
            }

            ISysAuthorizationService cs = new SysAuthorizationService(Settings.Default.db);
            List<SysAuthorization> shift = cs.GetAll();

            if (model.id <= 0)
            {
                bool isRecordExists = shift.Where(p => p.name == model.name).ToList().Count() > 0;

                if (isRecordExists)
                {
                    msg.Success = false;
                    msg.Content = "数据已经存在!";

                    return msg;
                }
            }
            else
            {
                bool isRecordExists = shift.Where(p => p.name == model.name && p.id != model.id).ToList().Count() > 0;

                if (isRecordExists)
                {
                    msg.Success = false;
                    msg.Content = "数据已经存在!";

                    return msg;
                }
            }

            return new ResultMessage() { Success = true, Content = "" };
        }
        private void SetAllTableName(string type, bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            ISysAuthorizationService sas = new SysAuthorizationService(Settings.Default.db);

            var SysAuthorization = sas.GetAllTableName();

            if (SysAuthorization != null)
            {
                //获取当前记录的属性
                foreach (var property in SysAuthorization[0].GetType().GetProperties())
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


        public ActionResult AdvancedSearch(SysAuthorizationSearchModel q)
        {
            {
                User user = System.Web.HttpContext.Current.Session["user"] as User;
                q.loginUser = user;
                ViewBag.Query = q;

                ISysAuthorizationService sas = new SysAuthorizationService(Settings.Default.db);
                int pageIndex = 0;
                int.TryParse(Request.QueryString.Get("page"), out pageIndex);
                pageIndex = PagingHelper.GetPageIndex(pageIndex);

                IPagedList<SysAuthorization> sysAuthorizations = null;
                IQueryable<SysAuthorization> sysAuthorizationstemp = null;
                IQueryable<SysAuthorization> sysAuthorizationstemp1 = null;
                List<SysAuthorization> Result = new List<SysAuthorization>();
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
                            sysAuthorizationstemp1 = sas.AdvancedSearch(AllTableNameArray[0], SearchConditionsArray[0], searchValueFirstArray[0])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                            if (AllTableNameArray.Length > 1)
                            {
                                for (var i = 1; i < AllTableNameArray.Length; i++)
                                {
                                    sysAuthorizationstemp = sas.AdvancedSearch(AllTableNameArray[i], SearchConditionsArray[i], searchValueFirstArray[i])/*.ToPagedList(pageIndex, Settings.Default.pageSize)*/;
                                    foreach (var temp in sysAuthorizationstemp)
                                    {
                                        if (sysAuthorizationstemp1.FirstOrDefault(s => s.id.Equals(temp.id)) != null) Result.Add(temp);
                                    }
                                }
                            }
                            else
                            {
                                sysAuthorizations = sysAuthorizationstemp1.ToPagedList(pageIndex, Settings.Default.pageSize);
                            }
                        }
                    }
                }
                sysAuthorizations = Result.ToPagedList(pageIndex, Settings.Default.pageSize);

                return View("Index", sysAuthorizations);
            }
        }
    }
}
