using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class AttendanceRecordCalExceptionView
    {
        public string attendanceDateStr
        {
         get { return attendanceDate.ToString("yyyy-MM-dd"); }
        }
    }
}
