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
    public class RewardsAndPenaltyController : Controller
    {
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            RewardsAndPenaltySearchModel q = new RewardsAndPenaltySearchModel();

            IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);

            IPagedList<RewardsAndPenalty> rewardsAndPenalties = raps.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            RewardsAndPenaltyInfoModel info = raps.GetRewardsAndPenaltyInfo(q);
            ViewBag.Info = info;

            return View(rewardsAndPenalties);
        }

        [RoleAndDataAuthorizationAttribute]
        public ActionResult Search([Bind(Include = "Name")] RewardsAndPenaltySearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);

            IPagedList<RewardsAndPenalty> rewardsAndPenalties = raps.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", rewardsAndPenalties);
        }


        // GET: RewardsAndPenalty/Details/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RewardsAndPenalty/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: RewardsAndPenalty/Create
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Create([Bind(Include = "staffNr,type,project,description")] RewardsAndPenalty rewardsAndPenalties)
        {
            ResultMessage msg = new ResultMessage();
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            try
            {
                msg = DoValidation(rewardsAndPenalties);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    rewardsAndPenalties.createdUserId = user.id;
                    rewardsAndPenalties.createdAt = System.DateTime.Now;
                    IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);
                    bool isSucceed = raps.Create(rewardsAndPenalties);
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

        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Approval([Bind(Include = "id,approvalStatus,approvalRemark")] RewardsAndPenalty rewardsAndPenalties)
        {
            ResultMessage msg = new ResultMessage();
            User user = System.Web.HttpContext.Current.Session["user"] as User;
            try
            {
                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    rewardsAndPenalties.approvalUserId = user.id;
                    IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);
                    bool isSucceed = raps.Update(rewardsAndPenalties);
                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "审批成功" : "审批失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: RewardsAndPenalty/Edit/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Edit(int id)
        {
            IRewardsAndPenaltyService cs = new RewardsAndPenaltyService(Settings.Default.db);

            RewardsAndPenalty rewardsAndPenalties = cs.FindById(id);

            SetDropDownList(rewardsAndPenalties);

            return View(rewardsAndPenalties);
        }

        [HttpGet]
        public JsonResult detail(string staffNr)
        {
            IRewardsAndPenaltyService rps = new RewardsAndPenaltyService(Settings.Default.db);
            List<Dictionary<string, string>> Result = new List<Dictionary<string, string>>();
            foreach (var i in rps.FindBystaffNr(staffNr))
            {
                Dictionary<string, string> detail = new Dictionary<string, string>();
                detail.Add("奖惩类型", i.type == 1?"奖励":"惩罚");
                detail.Add("奖惩项目", i.project);
                detail.Add("奖惩描述", i.description);
                detail.Add("奖惩日期", i.createdAt.ToString());
                IUserService us = new UserService(Settings.Default.db);
                try
                {
                    detail.Add("审批人", us.FindById((int)i.approvalUserId).name);
                }
                catch { detail.Add("审批人", null); }
                Result.Add(detail);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        // POST: RewardsAndPenalty/Edit/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id,staffNr,type,project,description")] RewardsAndPenalty rewardsAndPenalties)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(rewardsAndPenalties);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);
                    bool isSucceed = raps.Update(rewardsAndPenalties);

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

        // GET: RewardsAndPenalty/Delete/5
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Delete(int id)
        {
            IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);

            RewardsAndPenalty rewardsAndPenalties = raps.FindById(id);
            SetDropDownList(rewardsAndPenalties);
            return View(rewardsAndPenalties);
        }

        // POST: RewardsAndPenalty/Delete/5
        [RoleAndDataAuthorizationAttribute]
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();
            try
            {
                IRewardsAndPenaltyService raps = new RewardsAndPenaltyService(Settings.Default.db);
                bool isSucceed = raps.DeleteById(id);
                msg.Success = isSucceed;
                msg.Content = isSucceed ? "" : "删除失败";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private void SetDropDownList(RewardsAndPenalty rewardsAndPenalty)
        {
            if (rewardsAndPenalty != null)
            {
                SetTypeList(rewardsAndPenalty.type);
            }
            else
            {
                SetTypeList(null);
            }
        }

        private void SetTypeList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(RewardsAndPenaltyType));

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
            ViewData["rewardsAndPenaltiesTypeList"] = select;
        }

        [HttpPost]

        public ResultMessage DoValidation(RewardsAndPenalty model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.staffNr))
            {
                msg.Success = false;
                msg.Content = "员工号不能为空";
                return msg;
            }
            if (model.type == null)
            {
                msg.Success = false;
                msg.Content = "奖惩类别不能为空";
                return msg;
            }
            return new ResultMessage() { Success = true, Content = "" };
        }

       
    }
}
