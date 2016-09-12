using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class User
    {
        public string roleStr
        {
            get { return EnumHelper.GetDescription((RoleType)this.role.Value); }
        }

        public string isLockedStr
        {
            get { return this.isLocked.HasValue ? (this.isLocked.Value ? "是" : "否") : "是"; }
        }
    }
}
