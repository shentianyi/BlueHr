using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Implement
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        private BlueHrDataContext context;

        public CompanyRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public bool Create(Company company)
        {
            this.context.GetTable<Company>().InsertOnSubmit(company);
            this.context.SubmitChanges();
            return true;
        }

        public IQueryable<Company> Search(CompanySearchModel searchModel)
        {
            IQueryable<Company> companies = this.context.Company;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                companies = companies.Where(c => c.name.Contains(searchModel.Name));
            }
            return companies;
        }
    }
}
