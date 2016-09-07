using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Helper
{
    public class MessageRecordTypeHelper
    {
        public static List<MessageRecordType> UniqTypes = new List<MessageRecordType>() {
            MessageRecordType.StaffAttAlert,
            MessageRecordType.StaffToFullMemberAlert,
            MessageRecordType.StaffCertificateAlert
        };

        public static List<MessageRecordType> UrlTypes = new List<MessageRecordType>() {
            MessageRecordType.StaffAttAlert,
            MessageRecordType.StaffToFullMemberAlert
        };

        public static string GetToBeFullMemeberMsg(Staff staff)
        {
            return string.Format("员工:{0}({1})的转正日期为{2},请进行操作", staff.nr, staff.name, staff.trialOverAtStr);
        }
        public static string GetAttExceptionMsg(AttendanceRecordCalView record)
        {
            return string.Format("员工:{0}({1}){2}的考勤存在异常,请进行操作", record.nr, record.name, record.attendanceDateStr);
        }
    }
}
