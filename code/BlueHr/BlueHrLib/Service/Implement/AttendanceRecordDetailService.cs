using BlueHrLib.CusException;
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

namespace BlueHrLib.Service.Implement
{
    public class AttendanceRecordDetailService : ServiceBase, IAttendanceRecordDetailService
    {
        private IAttendanceRecordDetailRepository attendanceRecordDetailRep;
        public AttendanceRecordDetailService(string dbString) : base(dbString) {
            attendanceRecordDetailRep = new AttendanceRecordDetailRepository(this.Context);
        }
    
        /// <summary>
        /// 搜索详细考勤信息
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IQueryable<AttendanceRecordDetail> SearchDetail(AttendanceRecordDetailSearchModel searchModel)
        {
            IAttendanceRecordDetailRepository rep = new AttendanceRecordDetailRepository(new DataContext(this.DbString));
            return rep.Search(searchModel);
        }
        
        /// <summary>
        /// 搜索详细考勤信息视图, 包含员工的信息
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IQueryable<AttendanceRecordDetailView> SearchDetailView(AttendanceRecordDetailSearchModel searchModel)
        {
            IAttendanceRecordDetailViewRepository rep = new AttendanceRecordDetailViewRepository(new DataContext(this.DbString));
            return rep.Search(searchModel);
        }

    

        /// <summary>
        /// 批量创建详细考勤数据
        /// </summary>
        /// <param name="records"></param>
        public void CreateDetails(List<AttendanceRecordDetail> records)
        {
            DataContext dc = new DataContext(this.DbString);
            dc.Context.GetTable<AttendanceRecordDetail>().InsertAllOnSubmit(records);
            dc.Context.SubmitChanges();
        }

        /// <summary>
        /// 根据员工号和考勤时间查询详细记录
        /// </summary>
        /// <param name="nr"></param>
        /// <param name="recordAt"></param>
        /// <returns></returns>
        public AttendanceRecordDetail FindDetailByStaffAndRecordAt(string nr, DateTime recordAt)
        {
            DataContext dc = new DataContext(this.DbString);

            return dc.Context.GetTable<AttendanceRecordDetail>().FirstOrDefault(s => s.staffNr.Equals(nr) && s.recordAt.Equals(recordAt));
        }

        /// <summary>
        /// 根据员工号和日期获取员工的详细打卡记录视图
        /// 重点是，根据日期来判断排班，然后通过排班来查找所有的记录
        /// 如果排班时间开始都是在当前日期的排班，但是计算时要用下面废弃的！
        /// **如果排班的结束时间的HH:mm小于日期的HH:mm，并且结束时间的天等于（或加1等于）日期的天
        /// schedule视图里的fullEnd时间已经是根据排班类型计算过的了 -废弃，Charlot 2016.09.02**
        /// </summary>
        /// <param name="nr"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public List<AttendanceRecordDetailView> GetDetailsViewByStaffAndDate(string nr, DateTime datetime)
        {
            DataContext dc = new DataContext(this.DbString);
            List<AttendanceRecordDetailView> records = new List<AttendanceRecordDetailView>();

            /// 获取所有排班
            List<ShiftScheduleView> shifts= dc.Context.GetTable<ShiftScheduleView>().Where(s =>s.staffNr.Equals(nr) && s.fullStartAt.Value.Date.Equals(datetime.Date) && s.fullEndAt.Value<datetime).ToList();
            /// 系统配置
            SystemSetting setting = dc.Context.GetTable<SystemSetting>().FirstOrDefault();
            if (setting == null)
                throw new SystemSettingNotSetException();

            foreach(ShiftScheduleView s in shifts)
            {
                DateTime sq = s.fullStartAt.Value.AddMinutes(0-setting.validAttendanceRecordTime.Value);
                DateTime eq = s.fullEndAt.Value.AddMinutes(setting.validAttendanceRecordTime.Value);

                List<AttendanceRecordDetailView> shiftAttendRecords = dc.Context.GetTable<AttendanceRecordDetailView>().Where(ss=>ss.recordAt>=sq && ss.recordAt<=eq && ss.staffNr.Equals(nr)).OrderBy(ss=>ss.recordAt).ToList() ;//new List<AttendanceRecordDetailView>();

                records.AddRange(shiftAttendRecords);
            }

            records = records.Distinct().ToList();
            
            return records;
        }

