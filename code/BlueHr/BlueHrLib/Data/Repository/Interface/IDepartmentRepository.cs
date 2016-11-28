using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> Search(DepartmentSearchModel searchModel);
        IQueryable<Department> FindByCompanyId(int? id);

        bool Create(Department department);

        Department FindById(int id);

        Department FindByIdWithCompanyId(int? companyId, string departmentName);
        Department FindByIdWithCompanyIdAndParentId(int? companyId, string departmentName, int parentId);

        bool Update(Department department);

        bool DeleteById(int id);

        string FindByIds(List<string> ids);
        List<Department> GetAllTableName();
    }
}
