using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class RewardsAndPenaltyRepository : RepositoryBase<RewardsAndPenalty>, IRewardsAndPenaltyRepository
    {
        private BlueHrDataContext context;

        public RewardsAndPenaltyRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public bool Create(RewardsAndPenalty certf)
        {
            try
            {
                this.context.GetTable<RewardsAndPenalty>().InsertOnSubmit(certf);
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
            RewardsAndPenalty cp = this.context.GetTable<RewardsAndPenalty>().FirstOrDefault(c => c.id.Equals(id));

            if (cp != null)
            {
                this.context.GetTable<RewardsAndPenalty>().DeleteOnSubmit(cp);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public RewardsAndPenalty FindById(int id)
        {
            RewardsAndPenalty cp = this.context.GetTable<RewardsAndPenalty>().FirstOrDefault(c => c.id.Equals(id));
            return cp;
        }

        public IQueryable<RewardsAndPenalty> Search(RewardsAndPenaltySearchModel searchModel)
        {
            IQueryable<RewardsAndPenalty> certf = this.context.RewardsAndPenalty;
            if (!string.IsNullOrEmpty(searchModel.staffNr))
            {
                certf = certf.Where(c => c.staffNr.Contains(searchModel.staffNr.Trim()));
            }
            if (searchModel.type!=null)
            {
                certf = certf.Where(c => c.type == searchModel.type);
            }
            return certf;
        }

        public bool Update(RewardsAndPenalty certf)
        {
            RewardsAndPenalty cp = this.context.GetTable<RewardsAndPenalty>().FirstOrDefault(c => c.id.Equals(certf.id));

            if (cp != null)
            {
                cp.staffNr = certf.staffNr==null?cp.staffNr:certf.staffNr;
                cp.type = certf.type == null ? cp.type : certf.type;
                cp.project = certf.project == null ? cp.project : certf.project;
                cp.description = certf.description == null ? cp.description : certf.description;
                cp.createdAt = certf.createdAt == null ? cp.createdAt : certf.createdAt;
                cp.createdUserId = certf.createdUserId == null ? cp.createdUserId : certf.createdUserId;
                cp.approvalUserId = certf.approvalUserId == null ? cp.approvalUserId : certf.approvalUserId;
                cp.approvalStatus = certf.approvalStatus == null ? cp.approvalStatus : certf.approvalStatus;
                cp.approvalRemark = certf.approvalRemark == null ? cp.approvalRemark : certf.approvalRemark;
                cp.approvalAt = certf.approvalAt == null ? cp.approvalAt : certf.approvalAt; ;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<RewardsAndPenalty> GetAll()
        {
            return this.context.GetTable<RewardsAndPenalty>().ToList();
        }

        public List<RewardsAndPenalty> GetAllTableName()
        {
            try
            {
                return (this.context.GetTable<RewardsAndPenalty>()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RewardsAndPenalty> FindBystaffNr(string staffNr)
        {
            List<RewardsAndPenalty> rp = this.context.GetTable<RewardsAndPenalty>().Where(r => r.staffNr == staffNr).ToList();
            return rp;
        }
    }
}