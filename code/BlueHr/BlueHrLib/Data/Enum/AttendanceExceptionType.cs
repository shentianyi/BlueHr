using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Enum
{
    public enum AttendanceExceptionType
    {
        /// <summary>
        /// 迟到
        /// </summary>
        [Description("迟到")]
        Late = 100,

        /// <summary>
        /// 早退
        /// </summary>
        [Description("早退")]
        EarlyLeave = 200,

        /// <summary>
        /// 旷工
        /// </summary>
        [Description("旷工")]
        Absence = 300,

        /// <summary>
        /// 班次旷工
        /// 某日安排了多个班次，只出席了某些班次
        /// </summary>
        [Description("班次旷工")]
        ShiftAbsence =301,


        /// <summary>
        /// 未到应上班工时
        /// </summary>
        [Description("未到应上班工时")]
        LessWork = 400,

        /// <summary>
        /// 考勤数据不整齐
        /// </summary>
        [Description("考勤数据不整齐")]
        MessRecord = 500,


        /// <summary>
        /// 加班
        /// </summary>
        [Description("加班")]
        ExtraWork = 600,

        /// <summary>
        /// 与加班单时长不匹配
        /// </summary>
        [Description("与加班单时长不匹配")]
        ExtraWorkHourNotMatch = 700,

        /// <summary>
        /// 与加班单类型不匹配
        /// </summary>
        [Description("与加班单类型不匹配")]
        ExtraWorkTypeNotMatch = 800,

        /// <summary>
        /// 缺失加班单
        /// </summary>
        [Description("缺失加班单")]
        ExtraWorkNoRecord = 900
    }
}
