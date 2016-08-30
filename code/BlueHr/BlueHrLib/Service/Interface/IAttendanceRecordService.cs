using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Service.Interface
{
    public interface IAttendanceRecordService
    {
        /// <summary>
        /// 将Detail的数据计算到Cal中
        /// </summary>
        /// <param name="date">计算的时间</param>
        void CalculateAttendRecord(DateTime date);

        /// <summary>
        /// 搜索详细考勤信息
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IQueryable<AttendanceRecordDetail> SearchDetail(AttendanceRecordDetailSearchModel searchModel);
    }
}
