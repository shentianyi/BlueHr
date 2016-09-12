using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Service.Interface
{
    public interface IJobTitleService
    {
        IQueryable<JobTitle> Search(JobTitleSearchModel searchModel);

        JobTitle FindById(int id);

        /// <summary>
        /// 通过Name查询职位
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        JobTitle FindByName(string name);
        bool Create(JobTitle title);

        bool Update(JobTitle title);

        bool DeleteById(int id);

        JobTitleInfoModel GetJobTitleInfo(JobTitleSearchModel searchModel);

        List<JobTitle> GetAll();
    }
}
