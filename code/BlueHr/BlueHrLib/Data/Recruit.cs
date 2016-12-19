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
        private ICompanyRepository companyRep;
        private IDepartmentRepository departmentRep;
        public string companyName
        {
            get
            {
                return companyRep.FindById(Convert.ToInt32(this.companyId)).name;
            }
        }
        public string departmentName
        {
            get
            {
                return departmentRep.FindById(Convert.ToInt32(this.departmentId)).name;
            }
        }

    }
}
