using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Search
{
    /// <summary>
    /// •	对缺勤数据进行搜索，条件：员工号（模糊，输入），缺勤类别（选择），缺勤时间（时间范围，选择）
    /// </summary>
    public class AbsenceRecrodSearchModel
    {
        public string staffNr { get; set; }

        public string absenceTypeId { get; set; }

        public DateTime? durStart { get; set; }

        public DateTime? durEnd { get; set; }
    }
}
