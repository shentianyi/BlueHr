using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;
using BlueHrLib.Data.Repository.Interface;

namespace BlueHrLib.Data
{
    public partial class Recruit
    {
        public string companyName
        {
            get
            {
                return this.Company.name;
            }
        }
        public string departmentName
        {
            get
            {
                return this.Department.name;
            }
        }

    }
}
