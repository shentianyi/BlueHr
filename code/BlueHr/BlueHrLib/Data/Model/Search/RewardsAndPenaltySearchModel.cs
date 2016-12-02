using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Search
{
    public class RewardsAndPenaltySearchModel
    {
        public string StaffNr{ get; set; }
        public string StaffName { get; set; }
        public int? StaffSex { get; set; }
        public int? departmentId { get; set; }
        public int? type { get; set; }
    }
}
