using System;
using System.Collections.Generic;
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
        Late = 100,

        /// <summary>
        /// 早退
        /// </summary>
        EarlyLeave = 200,

        /// <summary>
        /// 旷工
        /// </summary>
        Absence = 300,

        /// <summary>
        /// 班次旷工
        /// 某日安排了多个班次，只出席了某些班次
        /// </summary>
        ShiftAbsence=301,


        /// <summary>
        /// 未到应上班工时
        /// </summary>
        LessWork = 400,

        /// <summary>
        /// 考勤数据不整齐
        /// </summary>
        MessRecord = 500
    }
}
