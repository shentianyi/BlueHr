using BlueHrLib.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data
{
    public partial class AttendanceRecordCal
    {
        private List<AttendanceExceptionType> _attendanceExceptions;

        public List<AttendanceExceptionType> attendanceExceptions
        {
            get { return this._attendanceExceptions; }
            set
            {
                this._attendanceExceptions = value;
                if(this._attendanceExceptions!=null && this._attendanceExceptions.Count > 0)
                {
                    this.isException = true;
                    this.isExceptionHandled = false;
                    this.exceptionCodes = string.Join(",", this._attendanceExceptions.ToArray());
                }
            }
        }
    }
}
