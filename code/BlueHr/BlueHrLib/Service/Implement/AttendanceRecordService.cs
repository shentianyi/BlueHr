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
    public class AttendanceRecordService : ServiceBase, IAttendanceRecordService
    {
        public AttendanceRecordService(string dbString) : base(dbString) { }

        /// <summary>
        /// 将Detail的数据计算到Cal中
        /// 将班次结束点日期等于date日期，结束点时间小于等于date时间的班次考勤进行计算
        /// </summary>
        /// <param name="date">计算的时间</param>
        /// <param name="shiftCodes">班次代码</param>
        /// <param name="searchModel">需要计算的员工的查询条件, **NOT IMPLEMENT**</param>
        public void CalculateAttendRecord(DateTime date, List<string> shiftCodes = null, StaffSearchModel searchModel = null)
        {
            DataContext dc = new DataContext(this.DbString);
            /// 判断配置
            SystemSetting setting = dc.Context.GetTable<SystemSetting>().FirstOrDefault();
            if (setting == null)
                throw new SystemSettingNotSetException();

            /// 查找员工的排班
            //IStaffService staffService = new StaffService(this.DbString);
            //List<Staff> staffs = staffService.Search(searchModel).ToList();
            var allShiftShedulesQ = dc.Context.GetTable<ShiftScheduleView>().Where(s => s.fullEndAt.Value <= date && s.fullEndAt.Value.Date.Equals(date.Date));
            if (shiftCodes != null && shiftCodes.Count > 0)
            {
                allShiftShedulesQ = allShiftShedulesQ.Where(s => shiftCodes.Contains(s.code));

            }
            List<ShiftScheduleView> allShiftShedules = allShiftShedulesQ.ToList();
            Dictionary<string, List<ShiftScheduleView>> allStaffShitSchedules = new Dictionary<string, List<ShiftScheduleView>>();
            foreach (var ssv in allShiftShedules)
            {
                if (!allStaffShitSchedules.ContainsKey(ssv.staffNr))
                {
                    allStaffShitSchedules.Add(ssv.staffNr, new List<ShiftScheduleView>());
                }
                allStaffShitSchedules[ssv.staffNr].Add(ssv);
            }
            Dictionary<string, List<AttendanceRecordCal>> staffAttendCals = new Dictionary<string, List<AttendanceRecordCal>>();

            /// 循环每一个员工
            foreach (var staffShiftShedule in allStaffShitSchedules)
            {
                Dictionary<DateTime, double> totalWorkingHours = new Dictionary<DateTime, double>();
                Dictionary<DateTime, List<AttendanceExceptionType>> exceptions = new Dictionary<DateTime, List<AttendanceExceptionType>>();
                // 获取今日、昨日的所有排班
                List<ShiftScheduleView> staffShitSchedules = staffShiftShedule.Value;// dc.Context.GetTable<ShiftScheduleView>()
                                                                                     // .Where(s => s.staffNr.Equals(staff.nr) && (s.scheduleAt.Date.Equals(date.Date) || (s.scheduleAt.Equals(date.Date.AddDays(-1)) && s.shiftType.Equals((int)ShiftType.Tommorrow)))).ToList();

                if (staffShitSchedules.Count == 0)
                {
                    //TODO UPDATE OR CREATE
                    //AttendanceRecordCal recordCal = new AttendanceRecordCal() { staffNr = staff.nr, actWorkingHour = 0, oriWorkingHour = 0, attendanceDate = date, createdAt = DateTime.Now, isManualCal = false, remark = "没有排班" };
                    //recordCal.actWorkingHour = recordCal.oriWorkingHour = 0;

                    /// 如果没有排班则跳过, 写入出勤时间为0 ?
                    break;
                }

                foreach (var shift in staffShitSchedules)
                {
                    DateTime shiftDate = date.Date.AddDays(shift.shiftType.Equals((int)ShiftType.Today) ? 0 : -1);

                    totalWorkingHours[shiftDate] = 0;
                    exceptions[shiftDate] = new List<AttendanceExceptionType>();


                    //  DateTime shiftStart = shift.scheduleAt.AddDays(shift.shiftType.Equals((int)ShiftType.Today) ? 0 : -1).Add(shift.startAt);

                    DateTime shiftStart = shift.scheduleAt.Add(shift.startAt);
                    DateTime shiftEnd = shift.scheduleAt.AddDays(shift.shiftType.Equals((int)ShiftType.Today) ? 0 : 1).Add(shift.endAt);

                    DateTime sq = shiftStart.AddHours(0 - setting.validAttendanceRecordTime.Value);
                    DateTime eq = shiftEnd.AddHours(setting.validAttendanceRecordTime.Value);

                    List<AttendanceRecordDetail> records = dc.Context.GetTable<AttendanceRecordDetail>().Where(s => s.staffNr.Equals(staffShiftShedule.Key) && s.recordAt >= sq && s.recordAt <= eq).OrderBy(s => s.recordAt).ToList();
                    //staff.AttendanceRecordDetail.Where(s => s.recordAt >= sq && s.recordAt <= eq).ToList().OrderBy(s => s.recordAt).ToList();

                    if (records.Count == 0)
                    {
                        /// TODO UPDATE OR CREATE
                        /// 创建异常，班次旷工，考勤的时间为 0
                        exceptions[shiftDate].Add(AttendanceExceptionType.ShiftAbsence);
                    }
                    else
                    {

                        /// 清洗短时间内的重复打卡数据
                        MarkRepeatRecord(records, (float)setting.repeatAttendanceRecordTime.Value);
                        List<AttendanceRecordDetail> validRecrods = records.Where(s => s.isRepeatedData == false).ToList();
                        if (validRecrods.Count == 1)
                        {
                            /// TODO UPDATE OR CREATE
                            /// 创建异常，考勤记录不完整，考勤的时间为 0
                            exceptions[shiftDate].Add(AttendanceExceptionType.MessRecord);
                        }
                        else
                        {
                            // 判断迟到
                            DateTime firstAt = validRecrods.First().recordAt;
                            if (firstAt.AddMinutes(0 - setting.lateExceptionTime.Value) > shiftStart)
                            {
                                /// TODO UPDATE OR CREATE
                                /// 创建异常，迟到
                                exceptions[shiftDate].Add(AttendanceExceptionType.Late);
                            }

                            DateTime lastAt = validRecrods.Last().recordAt;
                            if (lastAt.AddMinutes(setting.earlyLeaveExceptionTime.Value) < shiftEnd)
                            {
                                /// TODO UPDATE OR CREATE
                                /// 创建早退，迟到
                                exceptions[shiftDate].Add(AttendanceExceptionType.EarlyLeave);
                            }

                            if (validRecrods.Count % 2 == 1)
                            {
                                /// TODO UPDATE OR CREATE
                                /// 创建异常，考勤记录不完整，考勤的时间为 0
                                exceptions[shiftDate].Add(AttendanceExceptionType.MessRecord);
                            }

                            for (int i = 0; i < validRecrods.Count - 1; i += 2)
                            {
                                totalWorkingHours[shiftDate] += (validRecrods[i + 1].recordAt - validRecrods[i].recordAt).TotalHours;
                            }
                        }
                    }
                }
                staffAttendCals.Add(staffShiftShedule.Key, new List<AttendanceRecordCal>());
                // 创建, 计算过的出勤时间
                foreach (var dic in totalWorkingHours)
                {
                    AttendanceRecordCal cal = new AttendanceRecordCal()
                    {
                        attendanceExceptions = exceptions[dic.Key],
                        createdAt = DateTime.Now,
                        oriWorkingHour = dic.Value,
                        actWorkingHour = dic.Value,
                        staffNr = staffShiftShedule.Key,
                        attendanceDate = dic.Key
                    };
                    staffAttendCals[staffShiftShedule.Key].Add(cal);
                }

            }

            //using (TransactionScope scope = new TransactionScope())
            //{
            DataContext subDc = new DataContext(this.DbString);
            List<AttendanceRecordCal> insertCals = new List<AttendanceRecordCal>();
            List<AttendanceRecordCal> updateCals = new List<AttendanceRecordCal>();
            foreach (var dic in staffAttendCals)
            {
                /// 手动修改的不会被修改实际值
                List<AttendanceRecordCal> _updateCals = subDc.Context.GetTable<AttendanceRecordCal>().Where(s => staffAttendCals.Keys.Contains(s.staffNr) && (staffAttendCals[dic.Key].Select(ss => ss.attendanceDate).ToList().Contains(s.attendanceDate))).ToList();
                foreach (var u in _updateCals)
                {
                    var c = dic.Value.Where(d => d.attendanceDate.Equals(u.attendanceDate)).FirstOrDefault();

                    if (c != null)
                    {
                        u.oriWorkingHour = u.actWorkingHour = c.oriWorkingHour;
                        u.isManualCal = false;
                        if (c != null)
                        {
                            u.oriWorkingHour = c.oriWorkingHour;
                            if (u.isManualCal == false)
                            {
                                u.actWorkingHour = c.oriWorkingHour;
                                u.isManualCal = false;
                            }
                            u.createdAt = DateTime.Now;
                            u.isException = c.isException;
                            u.exceptionCodes = c.exceptionCodes;
                        }
                    }

                }
                List<AttendanceRecordCal> _insertCals = dic.Value.Where(s => !_updateCals.Select(ss => ss.attendanceDate).Contains(s.attendanceDate)).ToList();
                insertCals.AddRange(_insertCals);
                subDc.Context.GetTable<AttendanceRecordCal>().InsertAllOnSubmit(insertCals);
                subDc.Context.SubmitChanges();

                /// scope 完成
                //  scope.Complete();
                // }
            }
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

        private void MarkRepeatRecord(List<AttendanceRecordDetail> records, float repeatFlag)
        {
            for(int i=0;i<records.Count-1;i++)
            {
                MarkRepeatRecordCompare(records[i], records.Where(s=>s.isRepeatedData==false).Skip(i + 1).Take(records.Count - 1).ToList(), repeatFlag);
            }
        }

        private void MarkRepeatRecordCompare(AttendanceRecordDetail baseRecord,List<AttendanceRecordDetail> records, float repeatFlag)
        {
            foreach (var r in records)
            {
                r.isRepeatedData = Math.Abs((baseRecord.recordAt - r.recordAt).TotalSeconds) < repeatFlag;
            }
        }

    }
}
