using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Excel
{
    public class WorkSumExcelModel
    {
        public Staff Staff { get; set; }
        


        public List<WorkSumExcelItemModel> Items { get; set; }
    }

    public class WorkSumExcelItemModel
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 考勤统计记录
        /// </summary>
        public AttendanceRecordCal AttendanceRecordCal { get; set; }

        /// <summary>
        /// 缺勤记录
        /// </summary>
        public AbsenceRecrod AbsenceRecrod { get; set; }
    }
}
