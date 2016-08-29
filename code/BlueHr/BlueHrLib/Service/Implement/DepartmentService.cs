using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;

namespace BlueHrLib.Service.Implement
{
    public class DepartmentService : ServiceBase, IDepartmentService
    {
        IDepartmentRepository depRep;

        public DepartmentService(string dbString) : base(dbString)
        {
            depRep = new DepartmentRepository(this.Context);
        }
        public bool Create(Department department)
        {
            var dc = new DataContext(this.DbString);

            return depRep.Create(department);
        }

        public bool DeleteById(int id)
        {
            return depRep.DeleteById(id);
        }

        public Department FindById(int id)
        {
            return depRep.FindById(id);
        }

        public bool Update(Department department)
        {
            return depRep.Update(department);
        }
    }
}
