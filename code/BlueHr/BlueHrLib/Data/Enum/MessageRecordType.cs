using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Enum
{
    public enum MessageRecordType
    {
        ///
        /// ---------------------------------------员工数据修改---------------------------------------
        ///
        /// <summary>
        /// 员工创建
        /// </summary>
        [Description("员工创建")]
        StaffCreated=101,
        /// <summary>
        /// 员工删除
        /// </summary>
        [Description("员工删除")]
        StaffDeleted =102,
        /// <summary>
        /// 员工基础信息修改
        /// </summary>
        [Description("员工基础信息修改")]
        StaffBasicEdited =103,
        /// <summary>
        /// 员工银行卡改变
        /// </summary>
        [Description("员工银行卡改变")]
        StaffBankEdited =104,
        /// <summary>
        /// 员工家庭成员改变
        /// </summary>
        [Description("员工家庭成员改变")]
        StaffFamilyEdited =105,

        ///
        /// ---------------------------------------员工操作---------------------------------------
        /// 
        /// <summary>
        /// 员工转正
        /// </summary>
        [Description("员工转正")]
        StaffToFullMemeber =201,
        /// <summary>
        /// 员工离职
        /// </summary>
        [Description("员工离职")]
        StaffResign =202,
        /// <summary>
        /// 员工调岗
        /// </summary>
        [Description("员工调岗")]
        StaffShiftJob =203,
        /// <summary>
        /// 员工调整考勤
        /// </summary>
        [Description("员工调整考勤")]
        StaffUpdateAttHour =204,


        ///
        /// ---------------------------------------提醒---------------------------------------
        ///
        /// <summary>
        /// 员工考勤异常提醒
        /// </summary>
        [Description("员工考勤异常提醒")]
        StaffAttAlert =301,
        ///// <summary>
        ///// 员工证件照提醒
        ///// </summary>
        //[Description("员工证件照提醒")]
        //StaffCertificateAlert = 302,
        /// <summary>
        /// 员工转正提醒
        /// </summary>
        [Description("员工转正提醒")]
        StaffToFullMemberAlert =303



            
    }
}
