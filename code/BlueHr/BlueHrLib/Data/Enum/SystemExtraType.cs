using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Enum
{
    /// <summary>
    /// 系统预设 加班类型, 这个类型中的前三个的值要与WorkAndRestType的值对应，分别为100,200,300
    /// </summary>
    public enum SystemExtraType
    {
        /// <summary>
        /// 延时加班
        /// </summary>
        [Description("延时加班")]
        WorkExtra = 100,

        /// <summary>
        /// 双休加班
        /// </summary>
        [Description("双休加班")]
        WeekendExtra = 200,


        /// <summary>
        /// 节假日加班
        /// </summary>
        [Description("节假日加班")]
        HolidayExtra = 300



        ///// <summary>
        ///// 其它加班
        ///// </summary>
        //[Description("其它加班")]
        //OtherExtra = 400
    }
}
