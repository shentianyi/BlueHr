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

namespace BlueHrLib.Service.Implement
{
    public class AttendanceRecordService : ServiceBase, IAttendanceRecordService
    {
        public AttendanceRecordService(string dbString) : base(dbString) { }

        public void CalculateAttendRecord(DateTime date)
        {
            DataContext dc = new DataContext(this.DbString);
            /// 全部员工
            List<Staff> staffs = dc.Context.GetTable<Staff>().ToList();
            SystemSetting setting = dc.Context.GetTable<SystemSetting>().FirstOrDefault();
            if (setting == null)
                throw new SystemSettingNotSetException();

            Dictionary<string, List<AttendanceRecordCal>> staffAttendCals = new Dictionary<string, List<AttendanceRecordCal>>();

            /// 循环每一个员工
            foreach (Staff staff in staffs)
            {
                Dictionary<DateTime, double> totalWorkingHours = new Dictionary<DateTime, double>();
                Dictionary<DateTime, List<AttendanceExceptionType>> exceptions = new Dictionary<DateTime, List<AttendanceExceptionType>>();
                // 获取今日、昨日的所有排班
                List<ShiftScheduleView> staffShitSchedules = dc.Context.GetTable<ShiftScheduleView>()
                    .Where(s => s.staffNr.Equals(staff.nr) && (s.scheduleAt.Date.Equals(date.Date) || (s.scheduleAt.Equals(date.Date.AddDays(-1)) && s.shiftType.Equals((int)ShiftType.Tommorrow)))).ToList();

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
                    DateTime shiftDate = date.AddDays(shift.shiftType.Equals((int)ShiftType.Today) ? 0 : -1);

                    totalWorkingHours[shiftDate] = 0;
                    exceptions[shiftDate] = new List<AttendanceExceptionType>();


                  //  DateTime shiftStart = shift.scheduleAt.AddDays(shift.shiftType.Equals((int)ShiftType.Today) ? 0 : -1).Add(shift.startAt);

                    DateTime shiftStart = shift.scheduleAt.Add(shift.startAt);
                    DateTime shiftEnd = shift.scheduleAt.AddDays(shift.shiftType.Equals((int)ShiftType.Today) ? 0 : 1).Add(shift.endAt);

                    DateTime sq = shiftStart.AddHours(0 - setting.validAttendanceRecordTime.Value);
                    DateTime eq = shiftEnd.AddHours(setting.validAttendanceRecordTime.Value);

                    List<AttendanceRecordDetail> records = staff.AttendanceRecordDetail.Where(s => s.recordAt >= sq && s.recordAt <= eq).ToList().OrderBy(s => s.recordAt).ToList();

                    if (records.Count == 0)
                    {
                        /// TODO UPDATE OR CREATE
                        /// 创建异常，班次旷工，考勤的时间为 0
                        exceptions[shiftDate].Add(AttendanceExceptionType.ShiftAbsence);
                    }
                    else {

                        /// 清洗短时间内的重复打卡数据
                        MarkRepeatRecord(records, (float)setting.repeatAttendanceRecordTime.Value);
                        List<AttendanceRecordDetail> validRecrods = records.Where(s => s.isRepeatedData == false).ToList();
                        if (validRecrods.Count == 1)
                        {
                            /// TODO UPDATE OR CREATE
                            /// 创建异常，考勤记录不完整，考勤的时间为 0
                            exceptions[shiftDate].Add(AttendanceExceptionType.MessRecord);
                        }
                        else {
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
                staffAttendCals.Add(staff.nr, new List<AttendanceRecordCal>());
                // 创建, 计算过的出勤时间
                foreach (var dic in totalWorkingHours)
                {
                    AttendanceRecordCal cal = new AttendanceRecordCal() { attendanceExceptions = exceptions[dic.Key], createdAt = DateTime.Now, oriWorkingHour = dic.Value, actWorkingHour = dic.Value, staffNr = staff.nr, attendanceDate = dic.Key };
                    staffAttendCals[staff.nr].Add(cal);
                }

            }

            using (TransactionScope scope = new TransactionScope())
            {
                DataContext subDc = new DataContext(this.DbString);
                List<AttendanceRecordCal> insertCals = new List<AttendanceRecordCal>();
                List<AttendanceRecordCal> updateCals = new List<AttendanceRecordCal>();
                foreach (var dic in staffAttendCals)
                {
                    /// 手动修改的也会被删除
                    List<AttendanceRecordCal> _updateCals = subDc.Context.GetTable<AttendanceRecordCal>().Where(s => staffAttendCals.Keys.Contains(s.staffNr) && (staffAttendCals[dic.Key].Select(ss => ss.attendanceDate).ToList().Contains(s.attendanceDate))).ToList();
                    foreach(var u in _updateCals)
                    {
                        var c = dic.Value.Where(d => d.attendanceDate.Equals(u.attendanceDate)).FirstOrDefault();

                        if (c != null)
                        {
                            u.oriWorkingHour = u.actWorkingHour = c.oriWorkingHour;
                            u.isManualCal = false;
                            u.createdAt = DateTime.Now;
                            u.isException = c.isException;
                            u.exceptionCodes = c.exceptionCodes;
                        }
                    }
                    List<AttendanceRecordCal> _insertCals = dic.Value.Where(s => !_updateCals.Select(ss => ss.attendanceDate).Contains(s.attendanceDate)).ToList();
                    insertCals.AddRange(_insertCals);
                }
                subDc.Context.GetTable<AttendanceRecordCal>().InsertAllOnSubmit(insertCals);
                subDc.Context.SubmitChanges();
                
                /// scope 完成
                scope.Complete();
            }
        }

        public void MarkRepeatRecord(List<AttendanceRecordDetail> records, float repeatFlag)
        {
            for(int i=0;i<records.Count-1;i++)
            {
                MarkRepeatRecordCompare(records[i], records.Where(s=>s.isRepeatedData==false).Skip(i + 1).Take(records.Count - 1).ToList(), repeatFlag);
            }
        }

        public void MarkRepeatRecordCompare(AttendanceRecordDetail baseRecord,List<AttendanceRecordDetail> records, float repeatFlag)
        {
            foreach (var r in records)
            {
                r.isRepeatedData = Math.Abs((baseRecord.recordAt - r.recordAt).TotalSeconds) < repeatFlag;
            }
        }

    }
}
