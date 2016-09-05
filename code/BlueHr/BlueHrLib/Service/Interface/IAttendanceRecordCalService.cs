using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Message;

namespace BlueHrLib.Service.Interface
{
    public interface IAttendanceRecordCalService
    {
        /// <summary>
        /// 根据id查找到统计考勤数据视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AttendanceRecordCalView FindViewById(int id);

        /// <summary>
        /// 搜索统计考勤信息视图, 包含员工的信息
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IQueryable<AttendanceRecordCalView> SearchCalView(AttendanceRecordCalSearchModel searchModel);


        /// <summary>
        ///  根据统计记录id调整时间工时
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actHour">实际工时</param>
        /// <param name="isExceptionHandled">是否处理了异常</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        ResultMessage UpdateActHourById(int id,double actHour, bool isExceptionHandled,string remark);

        /// <summary>
        /// 根据时间获取未处理的异常统计列表
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="endDate"></param>
        /// <param name="exceptionHandled"></param>
        /// <returns></returns>
        List<AttendanceRecordCalExceptionView> GetCalExceptionHandleList(DateTime fromDate, DateTime endDate, bool exceptionHandled = false);
    }
}
