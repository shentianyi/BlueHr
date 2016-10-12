using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Model.Excel;
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
        /// 是否可以离职
        /// </summary>
        public bool canResign {
            get {
                return (this.workStatus == (int)WorkStatus.OnWork);
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
                foreach (var c in this.Certificates)
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

        public string companyName
        {
            get { return this.Company == null ? null : this.Company.name; }
        }
        public string departmentName
        {
            get { return this.Department == null ? null : this.Department.name; }
        }

        public string parentNames
        {
            get { return this.Department == null ? null : this.Department.parentNames; }
        }

        public string jobTitleName
        {
            get { return this.JobTitle == null ?   null : this.JobTitle.name; }
        }


        public bool IsMinusExtraWorkHour
        {
            get
            {
                if((this.departmentName =="成型课") || ( this.jobTitleName=="司机" && this.departmentName== "行政课")){
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static Dictionary<string, string> ValueName = new Dictionary<string, string>() {
             { "address","通信地址" },
             { "phone","联系电话" }
        };
        /// <summary>
        /// 操作者Id
        /// </summary>
        public int OperatorId { get; set; }
        /// -----------------------------------------记录改变者
        /// <summary>
        /// _Was 记录的是旧的值
        /// </summary>
        public string address_Was { get; set; }
        partial void OnaddressChanging(string value)
        { 
            this.address_Was = this.address;
        }
        public string phone_Was { get; set; }
        partial void OnphoneChanging(string value)
        {
            this.phone_Was = this.phone;
        }

       
    }
}
