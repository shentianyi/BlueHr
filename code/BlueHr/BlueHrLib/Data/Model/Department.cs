using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Model
{
    /// <summary>
    /// 部门实体类
    /// </summary>
    public class Department
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 部门名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 部门所在公司
        /// </summary>
        public int CompanyId { get; set; }
    }
}
