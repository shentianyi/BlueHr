using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Enum
{
    public enum CronJobType
    {
        /// <summary>
        /// 转正提醒
        /// </summary>
        ToFullWarn=100,

        /// <summary>
        /// 考勤计算
        /// </summary>
        CalAtt=200
    }
}
