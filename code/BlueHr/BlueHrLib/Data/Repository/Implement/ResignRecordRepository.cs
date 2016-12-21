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
            catch (Exception e)
            {
                Console.Write(e);
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
            IQueryable<ResignRecord> resignRecord = this.context.ResignRecord;
            if (!string.IsNullOrEmpty(searchModel.staffNr))
            {
                resignRecord = resignRecord.Where(c => c.staffNr.Contains(searchModel.staffNr.Trim()));
            }

            return resignRecord;
        }

        public bool Update(ResignRecord rsr)
        {
            ResignRecord cp = this.context.GetTable<ResignRecord>().FirstOrDefault(c => c.id.Equals(rsr.id));

            if (cp != null)
            {
                cp.remark = rsr.remark == null ? cp.remark : rsr.remark;
                cp.resignChecker = rsr.resignChecker == null ? cp.resignChecker : rsr.resignChecker;
                //cp.resignTypeId = rsr.resignTypeId;
                cp.resignEffectiveAt = rsr.resignEffectiveAt == null ? cp.resignEffectiveAt : rsr.resignEffectiveAt;
                cp.resignReason = rsr.resignReason == null ? cp.resignReason : rsr.resignReason;
                cp.resignCheckUserId = rsr.resignCheckUserId == null ? cp.resignCheckUserId : rsr.resignCheckUserId;
                cp.resignChecker = rsr.resignChecker == null ? cp.resignChecker : rsr.resignChecker;
                cp.approvalAt = rsr.approvalAt == null ? cp.approvalAt : rsr.approvalAt;
                cp.approvalStatus = rsr.approvalStatus == null ? cp.approvalStatus : rsr.approvalStatus;
                cp.approvalRemark = rsr.approvalRemark == null ? cp.approvalRemark : rsr.approvalRemark;
               

                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ResignRecord> FindByResignType(int id)
        {
            return this.context.GetTable<ResignRecord>().Where(p => p.resignTypeId.Equals(id)).ToList();
        }

        public ResignRecord FindByNr(string staffNr)
        {
            IQueryable<ResignRecord> resignRecord = this.context.ResignRecord;
            return resignRecord.Where(c => c.staffNr == staffNr).FirstOrDefault();
        }
    }
}