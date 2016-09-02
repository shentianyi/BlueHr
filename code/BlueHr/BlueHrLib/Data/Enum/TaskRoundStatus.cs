using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Enum
{
    public enum TaskRoundStatus
    {
        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting = 1000,

        /// <summary>
        /// 运行中
        /// </summary>
        [Description("运行中")]
        Running = 2000,

        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Cancel = 3000,

        /// <summary>
        /// 已结束
        /// </summary>
        [Description("已结束")]
        Finish = 4000
    }
}
