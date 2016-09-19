using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Service.Interface
{
    public interface IAttendanceRecordDetailService
    {
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

        /// <summary>
        /// 根据员工号和日期获取员工的详细打卡记录视图
        /// 重点是，根据日期来判断排班，然后通过排班来查找所有的记录
        /// 如果排班的结束时间的HH:mm小于日期的HH:mm，并且结束时间的天等于（或加1等于）日期的天
        /// schedule视图里的fullEnd时间已经是根据排班类型计算过的了
        /// </summary>
        /// <param name="nr"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        List<AttendanceRecordDetailView> GetDetailsViewByStaffAndDate(string nr, DateTime datetime);

        List<AttendanceRecordDetailView> GetDetailsViewByStaffAndDateWithExtrawork(string nr, DateTime datetime);

        List<AttendanceRecordDetail> GetByStaffAndTimespan(string staffNr, DateTime startTime, DateTime endTime);
    }
}
