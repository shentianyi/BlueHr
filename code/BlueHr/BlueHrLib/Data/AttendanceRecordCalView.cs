using BlueHrLib.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data
{
    public partial class AttendanceRecordCalView
    {
      
        public string attendanceDateStr
        {
            get { return this.attendanceDate.ToString("yyyy-MM-dd"); }
        }

        public string isExceptionStr
        {
            get { return this.isException ? "是" : "否"; }
        }
        
        public string isExceptionHandledStr
        {
            get { return this.isExceptionHandled.HasValue ? (this.isExceptionHandled.Value ? "是" : "否") : string.Empty; }
        }

        public double oriWorkingHourRound
        {
            get { return Math.Round(this.oriWorkingHour, 2); }
        }

        public double actWorkingHourRound
        {
            get { return Math.Round(this.actWorkingHour, 2); }
        }

    }
}
