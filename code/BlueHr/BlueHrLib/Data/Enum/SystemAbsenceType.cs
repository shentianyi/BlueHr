using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Enum
{
    public enum SystemAbsenceType
    {
        /// <summary>
        /// 放班
        /// </summary>
        [Description("放班")]
        HolidayWork = 100,


        /// <summary>
        /// 事假
        /// </summary>
        [Description("事假")]
        HolidayAbsence = 200,

        /// <summary>
        /// 病假
        /// </summary>
        [Description("病假")]
        SickAbsence = 300,

        /// <summary>
        /// 产假
        /// </summary>
        [Description("产假")]
        MaternityAbsence = 400,

        /// <summary>
        /// 丧假
        /// </summary>
        [Description("丧假")]
        FuneralAbsence = 500,

        /// <summary>
        /// 轮休
        /// </summary>
        [Description("轮休")]
        TurnAbsence = 600,

        /// <summary>
        /// 公休
        /// </summary>
        [Description("公休")]
        PublicAbsence = 700,

        /// <summary>
        /// 年休
        /// </summary>
        [Description("年休")]
        YearAbsence = 800,

        /// <summary>
        /// 新进
        /// </summary>
        [Description("新进")]
        NewAbsence = 900,

        /// <summary>
        /// 旷工
        /// </summary>
        [Description("旷工")]
        WorkAbsence = 1000,

        /// <summary>
        /// 离职
        /// </summary>
        [Description("离职")]
        ResignAbsence = 1100
    }
}