        /// <summary>
        /// 获取某一天的打卡记录，无论排班是否开始或结束
        /// </summary>
        /// <param name="nr"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public List<AttendanceRecordDetailView> GetDetailsViewByStaffAndDateWithExtrawork(string nr, DateTime datetime)
        {
            List<AttendanceRecordDetailView> records = new List<AttendanceRecordDetailView>();

            SystemSetting setting = new SystemSettingService(this.DbString).Find();
            DataContext dc = new DataContext(this.DbString);
            List<ShiftScheduleView> shifts = dc.Context.GetTable<ShiftScheduleView>().Where(s => s.staffNr.Equals(nr) && s.fullStartAt.Value.Date.Equals(datetime.Date)).OrderBy(s => s.fullStartAt).ToList();
            if (shifts.Count > 0)
            {
                /// 找出在单个shift之间的
                foreach (var s in shifts)
                {
                    DateTime sq = s.fullStartAt.Value.AddMinutes(0 - setting.validAttendanceRecordTime.Value);
                    DateTime eq = s.fullEndAt.Value.AddMinutes(setting.validAttendanceRecordTime.Value);
                    List<AttendanceRecordDetailView> shiftAttendRecords = dc.Context.GetTable<AttendanceRecordDetailView>().Where(ss => ss.recordAt >= sq && ss.recordAt <= eq && ss.staffNr.Equals(nr)).OrderBy(ss => ss.recordAt).ToList();//new List<AttendanceRecordDetailView>();
                   // records.AddRange(shiftAttendRecords);
                    foreach (var r in shiftAttendRecords)
                    {
                        if (records.FirstOrDefault(ss => s.id == r.id) == null)
                        {
                            records.Add(r);
                        }
                    }
                }

                /// 找出每两个shift之间的，及A的结束到B的开始，如果没有B，则A的开始到datetime的次日23:59:59

                foreach (var firstShift in shifts)
                {
                    ShiftScheduleView nextShift = dc.Context.GetTable<ShiftScheduleView>().Where(s => s.staffNr.Equals(nr) && s.fullStartAt > firstShift.fullEndAt).OrderBy(s => s.fullStartAt).FirstOrDefault();

                    DateTime sq = firstShift.fullEndAt.Value.AddMinutes(0 + setting.validAttendanceRecordTime.Value);
                    DateTime eq = nextShift == null ? datetime.Date.AddDays(1).Add(new TimeSpan(23, 59, 59)) : nextShift.fullStartAt.Value.AddMinutes(0 - setting.validAttendanceRecordTime.Value);
                    List<AttendanceRecordDetailView> shiftAttendRecords = dc.Context.GetTable<AttendanceRecordDetailView>().Where(ss => ss.recordAt >= sq && ss.recordAt <= eq && ss.staffNr.Equals(nr)).OrderBy(ss => ss.recordAt).ToList();//new List<AttendanceRecordDetailView>();
                                                                                                                                                                                                                                             // records.AddRange(shiftAttendRecords);
                    foreach (var r in shiftAttendRecords)
                    {
                        if (records.FirstOrDefault(ss => ss.id == r.id) == null)
                        {
                            records.Add(r);
                        }
                    }
                }
            }
            else
            {
                //ShiftScheduleView prevShift = dc.Context.GetTable<ShiftScheduleView>().Where(s => s.staffNr.Equals(nr) && s.fullEndAt < datetime).OrderByDescending(s => s.fullEndAt).FirstOrDefault();
                //ShiftScheduleView nextShift= dc.Context.GetTable<ShiftScheduleView>().Where(s => s.staffNr.Equals(nr) && s.fullStartAt > datetime).OrderBy(s => s.fullStartAt).FirstOrDefault();
                ///// 如果没有排班，则找出前一天开始，到次日的结束
                //DateTime sq = prevShift== null? datetime.Date.AddDays(-1) : prevShift.fullEndAt.Value.AddMinutes(0 + setting.validAttendanceRecordTime.Value);
                //DateTime eq = nextShift == null ? datetime.Date.AddDays(1).Add(new TimeSpan(23, 59, 59)) : nextShift.fullStartAt.Value.AddMinutes(0 - setting.validAttendanceRecordTime.Value);

                //List<AttendanceRecordDetailView> shiftAttendRecords = dc.Context.GetTable<AttendanceRecordDetailView>().Where(ss => ss.recordAt >= sq && ss.recordAt <= eq && ss.staffNr.Equals(nr)).OrderBy(ss => ss.recordAt).ToList();//new List<AttendanceRecordDetailView>();
                //records.AddRange(shiftAttendRecords);
            }
            List<AttendanceRecordDetailView> todayRecords = dc.Context.GetTable<AttendanceRecordDetailView>().Where(ss => ss.recordAt >= datetime.Date && ss.recordAt <= datetime.Date.Add(new TimeSpan(23, 59, 59)) && ss.staffNr.Equals(nr)).OrderBy(ss => ss.recordAt).ToList();
         foreach(var r in todayRecords)
            {
                if (records.FirstOrDefault(s => s.id == r.id)==null)
                {
                    records.Add(r);
                }
            }
            return records.Distinct().OrderBy(ss => ss.recordAt).ToList(); ;
        }

        public List<AttendanceRecordDetail> GetByStaffAndTimespan(string staffNr, DateTime startTime, DateTime endTime)
        {

            return new DataContext(this.DbString).Context.GetTable<AttendanceRecordDetail>().Where(s => s.staffNr.Equals(staffNr) && s.recordAt >= startTime && s.recordAt <= endTime).OrderBy(s => s.recordAt).ToList();
        }

        public bool Create(AttendanceRecordDetail attendanceRecordDetail)
        {
            return attendanceRecordDetailRep.Create(attendanceRecordDetail);
        }

        public bool Update(AttendanceRecordDetail attendanceRecordDetail)
        {
            return attendanceRecordDetailRep.Update(attendanceRecordDetail);
        }

        public AttendanceRecordDetail FindById(int id)
        {
            return attendanceRecordDetailRep.FindById(id);
        }

        public bool DeleteById(int id)
        {
            return attendanceRecordDetailRep.DeleteById(id);
        }
    }
}
