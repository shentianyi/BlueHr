using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data;
using BlueHrLib.Helper;
using Brilliantech.Framwork.Utils.LogUtil;

namespace BlueHrLib.Service.Implement
{
    public class MessageRecordService : ServiceBase, IMessageRecordService
    {
        public MessageRecordService(string dbString) : base(dbString) { }

        /// <summary>
        /// 创建信息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        /// <param name="type"></param>
        /// <param name="text"></param>
        /// <param name="uniqString">唯一性键</param>
        public void Create(string staffNr, int? operatorId, MessageRecordType type, string text,  string uniqString = null)
        {
            try
            {
                DataContext dc = new DataContext(this.DbString);
                bool isUniq = MessageRecordTypeHelper.UniqTypes.Contains(type);
                if (isUniq)
                {
                    MessageRecord unreadRecord = null;
                    var q = dc.Context.GetTable<MessageRecord>().Where(s => s.staffNr.Equals(staffNr) && (s.isRead == false) && s.messageType.Equals((int)type));
                    if (uniqString != null)
                    {
                        q = q.Where(s => uniqString==s.uniqString);
                    }

                    unreadRecord = q.FirstOrDefault();

                    if (unreadRecord != null)
                    {
                        return;
                    }
                }

                bool isUrl = MessageRecordTypeHelper.UrlTypes.Contains(type);
                MessageRecord record = new MessageRecord()
                {
                    createdAt = DateTime.Now,
                    staffNr = staffNr,
                    messageType = (int)type,
                    text = text,
                    operatorId = operatorId,
                    isRead = false,
                    isHandled = false,
                    isUrl = isUrl,
                    uniqString =uniqString
                };
                dc.Context.GetTable<MessageRecord>().InsertOnSubmit(record);
                dc.Context.SubmitChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Logger.Error("创建消息记录失败", ex);
            }
        }



        /// <summary>
        /// 创建员工转正提醒
        /// </summary>
        /// <param name="datetime"></param>
        public void CreateToFullMemberMessage(DateTime datetime)
        {
            List<Staff> staffs = new StaffService(this.DbString).GetToBeFullsLessThanDate(datetime);
            foreach (var staff in staffs)
            {
                Create(staff.nr, null, MessageRecordType.StaffToFullMemberAlert, MessageRecordTypeHelper.GetToBeFullMemeberMsg(staff));
            }
        }


        /// <summary>
        /// 创建员工考勤异常消息
        /// </summary>
        /// <param name="attendanceDate"></param>
        public void CreateAttExceptionMessage(DateTime attendanceDate)
        {
            IAttendanceRecordCalService service = new AttendanceRecordCalService(this.DbString);
            List<AttendanceRecordCalView> records = service.GetListByDateAndIsException(attendanceDate);
            foreach (var r in records)
            {
               
                Create(r.staffNr, null, MessageRecordType.StaffAttAlert, MessageRecordTypeHelper.GetAttExceptionMsg(r),attendanceDate.ToString("yyyy-MM-dd"));
            }
        }
    }
}
