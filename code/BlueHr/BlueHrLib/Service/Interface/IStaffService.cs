using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Service.Interface
{
    public interface IStaffService
    {
        /// <summary>
        /// 根据身份证号查询员工
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns></returns>
        Staff FindByStaffId(string id);

        /// <summary>
        /// 通过身份证号验证员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckStaffById(string id);

        /// <summary>
        /// 验证员工并且更新员工信息
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        bool CheckStaffAndUpdateInfo(StaffIdCard card);

        /// <summary>
        /// 创建员工信息，并设立是否系统验证
        /// </summary>
        /// <param name="card"></param>
        /// <param name="isIdChecked"></param>
        /// <returns></returns>
        bool CreateInfoAndSetCheck(StaffIdCard card,bool isIdChecked = true);
    }
}
