﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Enum
{
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
        HolidayExtra = 300,



        /// <summary>
        /// 其它加班
        /// </summary>
        [Description("其它加班")]
        OtherExtra = 400
    }
}
