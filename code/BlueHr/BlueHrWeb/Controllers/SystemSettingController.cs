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
    }
}