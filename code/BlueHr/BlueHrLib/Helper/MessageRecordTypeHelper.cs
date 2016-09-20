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
            return string.Format("{0}", staff.trialOverAtStr);
        }


        public static string ParseMsg(MessageRecordView record)
        {
            string[] texts = record.text.Split(',');
            switch ((MessageRecordType)record.messageType)
            {
                case MessageRecordType.StaffToFullMemberAlert:
                    return string.Format("{0}({1})转正日期为{2}, 请及时操作", record.staffNr, record.staffName, record.text);
                case MessageRecordType.StaffAttAlert:
                    return string.Format("{0}({1}){2}的考勤存在异常,请进行操作", record.staffNr, record.staffName, record.text);
                case MessageRecordType.StaffBasicEdited:
                    return string.Format("{0}在{1}将{2}({3})的{4}从{5}改为{6}", record.operatorName, record.createdAtStr, record.staffNr, record.staffName, texts[0], texts[1], texts[2]);
                case MessageRecordType.StaffToFullMemeber:
                    return string.Format("{0}在{1}对{2}({3})进行了转正", record.operatorName, record.createdAtStr, record.staffNr, record.staffName);
                case MessageRecordType.StaffResign:
                    return string.Format("{0}在{1}对{2}({3})进行了离职", record.operatorName, record.createdAtStr, record.staffNr, record.staffName);
                case MessageRecordType.StaffShiftJob:
                    return string.Format("{0}在{1}对{2}({3})进行了调岗,从{4}调至{5}", record.operatorName, record.createdAtStr, record.staffNr, record.staffName, texts[0], texts[1]);
                case MessageRecordType.StaffUpdateAttHour:
                    return string.Format("{0}在{1}对{2}({3})进行了考勤调整,工作日工时从{4}调至{5}，加班工时从{6}调至{7}", record.operatorName, record.createdAtStr, record.staffNr, record.staffName, texts[0], texts[1],texts[2],texts[3]);
                case MessageRecordType.StaffIdCheck:
                    return string.Format("在{0}对{1}({2})进行了身份证验证", record.createdAtStr, record.staffNr, record.staffName);
            }
            return string.Empty;
        }

       
        /// <summary>
        /// {0},{1},{2}--->{staffNr},{staffName},{attendanceDate}
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public static string FormatAttExceptionMsg(AttendanceRecordCalView record)
        {
            return string.Format("{0}", record.attendanceDateStr);
        }

  

        /// <summary>
        /// 格式化管理员工的消息内容
        /// </summary>
        /// <param name="staffNr"></param>
        /// <returns></returns>
        public static string FormatManageStaffMsg(string staffNr, params string[] texts)
        {
            string p = string.Empty;

            foreach (var s in texts)
            {
                p += s + ",";
            }
            return p.TrimEnd(',');
        }
     

       
    }
}
