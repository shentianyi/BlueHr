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
        public bool canTobeFullMember
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
         
        /// <summary>
        /// 是否拥有需求的全部证照
        /// </summary>
        public bool IsHasAllCertificate
        {
            get
            {
                bool has = true;

                List<int> mustCers = new List<int>();
                if (this.JobTitle != null)
                {
                    foreach (var c in this.JobTitle.JobCertificate)
                    {
                        mustCers.Add(c.certificateTypeId);
                    }
                }
                List<int> hasCers = new List<int>();
                foreach (var c in this.Certificate)
                {
                    hasCers.Add(c.certificateTypeId);
                }

                foreach (var i in mustCers)
                {
                    if (!hasCers.Contains(i))
                    {
                        has = false;
                        break;
                    }
                }
                return has;
            }
        }

        public string sexDisplay
        {
            get
            {
                return string.IsNullOrEmpty(this.sex) ? "" : this.sex.Equals("0") ? "男" : "女"; 
            }
        }
    }
}
