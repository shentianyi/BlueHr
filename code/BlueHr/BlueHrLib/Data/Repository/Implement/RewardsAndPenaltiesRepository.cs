using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class RewardsAndPenaltiesRepository : RepositoryBase<RewardsAndPenalties>, IRewardsAndPenaltiesRepository
    {
        private BlueHrDataContext context;

        public RewardsAndPenaltiesRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public bool Create(RewardsAndPenalties certf)
        {
            try
            {
                this.context.GetTable<RewardsAndPenalties>().InsertOnSubmit(certf);
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
            RewardsAndPenalties cp = this.context.GetTable<RewardsAndPenalties>().FirstOrDefault(c => c.id.Equals(id));

            if (cp != null)
            {
                this.context.GetTable<RewardsAndPenalties>().DeleteOnSubmit(cp);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public RewardsAndPenalties FindById(int id)
        {
            RewardsAndPenalties cp = this.context.GetTable<RewardsAndPenalties>().FirstOrDefault(c => c.id.Equals(id));
            return cp;
        }

        public IQueryable<RewardsAndPenalties> Search(RewardsAndPenaltiesSearchModel searchModel)
        {
            IQueryable<RewardsAndPenalties> certf = this.context.RewardsAndPenalties;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                certf = certf.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }
            return certf;
        }

        public bool Update(RewardsAndPenalties certf)
        {
            RewardsAndPenalties cp = this.context.GetTable<RewardsAndPenalties>().FirstOrDefault(c => c.id.Equals(certf.id));

            if (cp != null)
            {
                cp.nr = certf.nr;
                cp.name = certf.name;
                cp.sex = certf.sex;
                cp.departmentId = certf.departmentId;
                cp.type = certf.type;
                cp.project = certf.project;
                cp.description = certf.description;
                cp.datetime = certf.datetime;
                cp.approver = certf.approver;

                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<RewardsAndPenalties> GetAll()
        {
            return this.context.GetTable<RewardsAndPenalties>().ToList();
        }

        public List<RewardsAndPenalties> GetAllTableName()
        {
            try
            {
                return (this.context.GetTable<RewardsAndPenalties>()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}