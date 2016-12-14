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
                title.IsRevoked = false;
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
            var jobcDep = this.context.GetTable<JobCertificate>().Where(p => p.jobTitleId.Equals(id)).ToList();
            if (jobcDep != null)
            {
                this.context.GetTable<JobCertificate>().DeleteAllOnSubmit(jobcDep);
                this.context.SubmitChanges();
            }


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

        public bool Update(JobTitle title, string jobCertTypeIds)
        {
            //先删除再添加
            var jobcDep = this.context.GetTable<JobCertificate>().Where(p => p.jobTitleId.Equals(title.id)).ToList();
            if (jobcDep != null)
            {
                this.context.GetTable<JobCertificate>().DeleteAllOnSubmit(jobcDep);
                this.context.SubmitChanges();
            }


            var dep = this.context.GetTable<JobTitle>().FirstOrDefault(c => c.id.Equals(title.id));
            if (dep != null)
            {
                dep.name = title.name;
                dep.remark = title.remark;
                if (!string.IsNullOrEmpty(jobCertTypeIds))
                {
                    jobCertTypeIds.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(p =>
                    {
                        dep.JobCertificate.Add(new JobCertificate()
                        {
                            certificateTypeId = int.Parse(p),
                            jobTitleId = dep.id
                        });
                    });
                }
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

        public JobTitle FindByName(string name)
        {
            //获取查询到的第一个Name的职位
            return this.context.GetTable<JobTitle>().FirstOrDefault(c => c.name.Equals(name.Trim()));
        }

        public List<JobTitle> GetAllTableName()
        {
            try
            {
                return (this.context.GetTable<JobTitle>()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
