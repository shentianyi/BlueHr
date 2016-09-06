using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Enum
{
    public enum MessageRecordType
    {
        [Description("员工创建")]
        StaffCreated=101,
        [Description("员工删除")]
        StaffDeleted =102,
        [Description("员工基础信息修改")]
        StaffBasicEdited =103,
        [Description("员工银行卡改变")]
        StaffBankEdited =104,
        [Description("员工家庭成员改变")]
        StaffFamilyEdited =105,

        [Description("员工转正")]
        StaffToFullMemeber =201,
        [Description("员工离职")]
        StaffResign =202,
        [Description("员工调岗")]
        StaffShiftJob =203,
        [Description("员工调整考勤")]
        StaffUpdateAttHour =204,

        [Description("员工考勤异常提醒")]
        StaffAttAlert =301,
        [Description("员工证件照提醒")]
        StaffCertificateAlert = 302,
        [Description("员工转正提醒")]
        StaffToFullMemberAlert =303
    }
}
