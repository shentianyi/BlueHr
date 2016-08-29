using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Department FindById(int id)
        {
            return this.context.GetTable<Department>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(Department department)
        {
            var dep = this.context.GetTable<Department>().FirstOrDefault(c => c.id.Equals(department.id));
            if (dep != null)
            {
                dep.name = department.name;
                dep.remark = department.remark;
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
