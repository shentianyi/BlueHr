using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Search
{
    public class ShiftScheduleSearchModel
    {
        public string StaffNr { get; set; }

        public string StaffNrAct { get; set; }

        public DateTime? ScheduleAtFrom { get; set; }

        public DateTime? ScheduleAtEnd { get; set; }
    }
}
