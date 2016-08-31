using BlueHrLib.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data
{
    public partial class AttendanceRecordDetail
    {
        public bool isRepeatedData { get; set; }

        public DateTime recordAtDate
        {
            get { return this.recordAt.Date; }
        }

        public TimeSpan recordAtTime
        {
            get { return this.recordAt.TimeOfDay; }
        }
        public string recordAtDateStr
        {
            get { return this.recordAt.ToString("yyyy-MM-dd"); }
        }

        public string recordAtTimeStr
        {
            get { return this.recordAt.ToString("HH:mm"); }
        }

    }
}
