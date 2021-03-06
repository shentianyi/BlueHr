﻿using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{

    public class ShiftScheduleRepository : RepositoryBase<ShiftSchedule>, IShiftScheduleRepository
    {
        private BlueHrDataContext context;

        public ShiftScheduleRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<ShiftSchedule> Search(ShiftScheduleSearchModel searchModel)
        {
            //TODO
            IQueryable<ShiftSchedule> q = this.context.ShiftSchedule;
            if (!string.IsNullOrEmpty(searchModel.StaffNr))
            {
                q = q.Where(c => c.staffNr.Contains(searchModel.StaffNr.Trim()));
            }
            if (!string.IsNullOrEmpty(searchModel.StaffNrAct))
            {
                q = q.Where(c => c.staffNr.Equals(searchModel.StaffNrAct));
            }
            if (searchModel.ScheduleAtFrom.HasValue)
            {
                q = q.Where(s => s.scheduleAt >= searchModel.ScheduleAtFrom.Value);
            }


            if (searchModel.ScheduleAtEnd.HasValue)
            {
                q = q.Where(s => s.scheduleAt <= searchModel.ScheduleAtEnd.Value);
            }
            return q.OrderByDescending(s=>s.scheduleAt);
        }

        public bool Create(ShiftSchedule parModel)
        {
            try
            {
                this.context.GetTable<ShiftSchedule>().InsertOnSubmit(parModel);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            var dep = this.context.GetTable<ShiftSchedule>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<ShiftSchedule>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ShiftSchedule FindById(int id)
        {
            return this.context.GetTable<ShiftSchedule>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(ShiftSchedule parModel)
        {
            var dep = this.context.GetTable<ShiftSchedule>().FirstOrDefault(c => c.id.Equals(parModel.id));
            if (dep != null)
            {
                dep.shiftId = parModel.shiftId;
                dep.staffNr = parModel.staffNr;
                dep.scheduleAt = parModel.scheduleAt;

                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //根据班次或取排班信息
        public ShiftSchedule FindShiftScheduleByShiftId(int id)
        {
            return this.context.GetTable<ShiftSchedule>().FirstOrDefault(c => c.shiftId.Equals(id));
        }
    }
}
