using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Service.Interface
{
    public interface IStaffTypeService
    {
        IQueryable<StaffType> Search(StaffTypeSearchModel searchModel);

        /// <summary>
        /// 得到一个公司下的所有部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IQueryable<StaffType> FindAll();
        /// <summary>
        /// 根据部门ID获取部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StaffType FindById(int id);
        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="staffType"></param>
        /// <returns></returns>
        bool Create(StaffType staffType);
        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="staffType"></param>
        /// <returns></returns>
        bool Update(StaffType staffType);
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteById(int id);
    }
}
