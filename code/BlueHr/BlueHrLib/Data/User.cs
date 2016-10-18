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
            //get { return EnumHelper.GetDescription((RoleType)this.role.Value); }
            get;
            set;
        }

        public string isLockedStr
        {
            get { return this.isLocked.HasValue ? (this.isLocked.Value ? "是" : "否") : "是"; }
        }

        //权限公司
        public string AuthCompany { get; set; }

        //权限部门
        public string AuthDepartment { get; set; }
    }
}
