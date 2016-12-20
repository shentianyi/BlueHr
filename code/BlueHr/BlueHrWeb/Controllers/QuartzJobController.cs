using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.MQTask;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.CustomAttributes;
using BlueHrWeb.Properties;
using BlueHrLib.Helper;

namespace BlueHrWeb.Controllers
{
    public class QuartzJobController : Controller
    {
        // GET: QuartzJob
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index()
        {
            IQuartzJobService js = new QuartzJobService(Settings.Default.db);
            List<QuartzJob> jobs = js.GetByType(CronJobType.CalAtt);
            return View(jobs);
        }


        // GET: QuartzJob/Create
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Create()
        {
            SetShiftScheduleList(null);
            return View();
        }

        // POST: QuartzJob/Create
        [HttpPost]
        //[RoleAndDataAuthorizationAttribute]
        public JsonResult Create([Bind(Include = "cronSchedule,params")] QuartzJob job)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                job.jobType = (int)CronJobType.CalAtt;

                msg = DoValidation(job);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IQuartzJobService cs = new QuartzJobService(Settings.Default.db);
                    bool isSucceed = cs.Create(job);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "添加成功" : "添加失败";

                    // 重启任务服务
                    TaskDispatcher dtt = new TaskDispatcher(Settings.Default.queue);
                    dtt.SendRestartSvcMessage();

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        // POST: QuartzJob/Delete/5
        [HttpPost]
        //[RoleAndDataAuthorizationAttribute]
        //如果存在员工排班，则不可删除
        public ActionResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                IQuartzJobService js = new QuartzJobService(Settings.Default.db);
                bool isSucceed = js.Delete(id);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "删除成功" : "删除失败";
                // 重启任务服务
                TaskDispatcher dtt = new TaskDispatcher(Settings.Default.queue);
                dtt.SendRestartSvcMessage();
                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
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
                    select.Add(new SelectListItem { Text = certt.name, Value = certt.code.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = certt.name, Value = certt.code.ToString(), Selected = false });
                }
            }
            ViewData["shiftList"] = select;
        }


        public ResultMessage DoValidation(QuartzJob model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.cronSchedule))
            {
                msg.Success = false;
                msg.Content = "执行时间不能为空";

                return msg;
            }
            if (string.IsNullOrEmpty(model.@params))
            {
                msg.Success = false;
                msg.Content = "班次代码不能为空";

                return msg;
            }



            return new ResultMessage() { Success = true, Content = "" };
        }

        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IQuartzJobService at = new QuartzJobService(Settings.Default.db);

            var QuartzJob = at.GetAllTableName();

            if (QuartzJob != null)
            {
                //获取当前记录的属性
                foreach (var property in QuartzJob[0].GetType().GetProperties())
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