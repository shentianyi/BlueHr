using BlueHrLib.CusException;
using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Message;
using BlueHrLib.Helper;
using System.Collections.Specialized;

namespace BlueHrLib.Service.Implement
{
    public class AttendanceRecordCalService : ServiceBase, IAttendanceRecordCalService
    {
        private AttendanceRecordCalRepository attendanceRecordCalRepo;

        public AttendanceRecordCalService(string dbString) : base(dbString)
        {
            attendanceRecordCalRepo = new AttendanceRecordCalRepository(this.Context);
        }

        public AttendanceRecordCal FindById(int id)
        {
            IAttendanceRecordCalRepository rep = new AttendanceRecordCalRepository(new DataContext(this.DbString));
            return rep.FindById(id);
        }


        public AttendanceRecordCalView FindViewById(int id)
        {
            IAttendanceRecordCalViewRepository rep = new AttendanceRecordCalViewRepository(new DataContext(this.DbString));
            return rep.FindById(id);
        }



        /// <summary>
        /// 搜索统计考勤信息视图, 包含员工的信息
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IQueryable<AttendanceRecordCalView> SearchCalView(AttendanceRecordCalSearchModel searchModel)
        {
            IAttendanceRecordCalViewRepository rep = new AttendanceRecordCalViewRepository(new DataContext(this.DbString));
            return rep.Search(searchModel);
        }

        /// <summary>
        ///  根据统计记录id调整时间工时
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actHour">实际工时</param>
        /// <param name="isExceptionHandled">是否处理了异常</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public ResultMessage UpdateActHourById(int id, double actHour, double actExtraHour, bool isExceptionHandled,string remark, int? extraWorkType)
        {
            ResultMessage msg = new ResultMessage();
            try {
                DataContext dc = new DataContext(this.DbString);

                AttendanceRecordCal record = dc.Context.GetTable<AttendanceRecordCal>().FirstOrDefault(s => s.id.Equals(id));
                if (record == null)
                {
                    throw new DataNotFoundException();
                }

                record.actWorkingHour = actHour;
                record.actExtraWorkingHour = actExtraHour;
                record.isManualCal = true;
                record.isExceptionHandled = isExceptionHandled;
                record.remark = remark;
                record.extraworkType = extraWorkType;
                dc.Context.SubmitChanges();
                msg.Success = true;
            } catch(Exception ex)
            {
                msg.Content = ex.Message;
            }
            return msg;
        }

        /// <summary>
        /// 根据时间获取未处理的异常统计列表
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="endDate"></param>
        /// <param name="exceptionHandled"></param>
        /// <returns></returns>
        public List<AttendanceRecordCalExceptionView> GetCalExceptionHandleList(DateTime fromDate, DateTime endDate, bool exceptionHandled = false)
        {
            DataContext dc = new DataContext(this.DbString);

            return dc.Context.GetTable<AttendanceRecordCalExceptionView>()
                .Where(s => s.attendanceDate >= fromDate && s.attendanceDate <= endDate && s.isExceptionHandled==exceptionHandled)
                .OrderByDescending(s=>s.attendanceDate).ToList();
        }

        /// <summary>
        /// 发送考勤异常提醒邮件
        /// </summary>
        /// <param name="date"></param>
        /// <param name="shiftCodes"></param>
        public void SendWarnEmail(DateTime date, List<string> shiftCodes=null)
        {
            DataContext dc = new DataContext(this.DbString);

            AttendanceRecordCalExceptionView record = dc.Context.GetTable<AttendanceRecordCalExceptionView>().
                FirstOrDefault(s => s.attendanceDate.Equals(date) && s.isExceptionHandled == false);

            if (record != null && record.isExceptionHandledCount > 0)
            {
                SystemSetting setting = dc.Context.GetTable<SystemSetting>().FirstOrDefault();

                if (setting == null)
                    throw new SystemSettingNotSetException();
                Dictionary<string,string> values = new Dictionary<string,string>();
                values.Add("date", date.ToString("yyyy-MM-dd"));
                values.Add("count", record.isExceptionHandledCount.Value.ToString());
                values.Add("host", setting.systemHost);
                values.Add("shiftDesc", (shiftCodes == null || shiftCodes.Count == 0) ? "" : string.Join(",", shiftCodes.ToArray())+" 班");
                string body = EmailHelper.Build("AttendanceWarn.html", values);

                EmailHelper.SendEmail(setting.emaiSMTPHost,
                    setting.emailUser,
                   setting.emailAddress,
                   setting.emailPwd,
                   setting.attendanceExceptionAlertMails,
                   "考勤异常提醒",
                   body,true);
            }

        }

        /// <summary>
        /// 根据时间、是否异常、是否异常处理获取考勤统计列表
        /// </summary>
        /// <param name="attendanceDate">考勤日期</param>
        /// <param name="isException">是否异常</param>
        /// <param name="isExceptionHandled">是否已异常处理</param>
        /// <returns></returns>
        public List<AttendanceRecordCalView> GetListByDateAndIsException(DateTime attendanceDate, bool isException = true, bool isExceptionHandled = false)
        {
            DataContext dc = new DataContext(this.DbString);

            return dc.Context.GetTable<AttendanceRecordCalView>().Where(s => s.attendanceDate.Equals(attendanceDate) 
            && s.isException.Equals(isException) 
            && s.isExceptionHandled.Equals(isExceptionHandled)).ToList();
            
        }

        public bool Create(AttendanceRecordCal attendanceRecordCal)
        {
            return attendanceRecordCalRepo.Create(attendanceRecordCal);
        }

        public bool DeleteById(int id)
        {
            return attendanceRecordCalRepo.DeleteById(id);
        }

        public List<AttendanceRecordCal> GetAllTableName()
        {
            return attendanceRecordCalRepo.GetAllTableName();
        }
    }
}

