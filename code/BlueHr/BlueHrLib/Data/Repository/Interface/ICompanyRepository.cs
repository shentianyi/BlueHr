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

        bool Update(Company company);

        bool DeleteById(int id);
    }
}
