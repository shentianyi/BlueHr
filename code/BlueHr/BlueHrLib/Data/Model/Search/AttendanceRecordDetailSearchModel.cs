using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Model.Search
{
  public  class AttendanceRecordDetailSearchModel
    {
        /// <summary>
        /// 员工号，模糊
        /// </summary>
        public string StaffNr { get; set; }
        /// <summary>
        /// 员工号，精确
        /// </summary>
        public string StaffNrAct { get; set; }

        /// <summary>
        /// 起始打卡时间
        /// </summary>
        public DateTime? RecordAtFrom { get; set; }

        /// <summary>
        /// 截止打卡时间
        /// </summary>
        public DateTime? RecordAtEnd { get; set; }
    }
}
