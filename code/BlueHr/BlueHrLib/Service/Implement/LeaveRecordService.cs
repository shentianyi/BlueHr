using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Implement
{
    public class LeaveRecordService : ServiceBase, ILeaveRecordService
    {
        private ILeaveRecordRepository rep;

        public LeaveRecordService(string dbString) : base(dbString)
        {
            rep = new LeaveRecordRepository(this.Context);
        }

        public IQueryable<LeaveRecord> Search(LeaveRecordSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(LeaveRecord model)
        {
            bool isSucceed = false;
            try
            {
                isSucceed = rep.Create(model);
            }
            catch (Exception)
            {
            }

            return isSucceed;
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public LeaveRecord FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(LeaveRecord model)
        {
            return rep.Update(model);
        }

        public LeaveRecordInfoModel GetLeaveRecordInfo(LeaveRecordSearchModel searchModel)
        {
            LeaveRecordInfoModel info = new LeaveRecordInfoModel();
            DataContext dc = new DataContext(this.DbString);
            ILeaveRecordRepository rep = new LeaveRecordRepository(dc);
            IQueryable<LeaveRecord> results = rep.Search(searchModel);

            info.levaeRecordCount = dc.Context.GetTable<LeaveRecord>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }

        public IQueryable<LeaveRecord> AdvancedSearch(string v1, string v2, string v3)
        {
            return rep.AdvancedSearch(v1, v2, v3);
        }
    }
}