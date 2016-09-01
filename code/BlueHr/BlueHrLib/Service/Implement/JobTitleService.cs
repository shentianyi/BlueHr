using System.Linq;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;

namespace BlueHrLib.Service.Implement
{
    public class JobTitleService : ServiceBase, IJobTitleService
    {
        private IJobTitleRepository jobTitleRep;

        public JobTitleService(string dbString) : base(dbString)
        {
            jobTitleRep = new JobTitleRepository(this.Context);
        }

        public IQueryable<JobTitle> Search(JobTitleSearchModel searchModel)
        {
            return jobTitleRep.Search(searchModel);
        }

        public bool Create(JobTitle department)
        {
            return jobTitleRep.Create(department);
        }

        public bool DeleteById(int id)
        {
            return jobTitleRep.DeleteById(id);
        }

        public JobTitle FindById(int id)
        {
            return jobTitleRep.FindById(id);
        }

        public bool Update(JobTitle department)
        {
            return jobTitleRep.Update(department);
        }
    }
}
