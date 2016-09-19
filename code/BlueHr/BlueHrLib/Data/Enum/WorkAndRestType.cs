using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Enum
{
    /// <summary>
    /// 作息日期类型
    /// </summary>
    public enum WorkAndRestType
    {
        /// <summary>
        /// 工作日
        /// </summary>
        [Description("工作日")]
        WorkDay = 100,

        /// <summary>
        /// 双休日
        /// </summary>
        [Description("双休日")]
        Weekend = 200,
        

        /// <summary>
        /// 节假日
        /// </summary>
        [Description("节假日")]
        Holiday = 300


    }
}
