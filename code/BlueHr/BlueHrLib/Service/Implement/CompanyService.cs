using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data;

namespace BlueHrLib.Service.Implement
{
    public class CompanyService : ServiceBase, ICompanyService
    {
        private ICompanyRepository companyRep;

        public CompanyService(string dbString): base(dbString)
        {
            companyRep = new CompanyRepository(this.Context);
        }

        public bool Create(Company company)
        {
            return companyRep.Create(company);
        }

        public bool DeleteById(int id)
        {
            return companyRep.DeleteById(id);
        }

        public Company FindById(int id)
        {
            return companyRep.FindById(id);
        }

        public Company FindByName(string name)
        {
            return companyRep.FindByName(name);
        }

        public IQueryable<Company> Search(CompanySearchModel searchModel)
        {
            return companyRep.Search(searchModel);
        }

        public bool Update(Company company)
        {
            return companyRep.Update(company);
        }

        public string FindByIds(List<string> ids)
        {
            return companyRep.FindByIds(ids);
        }

        public List<Company> GetAllTableName()
        {
            return companyRep.GetAllTableName();
        }

        public int countDepartment(int companyId)
        {
            return companyRep.countDepartment(companyId);
        }

        public List<Company> GetAll()
        {
            return companyRep.GetAll();
        }
    }
}
