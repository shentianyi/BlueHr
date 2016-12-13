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
    public class ShiftJobRecordService : ServiceBase, IShiftJobRecordService
    {
        private IShiftJobRecordRepository rep;

        public ShiftJobRecordService(string dbString) : base(dbString)
        {
            rep = new ShiftJobRecordRepository(this.Context);
        }

        public IQueryable<ShiftJobRecord> Search(ShiftJobRecordSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(ShiftJobRecord model)
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

        public ShiftJobRecord FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(ShiftJobRecord model)
        {
            return rep.Update(model);
        }

        public ShiftJobRecordInfoModel GetShiftJobRecordInfo(ShiftJobRecordSearchModel searchModel)
        {
            ShiftJobRecordInfoModel info = new ShiftJobRecordInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IShiftJobRecordRepository rep = new ShiftJobRecordRepository(dc);
            IQueryable<ShiftJobRecord> results = rep.Search(searchModel);

            info.shiftJobRecordCount = dc.Context.GetTable<ShiftJobRecord>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }

        public IQueryable<ShiftJobRecord> AdvancedSearch(string v1, string v2, string v3)
        {
            return rep.AdvancedSearch(v1, v2, v3);
        }
    }
}