using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;

namespace BlueHrLib.Service.Interface
{
    /// <summary>
    /// 部门管理
    /// </summary>
    public interface IDepartmentService
    {
        Department FindById(int id);
        bool Create(Department department);
        bool Update(Department department);
        bool DeleteById(int id);
    }
}
