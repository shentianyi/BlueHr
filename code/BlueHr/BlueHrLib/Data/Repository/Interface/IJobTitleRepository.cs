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

        bool Update(JobTitle jobTitle);

        bool DeleteById(int id);
    }
}
