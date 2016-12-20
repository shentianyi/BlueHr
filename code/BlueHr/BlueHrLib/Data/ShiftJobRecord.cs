using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{

    public partial class ShiftJobRecord
    {

        public string beforeCompanyName
        {
            get
            {
                return this.Company.name;
            }
        }
        public string afterCompanyName
        {
            get
            {
                return this.Company1.name;
            }
        }

        public string beforeDepartmentName
        {
            get
            {
                return this.Department.name;
            }
        }
        public string afterDepartmentName
        {
            get
            {
                return this.Department1.name;
            }
        }
        public string userName
        {
            get
            {
                return this.User.name;

            }
        }

        public string approvalUserName
        {
            get
            {
                return this.User1.name;
            }
        }
    }
}
