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
        /// <param name="dateTime">计算的时间</param>
        /// <param name="shiftCodes">班次代码</param>
        /// <param name="searchModel">需要计算的员工的查询条件, **NOT IMPLEMENT**</param>
        public void CalculateAttendRecord(DateTime dateTime, List<string> shiftCodes = null, StaffSearchModel searchModel = null)
        {
            DataContext dc = new DataContext(this.DbString);
            /// 判断配置
            SystemSetting setting = dc.Context.GetTable<SystemSetting>().FirstOrDefault();
            if (setting == null)
                throw new SystemSettingNotSetException();

            /// 查找员工的需要被计算的排班
            //IStaffService staffService = new StaffService(this.DbString);
            //List<Staff> staffs = staffService.Search(searchModel).ToList();
            var allShiftShedulesQ = dc.Context.GetTable<ShiftScheduleView>().Where(s => s.fullEndAt.Value <= dateTime && s.fullEndAt.Value.Date.Equals(dateTime.Date));
            if (shiftCodes != null && shiftCodes.Count > 0)
            {
                if (shiftCodes.Count > 1)
                {
                    allShiftShedulesQ = allShiftShedulesQ.Where(s => shiftCodes.Contains(s.code));
                }
                else
                {
                    allShiftShedulesQ = allShiftShedulesQ.Where(s => s.code.Equals(shiftCodes.First()));
                }
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
                    DateTime shiftDate = shift.fullStartAt.Value.Date;// dateTime.Date.AddDays(shift.shiftType.Equals((int)ShiftType.Today) ? 0 : -1);
                    if (!totalWorkingHours.ContainsKey(shiftDate))
                    {
                        totalWorkingHours.Add(shiftDate, 0);
                    }
                    exceptions[shiftDate] = new List<AttendanceExceptionType>();


                    //  DateTime shiftStart = shift.scheduleAt.AddDays(shift.shiftType.Equals((int)ShiftType.Today) ? 0 : -1).Add(shift.startAt);

                    DateTime shiftStart = shift.fullStartAt.Value;//shift.scheduleAt.Add(shift.startAt);
                    DateTime shiftEnd = shift.fullEndAt.Value; //shift.scheduleAt.AddDays(shift.shiftType.Equals((int)ShiftType.Today) ? 0 : 1).Add(shift.endAt);

                    DateTime sq = shiftStart.AddMinutes(0 - setting.validAttendanceRecordTime.Value);
                    DateTime eq = shiftEnd.AddMinutes(setting.validAttendanceRecordTime.Value);

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
                        oriWorkingHour = Math.Round(dic.Value, 2),
                        actWorkingHour = Math.Round(dic.Value, 2),
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

            foreach (var dic in staffAttendCals)
            {
               // string nrsq = string.Format(",{0},", string.Join(",", staffAttendCals.Keys));
                // string dateq = string.Format(",{0},", string.Join(",", staffAttendCals[dic.Key].Select(ss => ss.attendanceDate.ToString("yyyy-MM-dd")).ToList()));
                /// 手动修改的不会被修改实际值
                //List<AttendanceRecordCal> _updateCals = subDc.Context.GetTable<AttendanceRecordCal>()
                //    .Where(s => staffAttendCals.Keys.Contains(s.staffNr)
                //    && (staffAttendCals[dic.Key].Select(ss => ss.attendanceDate).ToList()
                //    .Contains(s.attendanceDate))).ToList();
                //List<AttendanceRecordCal> _updateCals = subDc.Context.GetTable<AttendanceRecordCal>()
                // .AsEnumerable()
                //.Join(staffAttendCals.Keys,s=>s.staffNr,ci=>ci, (s,ci)=> s)
                //.Join(staffAttendCals[dic.Key].Select(ss => ss.attendanceDate).ToList(),sss=>sss.attendanceDate,cci=>cci,(sss,cci)=>sss).ToList();

                List<AttendanceRecordCal> _updateCals = new List<AttendanceRecordCal>();
                //   IQueryable<AttendanceRecordCal> _updateCalsQ = dc.Context.GetTable<AttendanceRecordCal>()
                //.Where(s => nrsq.IndexOf("," + s.staffNr + ",") != -1);
                IQueryable<AttendanceRecordCal> _updateCalsQ = dc.Context.GetTable<AttendanceRecordCal>()
             .Where(s => s.staffNr.Equals(dic.Key));
                if (staffAttendCals[dic.Key].Count == 0)
                {

                }
               else if (staffAttendCals[dic.Key].Count == 1)
                {
                    _updateCals = _updateCalsQ.Where(s => s.attendanceDate.Equals(staffAttendCals[dic.Key].First().attendanceDate)).ToList();
                }
                else
                {
                    _updateCals = _updateCalsQ.Where(s => staffAttendCals[dic.Key].Select(ss => ss.attendanceDate).ToList().Contains(s.attendanceDate)).ToList();
                }

                //.Where(ss => dateq.IndexOf("," + ss.attendanceDate.ToString("yyyy-MM-dd") + ",") != -1).ToList();


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

                /// scope 完成
                //  scope.Complete();
                // }
            }

            subDc.Context.GetTable<AttendanceRecordCal>().InsertAllOnSubmit(insertCals);

            subDc.Context.SubmitChanges();

        }
        
        public void CalculateAttendRecordWithExtrawork(DateTime datetime, List<string> shiftCodes = null, StaffSearchModel searchModel = null)
        {
            // 是否是休息日
            IWorkAndRestService wars = new WorkAndRestService(this.DbString);
            WorkAndRest wr = wars.FindByDate(datetime);
            bool isRestDay = wars.IsRestDay(wr);
            SystemSetting setting = new SystemSettingService(this.DbString).Find();
            

            DataContext dc = new DataContext(this.DbString);

            
            

            // 计算在职人员的考勤
            List<Staff> staffs = dc.Context.GetTable<Staff>().Where(s => (s.workStatus == (int)WorkStatus.OnWork) || (s.workStatus==(int) WorkStatus.OffWork && s.resignAt>=datetime)).ToList();
            foreach(Staff staff in staffs)
            {
                double workdayHour = 0;

                double extraHour = 0;
                // 异常情况
                List<AttendanceExceptionType> exceptions = new List<AttendanceExceptionType>();
                if (staff.nr.Equals("201471"))
                {
                    string s = "201471";
                }
                // 获取已经结束的排班
                List<ShiftScheduleView> shifts = dc.Context.GetTable<ShiftScheduleView>().Where(s => s.staffNr.Equals(staff.nr) && s.fullStartAt.Value.Date == datetime.Date && s.fullEndAt <= DateTime.Now).ToList();
                if (shifts.Count > 0)
                {
                    foreach (var shift in shifts)
                    {
                        DateTime validSQ= shift.fullStartAt.Value.AddMinutes(0 - setting.validAttendanceRecordTime.Value);
                        DateTime validEQ=shift.fullEndAt.Value.AddMinutes(0 + setting.validAttendanceRecordTime.Value);
                        DateTime sq = validSQ;
                        DateTime eq = shift.shiftType == (int)ShiftType.Today ? datetime.Date.AddDays(1).AddHours(8) : datetime.Date.AddDays(1).AddHours(23);

                        ShiftScheduleView nextShift = new ShiftSheduleService(this.DbString).GetNextShiftScheduleView(staff.nr, shift.fullEndAt.Value);

                        if (nextShift != null)
                        {
                            if ((nextShift.fullStartAt.Value.Date - shift.fullStartAt.Value.Date).TotalDays < 2)
                            {
                                eq =nextShift.fullStartAt.Value.AddMinutes(0 - setting.validAttendanceRecordTime.Value);
                            }
                        }

                        List<AttendanceRecordDetail> records = new AttendanceRecordDetailService(this.DbString).GetByStaffAndTimespan(staff.nr, sq, eq);
                        if (records.Count == 0)
                        {
                            // 旷工，可能他有请假。。。
                            exceptions.Add(AttendanceExceptionType.Absence);
                        }
                        else
                        {
                            /// 清洗数据
                            MarkRepeatRecordByMinute(records, (float)setting.repeatAttendanceRecordTime.Value);
                            records = records.Where(s => s.isRepeatedData == false).ToList();

                            // 如果考勤记录不配对，如 进-出 的偶数被，则是打开记录不完整
                            // 但是这不影响计算
                            if (records.Count % 2 != 0)
                            {
                                exceptions.Add(AttendanceExceptionType.MessRecord);
                            }

                            /// 开始计算
                            /// 早到的使用排班开始时间，晚到的算迟到，提早走的算早退
                            /// 此处不涉及到多次进出-计算目前只能算出大概，很难精确
                            /// 打卡迟了肯定算迟到
                            /// 如果早退了一般不会来加班了
                            // 是否有早打卡？
                            // int hasEarlyCount= records.Where(s => s.recordAt < shift.fullStartAt.Value).Count();

                            if (records.Count == 1) {
                                exceptions.Add(AttendanceExceptionType.MessRecord);
                                /// 以下都是推测，都是要人为的调整的
                                DateTime recordAt = records[0].recordAt;
                                // 如果是早来，迟走，可能是忘记打卡了
                                if (recordAt < shift.fullStartAt.Value || (records.Last().recordAt >= shift.fullEndAt.Value && recordAt <= validEQ))
                                {
                                    /// 如果早来超过一个小时，可能是加班
                                    if (recordAt <= shift.fullStartAt.Value.AddHours(-1))
                                    {
                                        extraHour = (shift.fullStartAt.Value - recordAt).TotalHours;
                                    }
                                    workdayHour = (shift.fullEndAt.Value - shift.fullStartAt.Value).TotalHours;
                                }// 如果迟走超过一个小时，可能是加班，即使此处误判，也需要加班单存在的
                                else if (recordAt >= shift.fullEndAt.Value.AddHours(1))
                                {
                                    exceptions.Add(AttendanceExceptionType.ExtraWork);
                                    workdayHour = (shift.fullEndAt.Value - shift.fullStartAt.Value).TotalHours;
                                    extraHour = (recordAt - shift.fullEndAt.Value).TotalHours;
                                }
                                else
                                {
                                    // 如果只有一次打卡记录在应该的工作区间内，则需要人工手动调整了！
                                }
                            }
                            else
                            {
                                DateTime firstD = records.First().recordAt;
                                DateTime lastD = records.Last().recordAt;
                                // 正常上班，无加班
                                // 1. 如果走时间在排班结束范围外内，则算整个排班时间
                                if (  lastD >= shift.fullEndAt.Value &&
                                      lastD <= validEQ)
                                {
                                    if (firstD <= shift.fullStartAt)
                                    {
                                        /// 如果早来超过一个小时，可能是加班
                                        if (firstD <= shift.fullStartAt.Value.AddHours(-1))
                                        {
                                            extraHour = (shift.fullStartAt.Value - firstD).TotalHours;
                                        }
                                        // 正常
                                        workdayHour = (shift.fullEndAt.Value - shift.fullStartAt.Value).TotalHours;
                                    }
                                    else
                                    {
                                        // 迟到
                                        exceptions.Add(AttendanceExceptionType.Late);
                                        workdayHour = (shift.fullEndAt.Value - firstD).TotalHours;

                                    }
                                }
                                else
                                {
                                    if (lastD < shift.fullEndAt.Value)
                                    {
                                        // 早退
                                        exceptions.Add(AttendanceExceptionType.EarlyLeave);
                                        if (firstD < shift.fullStartAt)
                                        {
                                            /// 如果早来超过一个小时，可能是加班
                                            if (firstD <= shift.fullStartAt.Value.AddHours(-1))
                                            {
                                                extraHour = (shift.fullStartAt.Value - firstD).TotalHours;
                                            }
                                            workdayHour = (lastD- shift.fullStartAt.Value).TotalHours;
                                        }
                                        else
                                        {
                                            // 迟到+早退
                                            exceptions.Add(AttendanceExceptionType.Late);
                                            workdayHour = (lastD - firstD).TotalHours;
                                        }
                                    }
                                    else
                                    {
                                        if (lastD >= shift.fullEndAt.Value.AddHours(1))
                                        {
                                            exceptions.Add(AttendanceExceptionType.ExtraWork);

                                            workdayHour = (shift.fullEndAt.Value - shift.fullStartAt.Value).TotalHours;
                                            extraHour = (lastD - shift.fullEndAt.Value).TotalHours;
                                        }
                                    }  
                                }
                            }

                        }

                    }
                }
                else
                {
                    // 如果没有排班怎么计算？
                    // 如果没有排班，应该都是加班
                    // TODO, 根据目前和德晋HR了解情况，会排班，所以这种情况暂时不考虑
                }
                /// 暂时对没有排班的不处理
                if (shifts.Count > 0)
                {
                   
                    // 基本工时都是 8 小时，不分部门和职称
                    if (workdayHour > 8)
                    {
                        workdayHour = 8;
                    }
                    
                    /// 如果是双休或节假日都算加班！无论是否有排班
                    if (isRestDay)
                    {
                        extraHour += workdayHour;
                        workdayHour = 0;
                    }
                    extraHour = Math.Round(extraHour, 1);
                    workdayHour = Math.Round(workdayHour, 1);

                    // 如果是成型课 或者 行政课的司机，则加班不减0.5h，其它的都减
                    if (extraHour > 0)
                    {
                        exceptions.Add(AttendanceExceptionType.ExtraWork);
                        if (  staff.IsMinusExtraWorkHour)
                        {
                            extraHour -= 0.5;
                            if (extraHour < 0)
                            {
                                extraHour = 0;
                            }
                        }
                        ExtraWorkRecord extraRecord = new ExtraWorkRecordService(this.DbString).FindByStaffNrAndDete(staff.nr, datetime.Date);
                        if (extraRecord == null)
                        {
                            exceptions.Add(AttendanceExceptionType.ExtraWorkNoRecord);
                        }
                        else
                        {
                            if (extraHour != extraRecord.duration)
                            {
                                exceptions.Add(AttendanceExceptionType.ExtraWorkHourNotMatch);
                            }

                            if (wr.dateType != extraRecord.ExtraWorkType.systemCode)
                            {
                                exceptions.Add(AttendanceExceptionType.ExtraWorkTypeNotMatch);
                            }

                        }
                    }

                    DataContext comitDC = new DataContext(this.DbString);
                    AttendanceRecordCal calRecord = comitDC.Context.GetTable<AttendanceRecordCal>().FirstOrDefault(s => s.staffNr.Equals(staff.nr) && s.attendanceDate.Equals(datetime.Date));

                    exceptions = exceptions.Distinct().ToList();
                    int? extraType = extraHour == 0 ? null : wr.dateType;
                    if (calRecord == null)
                    {
                        comitDC.Context.GetTable<AttendanceRecordCal>().InsertOnSubmit(new AttendanceRecordCal()
                        {
                            staffNr = staff.nr,
                            attendanceDate = datetime.Date,
                            oriWorkingHour = workdayHour,
                            actWorkingHour = workdayHour,
                            oriExtraWorkingHour = extraHour,
                            actExtraWorkingHour = extraHour,
                            extraworkType = extraType,
                            attendanceExceptions = exceptions.Distinct().ToList(),
                            createdAt = DateTime.Now
                        });
                    }
                    else
                    {
                        calRecord.extraworkType = extraType;
                        calRecord.oriWorkingHour = calRecord.actWorkingHour = workdayHour;
                        calRecord.oriExtraWorkingHour = calRecord.actExtraWorkingHour = extraHour;
                        calRecord.createdAt = DateTime.Now;
                        calRecord.attendanceExceptions = exceptions;
                    }
                    comitDC.Context.SubmitChanges();
                }
            }
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

        private void MarkRepeatRecordByMinute(List<AttendanceRecordDetail> records, float repeatFlag)
        {
            for (int i = 0; i < records.Count - 1; i++)
            {
                MarkRepeatRecordCompareByMinute(records[i], records.Where(s => s.isRepeatedData == false).Skip(i + 1).Take(records.Count - 1).ToList(), repeatFlag);
            }
        }

        private void MarkRepeatRecordCompareByMinute(AttendanceRecordDetail baseRecord, List<AttendanceRecordDetail> records, float repeatFlag)
        {
            foreach (var r in records)
            {
                r.isRepeatedData = Math.Abs((baseRecord.recordAt - r.recordAt).TotalMinutes) < repeatFlag;
            }
        }


    }
}
