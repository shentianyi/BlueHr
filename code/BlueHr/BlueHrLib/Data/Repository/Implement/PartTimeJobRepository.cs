using ALinq.Dynamic;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
   
    public class PartTimeJobRepository : RepositoryBase<PartTimeJob>, IPartTimeJobRepository
    {
        private BlueHrDataContext context;

        public PartTimeJobRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public IQueryable<PartTimeJob> AdvancedSearch(string v1, string v2, string v3)
        {
            string strWhere = string.Empty;

            try
            {
                strWhere = SearchConditionsHelper.GetStrWhere("PartTimeJob", v1, v2, v3);
                var q = this.context.CreateQuery<PartTimeJob>(strWhere);
                return q;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Create(PartTimeJob rsr)
        {
            try
            {
                this.context.GetTable<PartTimeJob>().InsertOnSubmit(rsr);
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
            PartTimeJob cp = this.context.GetTable<PartTimeJob>().FirstOrDefault(c => c.id.Equals(id));

            if (cp != null)
            {
                this.context.GetTable<PartTimeJob>().DeleteOnSubmit(cp);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public PartTimeJob FindById(int id)
        {
            PartTimeJob cp = this.context.GetTable<PartTimeJob>().FirstOrDefault(c => c.id.Equals(id));
            return cp;
        }

        public IQueryable<PartTimeJob> Search(PartTimeJobSearchModel searchModel)
        {
            IQueryable<PartTimeJob> PartTimeJobs = this.context.PartTimeJob;
            if (!string.IsNullOrEmpty(searchModel.staffNr))
            {
                PartTimeJobs = PartTimeJobs.Where(c => c.staffNr.Contains(searchModel.staffNr.Trim()));
            }
            return PartTimeJobs;
        }

        public bool Update(PartTimeJob lr)
        {
            PartTimeJob updatedlr = this.context.GetTable<PartTimeJob>().FirstOrDefault(c => c.id.Equals(lr.id));

            if (updatedlr != null)
            {
                updatedlr.staffNr = lr.staffNr == null ? updatedlr.staffNr : lr.staffNr;
                updatedlr.companyId = lr.companyId == 0 ? updatedlr.companyId : lr.companyId;
                updatedlr.departmentId = lr.departmentId == 0 ? updatedlr.departmentId : lr.departmentId;
                updatedlr.jobTitleId = lr.jobTitleId == 0 ? updatedlr.jobTitleId : lr.jobTitleId;
                updatedlr.startTime = lr.startTime == null ? updatedlr.startTime : lr.startTime;
                updatedlr.endTime = lr.endTime == null ? updatedlr.endTime : lr.endTime;
                updatedlr.isDelete = lr.isDelete == false ? updatedlr.isDelete : lr.isDelete;

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