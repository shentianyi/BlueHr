using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.CustomAttributes;
using BlueHrWeb.Properties;
using BlueHrLib.Helper;
using BlueHrLib.Data.Enum;

namespace BlueHrWeb.Controllers
{
    public class SystemSettingController : Controller
    {
        // GET: SystemSetting

        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index()
        {
            ISystemSettingService ss = new SystemSettingService(Settings.Default.db);
            SystemSetting setting = ss.Find();

            return View(setting);
        }

        [HttpPost]
        public ActionResult UpdateBasic([Bind(Include = "daysBeforeAlertStaffGoFull ，defaultTrailMonth,goFullAlertMails ,unCertifacteAlertMails,attendanceExceptionAlertMails,repeatAttendanceRecordTime,validAttendanceRecordTime,lateExceptionTime,earlyLeaveExceptionTime,systemHost,emaiSMTPHost,emailUser,emailPwd,emailAddress")] SystemSetting setting)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                ISystemSettingService cs = new SystemSettingService(Settings.Default.db);
                bool isSucceed = cs.Update(setting);

                msg.Success = isSucceed;
                msg.Content = isSucceed ? "" : "保存成功";

                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            ISystemSettingService at = new SystemSettingService(Settings.Default.db);

            var SystemSetting = at.GetAllTableName();

            if (SystemSetting != null)
            {
                //获取当前记录的属性
                foreach (var property in SystemSetting[0].GetType().GetProperties())
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
