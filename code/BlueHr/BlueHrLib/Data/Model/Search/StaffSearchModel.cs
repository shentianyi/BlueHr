﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Model.Search
{
   public class StaffSearchModel
    {
        public string Nr { get; set; }
        public string NrAct { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public int? Sex { get; set; }

        public int? JobTitleId { get; set; }
        //二级联动
        public int? companyId { get; set; }
        public int? departmentId { get; set; }

        public DateTime? CompanyEmployAtFrom { get; set; }
        public DateTime? CompanyEmployAtTo { get; set; }

        public bool? IsOnTrial { get; set; }

        public int? WorkStatus { get; set; }

        public List<string> StaffNrs { get; set; }
    }
}
