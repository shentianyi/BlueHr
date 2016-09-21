using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
  public partial  class ExtraWorkRecord
    {
        public string startEndHourStr
        {
            get
            {
                return string.Format("{0}~{1}", this.startHour, this.endHour);
            }
        }
    }
}
