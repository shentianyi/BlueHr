using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Search
{
    public class WorkAndRestSearchModel
    {
        public DateTime? DateAtFrom { get; set; }
        public DateTime? DateAtTo { get; set; }
        public int? DateType { get; set; }
        public User loginUser { get; set; }

    }
}
