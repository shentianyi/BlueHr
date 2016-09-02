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
        /// 将班次结束点日期等于date日期，结束点时间小于等于date时间的班次考勤进行计算
        /// </summary>
        /// <param name="dateTime">计算的时间</param>
        /// <param name="shiftCodes">班次代码</param>
        /// <param name="searchModel">需要计算的员工的查询条件, **NOT IMPLEMENT**</param>
        void CalculateAttendRecord(DateTime dateTime, List<string> shiftCodes = null, StaffSearchModel searchModel=null);

       
    }
}
