using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data;
using BlueHrLib.Service.Interface;

namespace BlueHrLib.Service.Implement
{
    public class ShiftSheduleService : ServiceBase, IShiftScheduleService
    {
        public ShiftSheduleService(string dbString) : base(dbString) { }

        /// <summary>
        /// 根据时间获取排班
        /// 排版在datetime日期开始的时间，并且结束时间小于datetime
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="shiftCodes">班次代码列表</param>
        /// <returns></returns>
        public List<ShiftScheduleView> GetDetailViewByDateTime(DateTime datetime, List<string> shiftCodes = null)
        {
            DataContext dc = new DataContext(this.DbString);

            var q =
                 dc.Context.GetTable<ShiftScheduleView>()
                 .Where(s => s.fullStartAt.Value.Date.Equals(datetime.Date)
                 && s.fullEndAt.Value < datetime);

            if (shiftCodes != null )
            {
                if (shiftCodes.Count == 1)
                {
                    q = q.Where(s => s.code.Equals(shiftCodes.First()));
                }
                else
                {
                    q = q.Where(s => shiftCodes.Contains(s.code));
                }
            }
            return q.ToList();
        }
    }
}
