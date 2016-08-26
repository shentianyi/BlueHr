using BlueHrLib.Data;
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
    }
}
