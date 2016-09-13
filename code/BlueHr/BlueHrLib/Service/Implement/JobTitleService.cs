using System.Linq;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;
using System.Collections.Generic;
using System;

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

        public bool Update(JobTitle department, string jobCertTypeIds)
        {
            return jobTitleRep.Update(department, jobCertTypeIds);
        }

        public JobTitleInfoModel GetJobTitleInfo(JobTitleSearchModel searchModel)
        {
            JobTitleInfoModel info = new JobTitleInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IJobTitleRepository jobTitleRep = new JobTitleRepository(dc);
            IQueryable<JobTitle> jobTitles = jobTitleRep.Search(searchModel);

            info.jobTitleCount = dc.Context.GetTable<JobTitle>().Where(c => c.id.Equals(jobTitles.Count() > 0 ? jobTitles.First().id : -1)).Count();

            return info;
        }

        public List<JobTitle> GetAll()
        {
            return jobTitleRep.GetAll();
        }

        public JobTitle FindByName(string name)
        {
            return jobTitleRep.FindByName(name);
        }
    }
}
