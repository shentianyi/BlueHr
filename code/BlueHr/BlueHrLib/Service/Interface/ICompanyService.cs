using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
    public  interface ICompanyService
    {
        IQueryable<Company> Search(CompanySearchModel searchModel);

        bool Create(Company company);

        /// <summary>
        /// 通过Id查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        List<Company> GetAllTableName();
    }
}
