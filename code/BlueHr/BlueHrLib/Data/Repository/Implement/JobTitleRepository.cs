using System;
using System.Linq;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System.Collections.Generic;

namespace BlueHrLib.Data.Repository.Implement
{
    public class JobTitleRepository : RepositoryBase<JobTitle>, IJobTitleRepository
    {
        private BlueHrDataContext context;

        public JobTitleRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<JobTitle> Search(JobTitleSearchModel searchModel)
        {
            IQueryable<JobTitle> jobtitles = this.context.JobTitle;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                jobtitles = jobtitles.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }
            return jobtitles;
        }

        public bool Create(JobTitle title)
        {
            try
            {
                this.context.GetTable<JobTitle>().InsertOnSubmit(title);
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
            var dep = this.context.GetTable<JobTitle>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<JobTitle>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public JobTitle FindById(int id)
        {
            return this.context.GetTable<JobTitle>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(JobTitle title)
        {
            var dep = this.context.GetTable<JobTitle>().FirstOrDefault(c => c.id.Equals(title.id));
            if (dep != null)
            {
                dep.name = title.name;
                dep.remark = title.remark;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<JobTitle> GetAll()
        {
            return this.context.GetTable<JobTitle>().ToList();
        }
    }
}
