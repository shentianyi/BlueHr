using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Search
{
    public class ExtraWorkRecordSearchModel
    {
        public string staffNr { get; set; }

        public string extraWorkTypeId { get; set; }

        public DateTime? durStart { get; set; }

        public DateTime? durEnd { get; set; }

        public User lgUser { get; set; }
    }
}
