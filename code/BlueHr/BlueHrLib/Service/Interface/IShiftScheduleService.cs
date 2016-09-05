using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.MQTask;

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
    }
}
