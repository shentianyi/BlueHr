using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Service.Implement
{
    public class AttendanceRecordService : ServiceBase, IAttendanceRecordService
    {
        public AttendanceRecordService(string dbString) : base(dbString) { }

        public void CalculateAttendRecord(DateTime date)
        {
            throw new NotImplementedException();
        }


    }
}
