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
        /// <param name="date">计算的时间</param>
        /// <param name="shiftCodes">班次代码</param>
        /// <param name="searchModel">需要计算的员工的查询条件, **NOT IMPLEMENT**</param>
        void CalculateAttendRecord(DateTime date, List<string> shiftCodes = null, StaffSearchModel searchModel=null);

        /// <summary>
        /// 搜索详细考勤信息
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IQueryable<AttendanceRecordDetail> SearchDetail(AttendanceRecordDetailSearchModel searchModel);

        /// <summary>
        /// 搜索详细考勤信息视图, 包含员工的信息
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IQueryable<AttendanceRecordDetailView> SearchDetailView(AttendanceRecordDetailSearchModel searchModel);

        /// <summary>
        /// 批量创建详细考勤数据
        /// </summary>
        /// <param name="records"></param>
        void CreateDetails(List<AttendanceRecordDetail> records);

        /// <summary>
        /// 根据员工号和考勤时间查询详细记录
        /// </summary>
        /// <param name="nr"></param>
        /// <param name="recordAt"></param>
        /// <returns></returns>
        AttendanceRecordDetail FindDetailByStaffAndRecordAt(string nr, DateTime recordAt);
    }
}
