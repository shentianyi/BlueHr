using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{

    public partial class PartTimeJob
    {

        public string CompanyName
        {
            get
            {
                return this.Company.name;
            }
        }
        public string DepartmentName
        {
            get
            {
                return this.Department.name;
            }
        }
        
        public string JobTitleName
        {
            get
            {
                return this.JobTitle.name;
            }
        }
    }
}
