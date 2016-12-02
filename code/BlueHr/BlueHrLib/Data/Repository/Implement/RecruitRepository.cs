using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class RecruitRepository : RepositoryBase<Recruit>, IRecruitRepository
    {
        private BlueHrDataContext context;

        public RecruitRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public bool Create(Recruit certf)
        {
            try
            {
                this.context.GetTable<Recruit>().InsertOnSubmit(certf);
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
            Recruit cp = this.context.GetTable<Recruit>().FirstOrDefault(c => c.id.Equals(id));

            if (cp != null)
            {
                this.context.GetTable<Recruit>().DeleteOnSubmit(cp);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Recruit FindById(int id)
        {
            Recruit cp = this.context.GetTable<Recruit>().FirstOrDefault(c => c.id.Equals(id));
            return cp;
        }

        public IQueryable<Recruit> Search(RecruitSearchModel searchModel)
        {
            IQueryable<Recruit> certf = this.context.Recruit;
            if (!string.IsNullOrEmpty(searchModel.requirement))
            {
                certf = certf.Where(c => c.requirement.Contains(searchModel.requirement.Trim()));
            }
            if (searchModel.companyId.HasValue)
            {
                certf = certf.Where(c => c.companyId==searchModel.companyId);
            }
            if (searchModel.departmentId.HasValue)
            {
                certf = certf.Where(c => c.departmentId == searchModel.departmentId);
            }
            if (searchModel.amount.HasValue)
            {
                certf = certf.Where(c => c.amount == searchModel.amount);
            }
            return certf;
        }

        public bool Update(Recruit certf)
        {
            Recruit cp = this.context.GetTable<Recruit>().FirstOrDefault(c => c.id.Equals(certf.id));

            if (cp != null)
            {
                cp.requirement = certf.requirement;
                cp.companyId = certf.companyId;
                cp.departmentId = certf.departmentId;
                cp.amount = certf.amount;
                cp.createAt = certf.createAt;
                cp.requirementAt = certf.requirementAt;
                cp.requirementMan = certf.requirementMan;
                cp.status = certf.status;
                cp.auditRecord = certf.auditRecord;
                cp.auditView = certf.auditView;

                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Recruit> GetAll()
        {
            return this.context.GetTable<Recruit>().ToList();
        }

        
    }
}