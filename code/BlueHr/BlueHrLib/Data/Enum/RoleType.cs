using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Enum
{
    public enum RoleType
    {
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Admin = 100,

        /// <summary>
        /// 普通用户
        /// </summary>
        [Description("普通用户")]
        User = 200
    }
}
