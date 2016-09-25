using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Excel;
using BlueHrLib.Data.Enum;

namespace BlueHrLib.Service.Implement
{
    public class ExcelReportService : ServiceBase, IExcelReportService
    {
        /// <summary>
        /// 正常工作日小时
        /// </summary>
        public static double WORK_DAY_HOUR = 8;
        
        public ExcelReportService(string dbString) : base(dbString) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="searchModel"></param>
        /// <param name="maxExtraTotalHour"></param>
        /// <returns></returns>
        public List<WorkSumExcelModel> GetSumExcelModels(DateTime startDate, DateTime endDate, StaffSearchModel searchModel,double maxExtraTotalHour=36)
        {
            List<WorkSumExcelModel> records = new List<WorkSumExcelModel>();
            DataContext dc = new DataContext(this.DbString);

            IQueryable<StaffView> staffQ = dc.Context.GetTable<StaffView>();
            IQueryable<AttendanceRecordCalView> attQ = dc.Context.GetTable<AttendanceRecordCalView>();
            IQueryable<AbsenceRecordView> abQ = dc.Context.GetTable<AbsenceRecordView>();

            staffQ = staffQ.Where(s => (s.workStatus == (int)WorkStatus.OnWork)
              || (s.workStatus == (int)WorkStatus.OffWork && (s.resignAt == null || s.resignAt >= startDate)));
            attQ = attQ.Where(s => (s.workStatus == (int)WorkStatus.OnWork)
             || (s.workStatus == (int)WorkStatus.OffWork && (s.resignAt == null || s.resignAt >= startDate)));
            abQ = abQ.Where(s => (s.workStatus == (int)WorkStatus.OnWork)
                         || (s.workStatus == (int)WorkStatus.OffWork && (s.resignAt == null || s.resignAt >= startDate)));

            if (searchModel.companyId.HasValue)
            {
                staffQ = staffQ.Where(s => s.companyId==searchModel.companyId.Value);
                attQ = attQ.Where(s => s.companyId == searchModel.companyId.Value);
                abQ = abQ.Where(s => s.companyId == searchModel.companyId.Value);
            }

            if (searchModel.departmentId.HasValue)
            {
                staffQ = staffQ.Where(s => s.departmentId==searchModel.departmentId.Value);
                attQ = attQ.Where(s => s.departmentId == searchModel.departmentId.Value);
                abQ = abQ.Where(s => s.departmentId == searchModel.departmentId.Value);
            }

            if(searchModel.StaffNrs!=null && searchModel.StaffNrs.Count > 0)
            {
                staffQ = staffQ.Where(s =>   searchModel.StaffNrs.Contains(s.nr));
                attQ = attQ.Where(s => searchModel.StaffNrs.Contains(s.nr));
                abQ = abQ.Where(s => searchModel.StaffNrs.Contains(s.nr));
            }

            attQ = attQ.Where(s => s.attendanceDate >= startDate && s.attendanceDate <= endDate);
            abQ = abQ.Where(s => s.absenceDate >= startDate && s.absenceDate <= endDate);

            List<StaffView> staffs = staffQ.ToList();
            List<AttendanceRecordCalView> attends = attQ.ToList();
            List<AbsenceRecordView> abs = abQ.ToList();
            List<WorkAndRest> wrs = new WorkAndRestService(this.DbString).GetByDateSpan(startDate,endDate);

            int OriginAttendDays = wrs.Where(s => s.IsWorkDay).Count();
            double OriginAttendHours = OriginAttendDays * WORK_DAY_HOUR;

            // 初始化系统预定义缺勤类型
            List<AbsenceType> abTypes = new AbsenceTypeService(this.DbString).GetSystemTypes();
            Dictionary<SystemAbsenceType, int> abTypesDic = new Dictionary<SystemAbsenceType, int>();
            foreach(var abT in abTypes)
            {
                abTypesDic.Add((SystemAbsenceType)abT.systemCode.Value, abT.id);
            }

            foreach (var staff in staffs)
            {
                WorkSumExcelModel sumModel = new WorkSumExcelModel() { Staff = staff, Items = new List<WorkSumExcelItemModel>() };
                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    AttendanceRecordCalView at = attends.FirstOrDefault(s => s.staffNr.Equals(staff.nr) && s.attendanceDate == date);
                    AbsenceRecordView ab = abs.FirstOrDefault(s => s.staffNr.Equals(staff.nr) && s.absenceDate == date);
                    WorkAndRest wr = wrs.FirstOrDefault(s => s.dateAt == date);
                    sumModel.Items.Add(new WorkSumExcelItemModel() { Id = Guid.NewGuid().ToString(), Staff = staff, DateTime = date, WorkAndRest = wr, AttendanceRecordCal = at, AbsenceRecord = ab });
                }

                IEnumerable<AttendanceRecordCalView> baseATQ = attends.Where(s => s.staffNr.Equals(staff.nr));
                IEnumerable<AbsenceRecordView> baseABQ = abs.Where(s => s.staffNr.Equals(staff.nr));

                // 节假日总时间的统计
                #region 节假日总时间的统计

                // 放班- -
                double? HolidayWork = sumModel.Items.Where(s => s.AbsenceRecord!=null && s.AbsenceRecord.absenceTypeId == abTypesDic[SystemAbsenceType.HolidayWork]).Sum(s => s.AbsenceRecord.duration);
                sumModel.HolidayWork = HolidayWork.HasValue ? HolidayWork.Value : 0;

                double? HolidayAbsence = sumModel.Items.Where(s => s.AbsenceRecord != null && s.AbsenceRecord.absenceTypeId == abTypesDic[SystemAbsenceType.HolidayAbsence]).Sum(s => s.AbsenceRecord.duration);
                sumModel.HolidayAbsence = HolidayAbsence.HasValue ? HolidayAbsence.Value : 0;

                double? SickAbsence = sumModel.Items.Where(s => s.AbsenceRecord != null && s.AbsenceRecord.absenceTypeId == abTypesDic[SystemAbsenceType.SickAbsence]).Sum(s => s.AbsenceRecord.duration);
                sumModel.SickAbsence = SickAbsence.HasValue ? SickAbsence.Value : 0;

                double? MaternityAbsence = sumModel.Items.Where(s => s.AbsenceRecord != null && s.AbsenceRecord.absenceTypeId == abTypesDic[SystemAbsenceType.MaternityAbsence]).Sum(s => s.AbsenceRecord.duration);
                sumModel.MaternityAbsence = MaternityAbsence.HasValue ? MaternityAbsence.Value : 0;

                double? FuneralAbsence = sumModel.Items.Where(s => s.AbsenceRecord != null && s.AbsenceRecord.absenceTypeId == abTypesDic[SystemAbsenceType.FuneralAbsence]).Sum(s => s.AbsenceRecord.duration);
                sumModel.FuneralAbsence = FuneralAbsence.HasValue ? FuneralAbsence.Value : 0;

                double? TurnAbsence = sumModel.Items.Where(s => s.AbsenceRecord != null && s.AbsenceRecord.absenceTypeId == abTypesDic[SystemAbsenceType.TurnAbsence]).Sum(s => s.AbsenceRecord.duration);
                sumModel.TurnAbsence = TurnAbsence.HasValue ? TurnAbsence.Value : 0;


                double? PublicAbsence = sumModel.Items.Where(s => s.AbsenceRecord != null && s.AbsenceRecord.absenceTypeId == abTypesDic[SystemAbsenceType.PublicAbsence]).Sum(s => s.AbsenceRecord.duration);
                sumModel.PublicAbsence = PublicAbsence.HasValue ? PublicAbsence.Value : 0;

                double? YearAbsence = sumModel.Items.Where(s => s.AbsenceRecord != null && s.AbsenceRecord.absenceTypeId == abTypesDic[SystemAbsenceType.YearAbsence]).Sum(s => s.AbsenceRecord.duration);
                sumModel.YearAbsence = YearAbsence.HasValue ? YearAbsence.Value : 0;

                double? WorkAbsence = sumModel.Items.Where(s => s.AbsenceRecord != null && s.AbsenceRecord.absenceTypeId == abTypesDic[SystemAbsenceType.WorkAbsence]).Sum(s => s.AbsenceRecord.duration);
                sumModel.WorkAbsence = WorkAbsence.HasValue ? WorkAbsence.Value : 0;

                // 新进
                sumModel.NewAbsence = sumModel.Items.Where(s => s.IsNew == true && s.WorkAndRest.IsWorkDay == true).Count() * WORK_DAY_HOUR;
                // 离职
                sumModel.ResignAbsence = sumModel.Items.Where(s => s.IsResign == true && s.WorkAndRest.IsWorkDay == true).Count() * WORK_DAY_HOUR;
                #endregion

                // 出勤工时
                #region 出勤工时
                sumModel.AttendHour = OriginAttendHours - sumModel.Items.Where(s => s.AbsenceRecord != null  && abTypesDic.Where(ss=>ss.Key!=SystemAbsenceType.HolidayWork).Select(ss=>ss.Value).Contains(s.AbsenceRecord.absenceTypeId) ).Sum(s => s.AbsenceRecord.duration) - sumModel.NewAbsence - sumModel.ResignAbsence;
                #endregion

                // 原始记录的加班统计
                #region 原始记录的加班统计
                double? WorkExtraHour = baseATQ.Where(s => s.extraworkType == (int)SystemExtraType.WorkExtra).Sum(s => s.actExtraWorkingHour);
                sumModel.OriWorkExtraHour = WorkExtraHour.HasValue ? WorkExtraHour.Value : 0;


                double? WeekendExtraHour = baseATQ.Where(s => s.extraworkType == (int)SystemExtraType.WeekendExtra).Sum(s => s.actExtraWorkingHour);
                sumModel.OriWeekendExtraHour = WeekendExtraHour.HasValue ? WeekendExtraHour.Value : 0;


                double? HolidayExtraHour = baseATQ.Where(s => s.extraworkType == (int)SystemExtraType.HolidayExtra).Sum(s => s.actExtraWorkingHour);
                sumModel.OriHolidayExtraHour = HolidayExtraHour.HasValue ? HolidayExtraHour.Value : 0;
                #endregion

                // 计算统计！！！！
                #region
                // 计算减去放班的加班总计
                // 计算A->B，减去放班的统计 PlanA, 可以根据其他的总计，舍弃这个这个方式
                //#region 计算A->B，减去放班的统计
                //double workHoliday = sumModel.HolidayWork;
                //if (workHoliday >= 0)
                //{
                //    if (sumModel.OriHolidayExtraHour >= workHoliday)
                //    {
                //        sumModel.MinusedHolidayWorkHolidayLeftExtraHour = sumModel.OriHolidayExtraHour - workHoliday;
                //        workHoliday = 0;
                //    }
                //    else
                //    {
                //        sumModel.MinusedHolidayWorkHolidayLeftExtraHour = 0;
                //        workHoliday = workHoliday - sumModel.OriHolidayExtraHour;
                //    }
                //    if (workHoliday >= 0)
                //    {
                //        if (sumModel.OriWeekendExtraHour >= workHoliday)
                //        {
                //            sumModel.MinusedHolidayWorkWeekendLeftExtraHour = sumModel.OriWeekendExtraHour - workHoliday;
                //            workHoliday = 0;
                //        }
                //        else
                //        {
                //            sumModel.MinusedHolidayWorkWeekendLeftExtraHour = 0;
                //            workHoliday = workHoliday - sumModel.OriWeekendExtraHour;
                //        }

                //        if (workHoliday >= 0)
                //        {
                //            if (sumModel.OriWorkExtraHour >= workHoliday)
                //            {
                //                sumModel.MinusedHolidayWorkWorkLeftExtraHour = sumModel.OriWorkExtraHour - workHoliday;
                //                workHoliday = 0;
                //            }
                //            else
                //            {
                //                sumModel.MinusedHolidayWorkWorkLeftExtraHour = 0;
                //                workHoliday = workHoliday - sumModel.OriWorkExtraHour;
                //            }
                //        }
                //    }

                //}
                //#endregion

                // 计算 

                // 分别计算单个的 去掉放班的时间
                #region 分别计算单个的 去掉放班 的时间
                if (sumModel.HolidayWork>0)
                {
                    double factor = sumModel.HolidayWork;
                    
                    // 先将 Items 排序，
                    // 按 type 降序，即 节假日、双休、平时，
                    // 按 加班量 降序
                    var q = sumModel.Items.Where(s=>s.AttendanceRecordCal!=null)
                        .OrderByDescending(s=>s.DateTime)
                      //  .OrderByDescending(s => s.AttendanceRecordCal.actExtraWorkingHour)
                        .OrderByDescending(s => s.AttendanceRecordCal.extraworkType);
                    List<WorkSumExcelItemModel> sortedItems = q.ToList();
                    foreach(var item in sortedItems)
                    {
                       
                        if (factor <= 0)
                        {
                            sumModel.Items.FirstOrDefault(s => s.Id == item.Id).MinusedHolidayWorkHour = 0;
                        }
                        else
                        {
                            if (factor <= item.AttendanceRecordCal.actExtraWorkingHour)
                            {
                                double xfactor = item.AttendanceRecordCal.actExtraWorkingHour.Value - factor;
                                if (xfactor >= 4)
                                {
                                     
                                    sumModel.Items.FirstOrDefault(s => s.Id == item.Id).MinusedHolidayWorkHour = factor;
                                }
                                else
                                {
                                    sumModel.Items.FirstOrDefault(s => s.Id == item.Id).MinusedHolidayWorkHour = item.AttendanceRecordCal.actExtraWorkingHour.Value;
                                }
                                factor = 0;
                            }
                            else
                            {
                                sumModel.Items.FirstOrDefault(s => s.Id == item.Id).MinusedHolidayWorkHour = item.AttendanceRecordCal.actExtraWorkingHour.Value;
                                factor = factor - item.AttendanceRecordCal.actExtraWorkingHour.Value;
                            }
                        }
                    }
                }
                #endregion
                
                // 计算减去放班的加班总计
                // 计算A->B，减去放班的统计 PlanB
                #region 计算A->B，减去放班的统计
                double? MinusedHolidayWorkWorkLeftExtraHour = sumModel.Items.Where(s => s.AttendanceRecordCal != null && s.AttendanceRecordCal.extraworkType == (int)SystemExtraType.WorkExtra).Sum(s => s.MinuseHolidayWorkLeftExtraHour);
                sumModel.MinusedHolidayWorkWorkLeftExtraHour = MinusedHolidayWorkWorkLeftExtraHour.HasValue ? MinusedHolidayWorkWorkLeftExtraHour.Value : 0;


                double? MinusedHolidayWorkWeekendLeftExtraHour = sumModel.Items.Where(s => s.AttendanceRecordCal != null && s.AttendanceRecordCal.extraworkType == (int)SystemExtraType.WeekendExtra).Sum(s => s.MinuseHolidayWorkLeftExtraHour);
                sumModel.MinusedHolidayWorkWeekendLeftExtraHour = MinusedHolidayWorkWeekendLeftExtraHour.HasValue ? MinusedHolidayWorkWeekendLeftExtraHour.Value : 0;

                double? MinusedHolidayWorkHolidayLeftExtraHour = sumModel.Items.Where(s => s.AttendanceRecordCal != null && s.AttendanceRecordCal.extraworkType == (int)SystemExtraType.HolidayExtra).Sum(s => s.MinuseHolidayWorkLeftExtraHour);
                sumModel.MinusedHolidayWorkHolidayLeftExtraHour = MinusedHolidayWorkHolidayLeftExtraHour.HasValue ? MinusedHolidayWorkHolidayLeftExtraHour.Value : 0;

                #endregion


                // 分别计算单个的 去掉放班的时间
                #region 分别计算单个的 打于maxExtraTotalHour 的时间
                if (sumModel.MinusedHolidayLeftTotalExtraHour > maxExtraTotalHour)
                {
                    double factor = sumModel.MinusedHolidayLeftTotalExtraHour-maxExtraTotalHour;
                    // 先将 Items 排序，
                    // 按 type 降序，即 节假日、双休、平时，
                    // 按 加班量减掉放班的剩余量 降序
                    var q = sumModel.Items.Where(s => s.AttendanceRecordCal != null)
                        //.OrderBy(s => s.DateTime)
                        .OrderByDescending(s => s.MinuseHolidayWorkLeftExtraHour)
                        .OrderByDescending(s => s.AttendanceRecordCal.extraworkType);
                    List<WorkSumExcelItemModel> sortedItems = q.ToList();

                    foreach (var item in sortedItems)
                    {
                        if (factor <= 0)
                        {
                            sumModel.Items.FirstOrDefault(s => s.Id == item.Id).MinusedThresholdHour = 0;
                        }
                        else
                        {
                            if (factor <= item.MinuseHolidayWorkLeftExtraHour)
                            {
                                //sumModel.Items.FirstOrDefault(s => s.Id == item.Id).MinusedThresholdHour = factor;
                                //factor = 0;
                                double xfactor = item.MinuseHolidayWorkLeftExtraHour - factor;
                                if (xfactor >= 4 )
                                {
                                    sumModel.Items.FirstOrDefault(s => s.Id == item.Id).MinusedThresholdHour = factor;
                                }
                                else
                                {
                                    sumModel.Items.FirstOrDefault(s => s.Id == item.Id).MinusedThresholdHour = item.MinuseHolidayWorkLeftExtraHour;
                                }
                                factor = 0;
                            }
                            else
                            {
                                sumModel.Items.FirstOrDefault(s => s.Id == item.Id).MinusedThresholdHour = item.MinuseHolidayWorkLeftExtraHour;
                                factor = factor - item.MinuseHolidayWorkLeftExtraHour;
                            }
                        }
                    }
                }
                #endregion


                #region 统计加班
               


                // 计算B->C，减去多余加班的统计
                #region 计算B->C，减去多余加班的统计
                double? MinusedThresholdWorkLeftExtraHour = sumModel.Items.Where(s => s.AttendanceRecordCal != null && s.AttendanceRecordCal.extraworkType == (int)SystemExtraType.WorkExtra).Sum(s => s.MinuseHolidayWorkAndThresHoldLeftExtraHour);
                sumModel.MinusedThresholdWorkLeftExtraHour = MinusedThresholdWorkLeftExtraHour.HasValue ? MinusedThresholdWorkLeftExtraHour.Value : 0;


                double? MinusedThresholdWeekendLeftExtraHour = sumModel.Items.Where(s => s.AttendanceRecordCal != null && s.AttendanceRecordCal.extraworkType == (int)SystemExtraType.WeekendExtra).Sum(s => s.MinuseHolidayWorkAndThresHoldLeftExtraHour);
                sumModel.MinusedThresholdWeekendLeftExtraHour = MinusedThresholdWeekendLeftExtraHour.HasValue ? MinusedThresholdWeekendLeftExtraHour.Value : 0;

                double? MinusedThresholdHolidayLeftExtraHour = sumModel.Items.Where(s => s.AttendanceRecordCal != null && s.AttendanceRecordCal.extraworkType == (int)SystemExtraType.HolidayExtra).Sum(s => s.MinuseHolidayWorkAndThresHoldLeftExtraHour);
                sumModel.MinusedThresholdHolidayLeftExtraHour = MinusedThresholdHolidayLeftExtraHour.HasValue ? MinusedThresholdHolidayLeftExtraHour.Value : 0;

                #endregion


                // 计算C->D，被减去多余加班的统计
                #region 计算C->D，被减去多余加班的统计
                double? MinusedThresholdWorkExtraHour = sumModel.Items.Where(s => s.AttendanceRecordCal != null && s.AttendanceRecordCal.extraworkType == (int)SystemExtraType.WorkExtra).Sum(s => s.MinusedThresholdHour);
                sumModel.MinusedThresholdWorkExtraHour = MinusedThresholdWorkExtraHour.HasValue ? MinusedThresholdWorkExtraHour.Value : 0;


                double? MinusedThresholdWeekendExtraHour = sumModel.Items.Where(s => s.AttendanceRecordCal != null && s.AttendanceRecordCal.extraworkType == (int)SystemExtraType.WeekendExtra).Sum(s => s.MinusedThresholdHour);
                sumModel.MinusedThresholdWeekendExtraHour = MinusedThresholdWeekendExtraHour.HasValue ? MinusedThresholdWeekendExtraHour.Value : 0;

                double? MinusedThresholdHolidayExtraHour = sumModel.Items.Where(s => s.AttendanceRecordCal != null && s.AttendanceRecordCal.extraworkType == (int)SystemExtraType.HolidayExtra).Sum(s => s.MinusedThresholdHour);
                sumModel.MinusedThresholdHolidayExtraHour = MinusedThresholdHolidayExtraHour.HasValue ? MinusedThresholdHolidayExtraHour.Value : 0;

                #endregion


                #endregion

                #endregion

                records.Add(sumModel);

            }



            return records;
        }
    }
}
