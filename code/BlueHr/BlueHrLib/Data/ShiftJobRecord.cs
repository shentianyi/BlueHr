using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{

    public partial class ShiftJobRecord
    {
        private ICompanyRepository companyRep;
        private IDepartmentRepository departmentRep;
        private IUserRepository userRep;

        public string beforeCompanyName
        {
            get { return companyRep.FindById(Convert.ToInt32(this.beforeCompanyId)).name; }
        }
        public string afterCompanyName
        {
            get { return companyRep.FindById(Convert.ToInt32(this.afterCompanyId)).name; }
        }

        public string beforeDepartmentName
        {
            get { return departmentRep.FindById(Convert.ToInt32(this.beforeDepartmentId)).name; }
        }
        public string afterDepartmentName
        {
            get { return departmentRep.FindById(Convert.ToInt32(this.afterDepartmentId)).name; }
        }
        public string userName
        {
            get
            {
                return userRep.FindById(Convert.ToInt32(this.userId)).name;
            }
        }

        public string approvalUserName
        {
            get
            {
                return userRep.FindById(Convert.ToInt32(this.approvalUserId)).name;
            }
        }
    }
}
