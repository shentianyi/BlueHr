using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.MQTask;
using BlueHrLib.Data.Model.PageViewModel;

namespace BlueHrLib.Service.Interface
{
    public interface IShiftScheduleService
    {
        /// <summary>
        /// 根据时间获取排班
        /// 排版在datetime日期开始的时间，并且结束时间小于datetime
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="shiftCodes">班次代码列表</param>
        /// <returns></returns>
        List<ShiftScheduleView> GetDetailViewByDateTime(DateTime datetime,List<string> shiftCodes=null);

        IQueryable<ShiftSchedule> Search(ShiftScheduleSearchModel searchModel);

        ShiftSchedule FindById(int id);

        bool Create(ShiftSchedule title);

        bool Update(ShiftSchedule title);

        bool DeleteById(int id);

        ShiftScheduleInfoModel GetShiftScheduleInfo(ShiftScheduleSearchModel searchModel);

        //根据班次或取排班信息
        ShiftSchedule FindShiftScheduleByShiftId(int id);

        /// <summary>
        /// 是否是重复数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool IsDup(ShiftSchedule model);

        bool Creates(List<ShiftSchedule> records);

        /// <summary>
        /// 获得员工时间点之后的下一个排班
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        ShiftScheduleView GetNextShiftScheduleView(string staffNr, DateTime datetime);

        /// <summary>
        /// 获得员工某一时间的排班
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ShiftScheduleView GetFirstShiftByStaffAndDate(string staffNr, DateTime date);



        /// <summary>
        /// 获得员工某一段时间的所有排班
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        List<ShiftScheduleView> GetShiftsByStaffAndDateSpan(string staffNr, DateTime startDate,DateTime endDate);
        bool EasyCreate(ShiftSchedule title, DateTime startTime, DateTime endTime);
    }
}
