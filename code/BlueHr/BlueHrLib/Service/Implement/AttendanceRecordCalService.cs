﻿using BlueHrLib.CusException;
using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Message;

namespace BlueHrLib.Service.Implement
{
    public class AttendanceRecordCalService : ServiceBase, IAttendanceRecordCalService
    {
        public AttendanceRecordCalService(string dbString) : base(dbString) { }

        public AttendanceRecordCalView FindViewById(int id)
        {
            IAttendanceRecordCalViewRepository rep = new AttendanceRecordCalViewRepository(new DataContext(this.DbString));
            return rep.FindById(id);
        }



        /// <summary>
        /// 搜索统计考勤信息视图, 包含员工的信息
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IQueryable<AttendanceRecordCalView> SearchCalView(AttendanceRecordCalSearchModel searchModel)
        {
            IAttendanceRecordCalViewRepository rep = new AttendanceRecordCalViewRepository(new DataContext(this.DbString));
            return rep.Search(searchModel);
        }

        /// <summary>
        ///  根据统计记录id调整时间工时
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actHour">实际工时</param>
        /// <param name="isExceptionHandled">是否处理了异常</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public ResultMessage UpdateActHourById(int id, double actHour, bool isExceptionHandled,string remark)
        {
            ResultMessage msg = new ResultMessage();
            try {
                DataContext dc = new DataContext(this.DbString);

                AttendanceRecordCal record = dc.Context.GetTable<AttendanceRecordCal>().FirstOrDefault(s => s.id.Equals(id));
                if (record == null)
                {
                    throw new DataNotFoundException();
                }

                record.actWorkingHour = actHour;
                record.isManualCal = true;
                record.isExceptionHandled = isExceptionHandled;
                record.remark = remark;
                dc.Context.SubmitChanges();
                msg.Success = true;
            } catch(Exception ex)
            {
                msg.Content = ex.Message;
            }
            return msg;
        }

        /// <summary>
        /// 根据时间获取未处理的异常统计列表
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="endDate"></param>
        /// <param name="exceptionHandled"></param>
        /// <returns></returns>
        public List<AttendanceRecordCalExceptionView> GetCalExceptionHandleList(DateTime fromDate, DateTime endDate, bool exceptionHandled = false)
        {
            DataContext dc = new DataContext(this.DbString);

            return dc.Context.GetTable<AttendanceRecordCalExceptionView>()
                .Where(s => s.attendanceDate >= fromDate && s.attendanceDate <= endDate && s.isExceptionHandled==exceptionHandled)
                .OrderByDescending(s=>s.attendanceDate).ToList();
        }

    }
}