using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.MQTask;
using BlueHrLib.CusException;
using BlueHrLib.Data.Enum;

namespace BlueHrLib.Service.Implement
{
    public class SystemSettingService : ServiceBase, ISystemSettingService
    {
        public SystemSettingService(string dbString) : base(dbString) { }

        public SystemSetting Find()
        {
            DataContext dc = new DataContext(this.DbString);

            SystemSetting setting = dc.Context.GetTable<SystemSetting>().FirstOrDefault();
            if (setting == null)
                throw new SystemSettingNotSetException();
            return setting;
        }

        public bool Update(SystemSetting setting)
        {
            DataContext dc = new DataContext(this.DbString);

            SystemSetting oldsetting = dc.Context.GetTable<SystemSetting>().FirstOrDefault();
            if (oldsetting == null)
                throw new SystemSettingNotSetException();


            oldsetting.daysBeforeAlertStaffGoFull = setting.daysBeforeAlertStaffGoFull;
            oldsetting.goFullAlertMails = setting.goFullAlertMails;
            oldsetting.unCertifacteAlertMails = setting.unCertifacteAlertMails;
            oldsetting.attendanceExceptionAlertMails = setting.attendanceExceptionAlertMails;
            oldsetting.repeatAttendanceRecordTime = setting.repeatAttendanceRecordTime;
            oldsetting.validAttendanceRecordTime = setting.validAttendanceRecordTime;
            oldsetting.lateExceptionTime = setting.lateExceptionTime;
            oldsetting.earlyLeaveExceptionTime = setting.earlyLeaveExceptionTime;
            oldsetting.systemHost = setting.systemHost;
            oldsetting.emaiSMTPHost = setting.emaiSMTPHost;
            oldsetting.emailUser = setting.emailUser;
            oldsetting.emailPwd = setting.emailPwd;
            oldsetting.emailAddress = setting.emailAddress;

            dc.Context.SubmitChanges();

            return true;
        }
    }
}
