using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface IAttendanceRecordDetailRepository
    {
        IQueryable<AttendanceRecordDetail> Search(AttendanceRecordDetailSearchModel searchModel);

        bool Create(AttendanceRecordDetail attendanceRecordDetail);

        bool Update(AttendanceRecordDetail attendanceRecordDetail);

        AttendanceRecordDetail FindById(int id);

        bool DeleteById(int id);
    }
}
