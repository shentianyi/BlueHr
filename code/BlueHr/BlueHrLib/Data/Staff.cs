using BlueHrLib.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class Staff
    {
        /// <summary>
        /// 是否可以转正
        /// </summary>
        public bool CanTobeFullMember
        {
            get
            {
                return (this.workStatus==(int)WorkStatus.OnWork) && this.isOnTrial;
            }
        }
    }
}
