using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface IAttendanceRecordCalRepository
    {
        AttendanceRecordCal FindById(int id);

        bool Create(AttendanceRecordCal attendanceRecordCal);

        bool DeleteById(int id);
    }
}
