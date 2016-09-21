using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class ExtraWorkRecordView
    {
        public string otTimeStr { get { return this.otTime.Value.ToString("yyyy-MM-dd"); } }


        public string startEndHourStr
        {
            get { return string.Format("{0}~{1}", this.startHour.Value.ToString("HH:mm"), this.endHour.Value.ToString("HH:mm")); }
        }
    }
}
