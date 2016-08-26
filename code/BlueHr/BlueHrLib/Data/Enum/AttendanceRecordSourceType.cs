using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Enum
{
    public enum AttendanceRecordSourceType
    {
        /// <summary>
        /// 数据库
        /// </summary>
        DataTrigger = 100,

        /// <summary>
        /// 文件导入
        /// </summary>
        FileImport200,

        /// <summary>
        /// 人工录入
        /// </summary>
        MuanualCreate = 300
    }
}
