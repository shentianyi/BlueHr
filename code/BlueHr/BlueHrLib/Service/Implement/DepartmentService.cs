using System;
using System.Linq;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;

namespace BlueHrLib.Service.Implement
{
    public class DepartmentService : ServiceBase, IDepartmentService
    {
        private IDepartmentRepository depRep;

        public DepartmentService(string dbString) : base(dbString)
        {
            depRep = new DepartmentRepository(this.Context);
        }
        public bool Create(Department department)
        {
            return depRep.Create(department);
        }

        public bool DeleteById(int id)
        {
            return depRep.DeleteById(id);
        }

        public IQueryable<Department> FindByCompanyId(int? id)
        {
            return depRep.FindByCompanyId(id);
        }

        public Department FindById(int id)
        {
            return depRep.FindById(id);
        }

        public Department FindByIdWithCompanyId(int? companyId, string departmentName)
        {
            return depRep.FindByIdWithCompanyId(companyId, departmentName);
        }

        public IQueryable<Department> Search(DepartmentSearchModel searchModel)
        {
            return depRep.Search(searchModel);
        }

        public bool Update(Department department)
        {
            return depRep.Update(department);
        }
    }
}
