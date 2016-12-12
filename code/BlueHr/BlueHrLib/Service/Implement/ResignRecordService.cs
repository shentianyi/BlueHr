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
    public class ResignRecordService : ServiceBase, IResignRecordService
    {
        private IResignRecordRepository rep;

        public ResignRecordService(string dbString) : base(dbString)
        {
            rep = new ResignRecordRepository(this.Context);
        }

        public IQueryable<ResignRecord> Search(ResignRecordSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(ResignRecord model)
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

        public ResignRecord FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(ResignRecord model)
        {
            return rep.Update(model);
        }

        public ResignRecordInfoModel GetResignRecordInfo(ResignRecordSearchModel searchModel)
        {
            ResignRecordInfoModel info = new ResignRecordInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IResignRecordRepository rep = new ResignRecordRepository(dc);
            IQueryable<ResignRecord> results = rep.Search(searchModel);

            info.resignRecordCount = dc.Context.GetTable<ResignRecord>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }
 
        public List<ResignRecord> FindByResignType(int id)
        {
            return rep.FindByResignType(id);
        }

        public ResignRecord FindByNr(string staffNr)
        {
            return rep.FindByNr(staffNr);
        }
    }
}