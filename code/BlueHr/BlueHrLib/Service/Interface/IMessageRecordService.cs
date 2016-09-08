using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
    public interface IMessageRecordService
    {
        /// <summary>
        /// 创建信息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        /// <param name="type"></param>
        /// <param name="text"></param>
        /// <param name="uniqString">唯一性键</param>
        void Create(string staffNr, int? operatorId, MessageRecordType type, string text,string uniqString=null);

        /// <summary>
        /// 创建员工转正提醒
        /// </summary>
        /// <param name="datetime"></param>
        void CreateToFullMemberMessage(DateTime datetime);

        /// <summary>
        /// 创建员工考勤异常消息
        /// </summary>
        /// <param name="attendanceDate"></param>
        void CreateAttExceptionMessage(DateTime attendanceDate);

        /// <summary>
        /// 创建管理员工消息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="staff"></param>
        /// <param name="operatorId"></param
        void CreateManageStaffMessage(MessageRecordType type, Staff staff, int? operatorId);
    }
}
