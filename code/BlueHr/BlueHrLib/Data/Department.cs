using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class Department
    {
        public string fullName
        {
            get
            {
                if (this.ParentDepartment != null)
                {

                    return string.Format("{0}-{1}", this.ParentDepartment.name, this.name);
                }
                return this.name;
            }
        }

        public string parentNames
        {
            get
            {
                if (this.ParentDepartment != null)
                {
                    if (this.ParentDepartment.ParentDepartment == null)
                    {
                        return this.ParentDepartment.name;
                    }
                    else
                    {
                        return string.Format("{0}-{1}", this.ParentDepartment.name, this.ParentDepartment.ParentDepartment.name);
                    }
                }
                return null;
            }
        }
    }
}
