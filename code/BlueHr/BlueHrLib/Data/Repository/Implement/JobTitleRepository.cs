using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Search;

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
    }
}
