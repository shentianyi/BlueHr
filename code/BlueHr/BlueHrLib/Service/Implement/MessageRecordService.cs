using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data;
using BlueHrLib.Helper;
using Brilliantech.Framwork.Utils.LogUtil;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;

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
        public void Create(string staffNr, int? operatorId, MessageRecordType type, string text, string uniqString = null)
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
                        q = q.Where(s => uniqString == s.uniqString);
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
                    uniqString = uniqString
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
        /// 创建员工基础信息被编辑消息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        /// <param name="fieldName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void CreateStaffBasicEdited(string staffNr, int operatorId, string fieldName, string oldValue, string newValue)
        {
            Create(staffNr, operatorId, MessageRecordType.StaffBasicEdited, MessageRecordTypeHelper.FormatManageStaffMsg(staffNr, fieldName, oldValue, newValue));
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
                Create(staff.nr, null, MessageRecordType.StaffToFullMemberAlert, MessageRecordTypeHelper.FormatToBeFullMemeberMsg(staff), staff.trialOverAtStr);
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

                Create(r.staffNr, null, MessageRecordType.StaffAttAlert, MessageRecordTypeHelper.FormatAttExceptionMsg(r), attendanceDate.ToString("yyyy-MM-dd"));
            }
        }


        /// <summary>
        /// 创建员工转正消息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        public void CreateStaffFullMemeberMessage(string staffNr, int operatorId)
        {
            Create(staffNr, operatorId, MessageRecordType.StaffToFullMemeber, MessageRecordTypeHelper.FormatManageStaffMsg(staffNr));
        }

        /// <summary>
        /// 创建员工离职消息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        public void CreateStaffResignMessage(string staffNr, int operatorId)
        {
            Create(staffNr, operatorId, MessageRecordType.StaffResign, MessageRecordTypeHelper.FormatManageStaffMsg(staffNr));
        }


        /// <summary>
        /// 创建员工调岗消息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        public void CreateStaffShiftJobMessage(string staffNr, int operatorId, string oldJobStr, string newJobStr)
        {
            Create(staffNr, operatorId, MessageRecordType.StaffShiftJob, MessageRecordTypeHelper.FormatManageStaffMsg(staffNr, oldJobStr, newJobStr));
        }

        /// <summary>
        /// 创建员工调整考勤消息
        /// </summary>
        /// <param name="staffNr"></param>
        public void CreateStaffUpdateAttHourMessage(string staffNr, int operatorId, string oldHour, string newHour)
        {
            Create(staffNr, operatorId, MessageRecordType.StaffUpdateAttHour, MessageRecordTypeHelper.FormatManageStaffMsg(staffNr, oldHour, newHour));
        }

        /// <summary>
        /// 创建员工身份证验证消息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        public void CreateStaffIdCheckMessage(string staffNr)
        {
            Create(staffNr, null, MessageRecordType.StaffIdCheck, MessageRecordTypeHelper.FormatManageStaffMsg(staffNr));
        }

        /// <summary>
        /// 获取未读的消息数量
        /// </summary>
        /// <param name="read"></param>
        /// <returns></returns>
        public int CountUnRead(bool read = false)
        {
            IMessageRecordRepository rep = new MessageRecordRepository(new DataContext(this.DbString));

            return rep.CountUnRead();
        }

        /// <summary>
        /// 根据类型和是否阅读获取列表
        /// </summary>
        /// <param name="types"></param>
        /// <param name="all"></param>
        /// <returns></returns>
        public IQueryable<MessageRecordView> GetByTypesAndAllOrUnread(List<MessageRecordType> types, bool all)
        {
            DataContext dc = new DataContext(this.DbString);
            IQueryable<MessageRecordView> q = dc.Context.GetTable<MessageRecordView>();
            if (!all)
            {
                q = q.Where(s => s.isRead == false);
            }
            if (types != null && types.Count > 0)
            {
                q = q.Where(s => types.Contains((MessageRecordType)s.messageType));
            }

            return q.OrderByDescending(s => s.createdAt);
        }
    }
}
