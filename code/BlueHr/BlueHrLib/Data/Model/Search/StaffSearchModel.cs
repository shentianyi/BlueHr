using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Model.Search
{
   public class StaffSearchModel
    {
        public string Nr { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public int? Sex { get; set; }

        public int? JobTitleId { get; set; }
        //二级联动
        public int? CompanyId { get; set; }
        public int? DepartmentId { get; set; }

        public DateTime? CompanyEmployAtFrom { get; set; }
        public DateTime? CompanyEmployAtTo { get; set; }

        public int? IsOnTrial { get; set; }
    }
}
