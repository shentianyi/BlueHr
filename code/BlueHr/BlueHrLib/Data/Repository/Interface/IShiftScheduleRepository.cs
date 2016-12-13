using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface IShiftScheduleRepository
    {
        IQueryable<ShiftSchedule> Search(ShiftScheduleSearchModel searchModel);

        bool Create(ShiftSchedule parModel);

        ShiftSchedule FindById(int id);

        bool Update(ShiftSchedule parModel);

        bool DeleteById(int id);

        //根据班次或取排班信息
        ShiftSchedule FindShiftScheduleByShiftId(int id);
        bool EasyCreate(ShiftSchedule title, DateTime startTime, DateTime endTime);

        List<ShiftScheduleView> GetAllShiftSchedule();
        ShiftSchedule FindForEdit(string staffNr, DateTime scheduleAt, int shiftId);
    }
}
