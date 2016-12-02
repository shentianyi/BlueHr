using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Search
{
    public class RewardsAndPenaltiesSearchModel
    {
        public string Nr { get; set; }
        public string Name { get; set; }
        public int? Sex { get; set; }
        public int? departmentId { get; set; }
        public int? type { get; set; }
    }
}
