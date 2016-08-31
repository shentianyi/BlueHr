using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;

namespace BlueHrLib.Service.Implement
{
    public class JobTitleService : ServiceBase, IJobTitleService
    {
        public JobTitleService(string dbString) : base(dbString) { }

        public IQueryable<JobTitle> Search(JobTitleSearchModel searchModel)
        {
            DataContext dc = new DataContext(this.DbString);
            IJobTitleRepository jobTitleRep = new JobTitleRepository(dc);
            return jobTitleRep.Search(searchModel);
        }
    }
}
