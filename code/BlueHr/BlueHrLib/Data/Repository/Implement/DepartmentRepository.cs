using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;

namespace BlueHrLib.Data.Repository.Implement
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        private BlueHrDataContext context;

        public DepartmentRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public bool Create(Department department)
        {
            try
            {
                this.context.GetTable<Department>().InsertOnSubmit(department);
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
            var dep = this.context.GetTable<Department>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<Department>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<Department> FindByCompanyId(int? id)
        {
            IQueryable<Department> departments = this.context.Department;
            if (id.HasValue)
            {
                departments = departments.Where(c => c.companyId.Equals(id));
            }
            return departments;
        }

        public Department FindById(int id)
        {
            return this.context.GetTable<Department>().FirstOrDefault(c => c.id.Equals(id));
        }

        public string FindByIds(List<string> ids)
        {
            List<Department> deps = this.context.GetTable<Department>().Where(p => ids.Contains(p.id.ToString())).ToList();
            string depsNames = "";
            deps.ForEach(p =>
            {
                depsNames += p.name + ",";
            });

            return depsNames;
        }

        public Department FindByIdWithCompanyId(int? companyId, string departmentName)
        {
            //如果没有companyID， 那么查找表中第一个匹配的职位名称 的 职位ID

            if (companyId.HasValue)
            {
                return this.context.GetTable<Department>().Where(m => m.companyId.Equals(companyId)).FirstOrDefault(c => c.name.Equals(departmentName.Trim()));
            }
            else
            {
                return this.context.GetTable<Department>().FirstOrDefault(c => c.name.Equals(departmentName.Trim()));
            }
        }

        public Department FindByIdWithCompanyIdAndParentId(int? companyId, string departmentName, int parentId)
        {
            return this.context.GetTable<Department>().Where(m => m.companyId.Equals(companyId) && m.parentId.Equals(parentId)).FirstOrDefault(c => c.name.Equals(departmentName.Trim()));
        }

        public IQueryable<Department> Search(DepartmentSearchModel searchModel)
        {
            IQueryable<Department> departments = this.context.Department;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                departments = departments.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }

            if (searchModel.CompanyId.HasValue)
            {
                departments = departments.Where(c => c.companyId.Equals(searchModel.CompanyId));
            }
            return departments;
        }

        public bool Update(Department department)
        {
            var dep = this.context.GetTable<Department>().FirstOrDefault(c => c.id.Equals(department.id));
            if (dep != null)
            {
                dep.name = department.name;
                dep.remark = department.remark;
                dep.parentId = department.parentId;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
