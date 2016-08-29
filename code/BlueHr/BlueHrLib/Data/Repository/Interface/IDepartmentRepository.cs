using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface IDepartmentRepository
    {

        bool Create(Department department);

        Department FindById(int id);

        bool Update(Department department);

        bool DeleteById(int id);
    }
}
