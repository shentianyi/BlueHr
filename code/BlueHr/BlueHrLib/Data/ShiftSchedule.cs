using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
   public partial class ShiftSchedule
    {
        public string scheduleAtStr {
            get { return this.scheduleAt.ToString("yyyy-MM-dd"); }
        }
    }
}
