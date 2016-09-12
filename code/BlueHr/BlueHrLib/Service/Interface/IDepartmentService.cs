using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Service.Interface
{
    /// <summary>
    /// 部门管理
    /// </summary>
    public interface IDepartmentService
    {
        IQueryable<Department> Search(DepartmentSearchModel searchModel);
        /// <summary>
        /// 得到一个公司下的所有部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IQueryable<Department> FindByCompanyId(int? id);
        /// <summary>
        /// 根据部门ID获取部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Department FindById(int id);

        /// <summary>
        /// 通过 公司ID 和Departmentname 获取到唯一的公司ID
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="departmentName">部门Name</param>
        /// <returns></returns>
        Department FindByIdWithCompanyId(int? companyId, string departmentName);

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        bool Create(Department department);
        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        bool Update(Department department);
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteById(int id);
    }
}
