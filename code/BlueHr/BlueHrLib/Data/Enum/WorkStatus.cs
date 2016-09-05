using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Enum
{
    public enum WorkStatus
    {
        /// <summary>
        /// 在职
        /// </summary>

        [Description("在职")]
        OnWork = 100,

        /// <summary>
        /// 离职
        /// </summary>

        [Description("离职")]
        OffWork = 200
    }
}
