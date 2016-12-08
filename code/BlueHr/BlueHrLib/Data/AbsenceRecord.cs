using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class AbsenceRecrod
    {
        public string absenceDateStr
        {
            get
            {
                //fix Nullable object must have a value.
                return absenceDate != null ? this.absenceDate.Value.ToString("yyyy-MM-dd") : "";

            }
        }


        public string startEndHourStr
        {
            get
            {

                //fix Nullable object must have a value.
                if (startHour != null && endHour != null)
                {
                    return string.Format("{0}~{1}", this.startHour.Value.ToString("hh\\:mm"), this.endHour.Value.ToString("hh\\:mm"));
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
