using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Service.Interface
{
    public interface IAttendanceRecordCalService
    { 

        /// <summary>
        /// 搜索计算考勤信息视图, 包含员工的信息
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IQueryable<AttendanceRecordCalView> SearchCalView(AttendanceRecordCalSearchModel searchModel);
 
    }
}
