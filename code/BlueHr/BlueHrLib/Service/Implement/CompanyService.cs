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
    public class CompanyService : ServiceBase, ICompanyService
    {
        public CompanyService(string dbString) : base(dbString) { }

        public bool Create(Company company)
        {
            DataContext dc = new DataContext(this.DbString);
            ICompanyRepository companyRep = new CompanyRepository(dc);
            return companyRep.Create(company);
        }

        public IQueryable<Company> Search(CompanySearchModel searchModel)
        {
            DataContext dc = new DataContext(this.DbString);
            ICompanyRepository companyRep = new CompanyRepository(dc);
            return companyRep.Search(searchModel);
        }
    }
}
