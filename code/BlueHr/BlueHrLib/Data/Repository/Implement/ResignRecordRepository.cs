using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
   
    public class ResignRecordRepository : RepositoryBase<ResignRecord>, IResignRecordRepository
    {
        private BlueHrDataContext context;

        public ResignRecordRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public bool Create(ResignRecord rsr)
        {
            try
            {
                this.context.GetTable<ResignRecord>().InsertOnSubmit(rsr);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            ResignRecord cp = this.context.GetTable<ResignRecord>().FirstOrDefault(c => c.id.Equals(id));

            if (cp != null)
            {
                this.context.GetTable<ResignRecord>().DeleteOnSubmit(cp);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ResignRecord FindById(int id)
        {
            ResignRecord cp = this.context.GetTable<ResignRecord>().FirstOrDefault(c => c.id.Equals(id));
            return cp;
        }

        public IQueryable<ResignRecord> Search(ResignRecordSearchModel searchModel)
        {
            IQueryable<ResignRecord> certf = this.context.ResignRecord;
            if (!string.IsNullOrEmpty(searchModel.staffNr))
            {
                certf = certf.Where(c => c.staffNr.Contains(searchModel.staffNr.Trim()));
            }
            return certf;
        }

        public bool Update(ResignRecord rsr)
        {
            ResignRecord cp = this.context.GetTable<ResignRecord>().FirstOrDefault(c => c.id.Equals(rsr.id));

            if (cp != null)
            {
                cp.remark = rsr.remark;
                cp.resignAt = rsr.resignAt;
                cp.resignChecker = rsr.resignChecker;
                cp.resignTypeId = rsr.resignTypeId;
                cp.staffNr = rsr.staffNr;
               

                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}