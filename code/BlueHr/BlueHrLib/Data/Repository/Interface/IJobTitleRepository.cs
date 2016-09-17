using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IJobTitleRepository
    {
        IQueryable<JobTitle> Search(JobTitleSearchModel searchModel);

        bool Create(JobTitle jobTitle);

        JobTitle FindById(int id);

        JobTitle FindByName(string name);

        bool Update(JobTitle jobTitle, string jobCertTypeIds);

        bool DeleteById(int id);

        //或取所有JOBTITLE
        List<JobTitle> GetAll();
    }
}
