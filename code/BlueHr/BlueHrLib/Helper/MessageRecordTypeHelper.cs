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
            MessageRecordType.StaffToFullMemberAlert 
           // MessageRecordType.StaffCertificateAlert
        };

        public static List<MessageRecordType> UrlTypes = new List<MessageRecordType>() {
            MessageRecordType.StaffAttAlert,
            MessageRecordType.StaffToFullMemberAlert
        };

        /// <summary>
        /// {0},{1},{2}--->{staffNr},{staffName},{trialOverAt}
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public static string FormatToBeFullMemeberMsg(Staff staff)
        {
            return string.Format("{0},{1},{2}", staff.nr,staff.name, staff.trialOverAtStr);
        }

        public static string ParseToBeFullMemeberMsg(string msg)
        {
            return string.Format("{0}({1})转正日期为{2}, 请及时操作", msg.Split(','));
        }
        /// <summary>
        /// {0},{1},{2}--->{staffNr},{staffName},{attendanceDate}
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public static string FormatAttExceptionMsg(AttendanceRecordCalView record)
        {
            return string.Format("{0},{1},{2}", record.nr, record.name, record.attendanceDateStr);
        }

        public static string ParseAttExceptionMsg(string msg)
        {
            return string.Format("{0}({1}){2}的考勤存在异常,请进行操作", msg.Split(','));
        }


        //public static string FormatManageStaffMessage(MessageRecordType type, Staff staff)
    }
}
