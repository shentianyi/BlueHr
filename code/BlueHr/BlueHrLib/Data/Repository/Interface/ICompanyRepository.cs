using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface ICompanyRepository
    {
        IQueryable<Company> Search(CompanySearchModel searchModel);

        bool Create(Company company);

        Company FindById(int id);

        /// <summary>
        /// 通过公司名查找
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Company FindByName(string name);

        bool Update(Company company);

        bool DeleteById(int id);

        string FindByIds(List<string> ids);
      

    }
}
