using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class ExtraWorkRecord
    {
        public string otTimeStr { get { return otTime != null ? this.otTime.Value.ToString("yyyy-MM-dd") : ""; } }

        public string startEndHourStr
        {
            get
            {
                //Nullable object must have a value.
                return startHour != null && endHour != null ? string.Format("{0}~{1}", this.startHour.Value.ToString("hh\\:mm"), this.endHour.Value.ToString("hh\\:mm")) : "";
            }
        }
    }
}
