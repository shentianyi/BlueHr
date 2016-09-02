using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Model.Search
{
  public  class AttendanceRecordCalSearchModel:AttendanceRecordDetailSearchModel
    {

        /// <summary>
        /// 是否异常
        /// </summary>
        public bool? IsException { get; set; }

        /// <summary>
        /// 异常是否处理
        /// </summary>
        public bool? IsExceptionHandled { get; set; }

        /// <summary>
        /// 出勤开始日期
        /// </summary>
        public DateTime? AttendanceDateFrom { get; set; }

        /// <summary>
        /// 出勤日期
        /// </summary>
        public DateTime? AttendanceDateEnd { get; set; }
    }
}
