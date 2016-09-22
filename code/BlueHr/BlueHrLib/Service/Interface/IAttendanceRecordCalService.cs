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
        /// 根据id查找到统计考勤数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AttendanceRecordCal FindById(int id);


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
        ResultMessage UpdateActHourById(int id,double actHour,double actExtraHour, bool isExceptionHandled,string remark, int? extraWorkType);

        /// <summary>
        /// 根据时间获取未处理的异常统计列表
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="endDate"></param>
        /// <param name="exceptionHandled"></param>
        /// <returns></returns>
        List<AttendanceRecordCalExceptionView> GetCalExceptionHandleList(DateTime fromDate, DateTime endDate, bool exceptionHandled = false);


        /// <summary>
        /// 发送考勤异常邮件
        /// </summary>
        /// <param name="date"></param>
        /// <param name="shiftCodes"></param>
        void SendWarnEmail(DateTime date,List<string> shiftCodes=null);

        /// <summary>
        /// 根据时间、是否异常、是否异常处理获取考勤统计列表
        /// </summary>
        /// <param name="attendanceDate">考勤日期</param>
        /// <param name="isException">是否异常</param>
        /// <param name="isExceptionHandled">是否已异常处理</param>
        /// <returns></returns>
        List<AttendanceRecordCalView> GetListByDateAndIsException(DateTime attendanceDate,  bool isException = true, bool isExceptionHandled = false);

        /// <summary>
        /// 创建考勤统计数据
        /// </summary>
        /// <param name="attendanceRecorCal">统计考勤 参数</param>
        /// <returns></returns>
        bool Create(AttendanceRecordCal attendanceRecordCal);

        /// <summary>
        /// 通过ID 删除 统计考勤数据
        /// </summary>
        /// <param name="id"> 统计考勤 ID</param>
        /// <returns></returns>
        bool DeleteById(int id);

    }
}
