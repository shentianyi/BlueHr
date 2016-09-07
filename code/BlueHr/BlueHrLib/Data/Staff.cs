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

        /// <summary>
        /// 转正日期
        /// </summary>
        public string trialOverAtStr
        {
            get
            {
                return this.trialOverAt.HasValue ? this.trialOverAt.Value.ToString("yyyy-MM-dd") : string.Empty;
            }
        }

        public string sexDisplay
        {
            get
            {
                return this.sex.Equals("0") ? "男" : "女";
            }
        }
    }
}
