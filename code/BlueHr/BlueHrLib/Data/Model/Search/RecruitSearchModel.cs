using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Search
{
    public class RecruitSearchModel
    {
        public string requirement { get; set; }
        public int? companyId { get; set; }
        public int? departmentId { get; set; }
        public int? amount { get; set; }
    }
}
