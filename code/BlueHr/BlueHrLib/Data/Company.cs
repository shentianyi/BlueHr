using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class Company
    {
        public int departmentCound
        {
            get
            {
                return this.Department.Count;
            }
        }
    }
}
