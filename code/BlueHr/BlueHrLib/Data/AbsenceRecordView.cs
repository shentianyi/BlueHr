using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
   public partial class AbsenceRecordView
    {
        public string absenceDateStr { get { return this.absenceDate.Value.ToString("yyyy-MM-dd"); } }


        public string startEndHourStr {
            get { return string.Format("{0}~{1}", this.startHour.Value.ToString("hh\\:mm"), this.endHour.Value.ToString("hh\\:mm")); }
        } 
    }
}
