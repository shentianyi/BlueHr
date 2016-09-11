using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data;
using BlueHrLib.Service.Interface;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;

namespace BlueHrLib.Service.Implement
{
    public class ShiftSheduleService : ServiceBase, IShiftScheduleService
    {
        //public ShiftSheduleService(string dbString) : base(dbString) { }

        private IShiftScheduleRepository rep;

        public ShiftSheduleService(string dbString) : base(dbString)
        {
            rep = new ShiftScheduleRepository(this.Context);
        }

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
        public IQueryable<ShiftSchedule> Search(ShiftScheduleSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(ShiftSchedule model)
        {
            return rep.Create(model);
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public ShiftSchedule FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(ShiftSchedule model)
        {
            return rep.Update(model);
        }

        public ShiftScheduleInfoModel GetShiftScheduleInfo(ShiftScheduleSearchModel searchModel)
        {
            ShiftScheduleInfoModel info = new ShiftScheduleInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IShiftScheduleRepository rep = new ShiftScheduleRepository(dc);
            IQueryable<ShiftSchedule> results = rep.Search(searchModel);

            info.shiftScheduleCount = dc.Context.GetTable<ShiftSchedule>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }
    }
}
