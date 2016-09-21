using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class AbsenceRecrod
    {
        public string absenceDateStr { get { return this.absenceDate.Value.ToString("yyyy-MM-dd"); } }


        public string startEndHourStr
        {
            get { return string.Format("{0}~{1}", this.startHour.Value.ToString("HH:mm"), this.endHour.Value.ToString("HH:mm")); }
        }
    }
}
