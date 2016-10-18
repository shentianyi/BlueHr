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
            try
            {
                this.context.GetTable<Company>().InsertOnSubmit(company);
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
            Company cp = this.context.GetTable<Company>().FirstOrDefault(c => c.id.Equals(id));

            if (cp != null)
            {
                this.context.GetTable<Company>().DeleteOnSubmit(cp);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Company FindById(int id)
        {
            Company cp = this.context.GetTable<Company>().FirstOrDefault(c => c.id.Equals(id));
            return cp;
        }

        //根据公司ID列表获取所有公司名
        public string FindByIds(List<string> ids)
        {
            string cmpNames = "";

            List<Company> cp = this.context.GetTable<Company>().Where(p => ids.Contains(p.id.ToString())).ToList();
            cp.ForEach(p =>
            {
                cmpNames += p.name + ",";
            });

            return cmpNames;
        }

        public Company FindByName(string name)
        {
            Company cp = this.context.GetTable<Company>().FirstOrDefault(c => c.name.Equals(name.Trim()));
            return cp;
        }

        public IQueryable<Company> Search(CompanySearchModel searchModel)
        {
            IQueryable<Company> companies = this.context.Company;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                companies = companies.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }
            return companies;
        }

        public bool Update(Company company)
        {
            Company cp = this.context.GetTable<Company>().FirstOrDefault(c => c.id.Equals(company.id));

            if (cp != null)
            {
                cp.name = company.name;
                cp.address = company.address;
                cp.remark = company.remark;

                try
                {
                    this.context.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            else
            {
                return false;
            }


        }
    }
}
