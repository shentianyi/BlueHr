using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Enum
{
    /// <summary>
    /// 班次类型
    /// </summary>
    public enum ShiftType
    {
        /// <summary>
        /// 今日
        /// </summary>
        [Description("今日")]
        Today = 100,

        /// <summary>
        /// 次日
        /// </summary>
        [Description("次日")]
        Tommorrow = 200
    }
}
