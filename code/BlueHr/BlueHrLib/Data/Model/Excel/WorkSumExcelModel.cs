using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;
using System.ComponentModel;

namespace BlueHrLib.Data.Model.Excel
{
    public class WorkSumExcelModel
    {
        public StaffView Staff { get; set; }

        public List<WorkSumExcelItemModel> Items { get; set; }

        //=========== 原始 =========== 
        /// <summary>
        ///原始，延时加班总计
        /// </summary>
        public double OriWorkExtraHour { get; set; }

        /// <summary>
        ///原始，双休加班总计
        /// </summary>
        public double OriWeekendExtraHour { get; set; }

        /// <summary>
        /// 原始，节假日加班总计
        /// </summary>
        public double OriHolidayExtraHour { get; set; }
        
        /// <summary>
        /// 原始， 加班总计
        /// </summary>
        public double OriTotalExtraHour { get { return this.OriWorkExtraHour + this.OriWeekendExtraHour + this.OriHolidayExtraHour; } }

        //=========== 减去放班剩余的 =========== 
        /// <summary>
        ///减去放班的剩余的，延时加班总计，A-B=C, 这个就是C
        /// </summary>
        public double MinusedHolidayWorkWorkLeftExtraHour { get; set; }

        /// <summary>
        ///减去放班的剩余的，双休加班总计，A-B=C, 这个就是C
        /// </summary>
        public double MinusedHolidayWorkWeekendLeftExtraHour { get; set; }

        /// <summary>
        /// 减去放班的剩余的，节假日加班总计，A-B=C, 这个就是C
        /// </summary>
        public double MinusedHolidayWorkHolidayLeftExtraHour { get; set; }


        /// <summary>
        /// 减去放班的剩余的， 加班总计
        /// </summary>
        public double MinusedHolidayLeftTotalExtraHour { get { return this.MinusedHolidayWorkWorkLeftExtraHour + this.MinusedHolidayWorkWeekendLeftExtraHour + this.MinusedHolidayWorkHolidayLeftExtraHour; } }


        //=========== 减去因子(默认36)剩余的 =========== 
        /// <summary>
        ///减去因子(默认36)剩余的，延时加班总计，A-B=C, 这个就是C
        /// </summary>
        public double MinusedThresholdWorkLeftExtraHour { get; set; }

        /// <summary>
        ///减去因子(默认36)剩余的，双休加班总计，A-B=C, 这个就是C
        /// </summary>
        public double MinusedThresholdWeekendLeftExtraHour { get; set; }

        /// <summary>
        /// 减去因子(默认36)剩余的，节假日加班总计，A-B=C, 这个就是C
        /// </summary>
        public double MinusedThresholdHolidayLeftExtraHour { get; set; }


        /// <summary>
        /// 减去因子(默认36)剩余的， 加班总计
        /// </summary>
        public double MinusedThresholdLeftTotalExtraHour { get { return this.MinusedThresholdWorkLeftExtraHour + this.MinusedThresholdWeekendLeftExtraHour + this.MinusedThresholdHolidayLeftExtraHour; } }



        //=========== 减去因子(默认36)的值 =========== 
        /// <summary>
        ///减去因子(默认36)的值，延时加班总计，A-B=C, 这个就是B
        /// </summary>
        public double MinusedThresholdWorkExtraHour { get; set; }

        /// <summary>
        ///减去因子(默认36)的值，双休加班总计，A-B=C, 这个就是B
        /// </summary>
        public double MinusedThresholdWeekendExtraHour { get; set; }

        /// <summary>
        /// 减去因子(默认36)的值，节假日加班总计，A-B=C, 这个就是B
        /// </summary>
        public double MinusedThresholdHolidayExtraHour { get; set; }


        /// <summary>
        ///减去因子(默认36)的值， 加班总计
        /// </summary>
        public double MinusedThresholdTotalExtraHour { get { return this.MinusedThresholdWorkExtraHour + this.MinusedThresholdWeekendExtraHour + this.MinusedThresholdHolidayExtraHour; } }


        /// <summary>
        /// 白班，暂时不用
        /// </summary>
        public double DayHour { get; set; }
        /// <summary>
        /// 夜班，暂时不用
        /// </summary>
        public double NightHour { get; set; }

        /// <summary>
        /// 轮班费班，暂时不用
        /// </summary>
        public double TurnWorkSalary { get; set; }

        /// <summary>
        /// 出勤工时
        /// </summary>
        public double AttendHour { get; set; }


        /// <summary>
        /// 放班
        /// </summary>
        public double HolidayWork { get; set; }


        /// <summary>
        /// 事假
        /// </summary>
        public double HolidayAbsence { get; set; }

        /// <summary>
        /// 病假
        /// </summary>
        public double SickAbsence { get; set; }

        /// <summary>
        /// 产假
        /// </summary>
        public double MaternityAbsence { get; set; }

        /// <summary>
        /// 丧假
        /// </summary>
        public double FuneralAbsence { get; set; }

        /// <summary>
        /// 轮休
        /// </summary>
        public double TurnAbsence { get; set; }

        /// <summary>
        /// 公休
        /// </summary>
        public double PublicAbsence { get; set; }

        /// <summary>
        /// 年休
        /// </summary>
        public double YearAbsence { get; set; }

        /// <summary>
        /// 新进，暂时不用,可能不准确，需要人员确认
        /// </summary>
        public double NewAbsence { get; set; }

        /// <summary>
        /// 旷工，暂时不用
        /// </summary>
        public double WorkAbsence { get; set; }

        /// <summary>
        /// 离职，暂时不用,可能不准确，需要人员确认
        /// </summary>
        public double ResignAbsence { get; set; }


        /// <summary>
        /// 延时费，暂时不用
        /// </summary>
        public double WorkExtraSalary { get; set; }

        /// <summary>
        /// 双休费，暂时不用
        /// </summary>
        public double WeekendExtraSalary { get; set; }

        /// <summary>
        /// 节日费，暂时不用
        /// </summary>
        public double HolidayExtraSalary { get; set; }

        /// <summary>
        /// 加班费合计，暂时不用
        /// </summary>
        public double TotalExtraSalary { get; set; }

        /// <summary>
        /// 独生子女费，暂时不用
        /// </summary>
        public double ChildSalary { get; set; }
        /// <summary>
        /// 病假扣款，暂时不用
        /// </summary>
        public double SickMinSalary { get; set; }

        /// <summary>
        /// 工龄，暂时不用
        /// </summary>
        public int WorkYear { get; set; }
        /// <summary>
        /// 事假扣款，暂时不用
        /// </summary>
        public double HolidayMinSalary { get; set; }

        /// <summary>
        /// 漏打卡，暂时不用
        /// </summary>
        public double LackAttRecord { get; set; }

        /// <summary>
        /// 迟到早退，暂时不用
        /// </summary>
        public double LateOrEarlyLeave { get; set; }

        /// <summary>
        /// 用工，暂时不用
        /// </summary>
        public string StaffContractType { get; set; }



        //=========== 显示，根据不同的报表类型 =========== 
        /// <summary>
        /// 延时加班总计，显示
        /// </summary>
        public double WorkExtraHourDisplay(WorkSumExcelType type) {
            switch (type)
            {
                case WorkSumExcelType.Orign:
                    return this.OriWorkExtraHour;
                case WorkSumExcelType.MinusHolidayWork:
                    return this.MinusedHolidayWorkWorkLeftExtraHour; 
                case WorkSumExcelType.MinusThreasholdWork:
                    return this.MinusedThresholdWorkLeftExtraHour;
                case WorkSumExcelType.JobAward:
                    return this.MinusedThresholdWorkExtraHour;
                default:
                    break;
            }
            return 0;
        }

        /// <summary>
        /// 双休加班总计
        /// </summary>
        public double WeekendExtraHourDisplay(WorkSumExcelType type)
        {
            switch (type)
            {
                case WorkSumExcelType.Orign:
                    return this.OriWeekendExtraHour;
                case WorkSumExcelType.MinusHolidayWork:
                    return this.MinusedHolidayWorkWeekendLeftExtraHour;
                case WorkSumExcelType.MinusThreasholdWork:
                    return this.MinusedThresholdWeekendLeftExtraHour;
                case WorkSumExcelType.JobAward:
                    return this.MinusedThresholdWeekendExtraHour;
                default:
                    break;
            }
            return 0;
        }

        /// <summary>
        /// 节假日加班总计
        /// </summary>
        public double HolidayExtraHourDisplay(WorkSumExcelType type)
        {
            switch (type)
            {
                case WorkSumExcelType.Orign:
                    return this.OriHolidayExtraHour;
                case WorkSumExcelType.MinusHolidayWork:
                    return this.MinusedHolidayWorkHolidayLeftExtraHour;
                case WorkSumExcelType.MinusThreasholdWork:
                    return this.MinusedThresholdHolidayLeftExtraHour;
                case WorkSumExcelType.JobAward:
                    return this.MinusedThresholdHolidayExtraHour;
                default:
                    break;
            }
            return 0;
        }


        /// <summary>
        /// 加班总计
        /// </summary>
        public double TotalExtraHourDisplay(WorkSumExcelType type)
        {
            switch (type)
            {
                case WorkSumExcelType.Orign:
                    return this.OriTotalExtraHour;
                case WorkSumExcelType.MinusHolidayWork:
                    return this.MinusedHolidayLeftTotalExtraHour;
                case WorkSumExcelType.MinusThreasholdWork:
                    return this.MinusedThresholdLeftTotalExtraHour;
                case WorkSumExcelType.JobAward:
                    return this.MinusedThresholdTotalExtraHour;
                default:
                    break;
            }
            return 0;
        }
    }

    public class WorkSumExcelItemModel
    {

        private AttendanceRecordCalView attendRecord;
        //  private double minusedHolidayWorkHour;
        //  private double minusedThresholdHour;
        public WorkSumExcelItemModel()
        {
            this.MinusedHolidayWorkHour = 0;

            this.MinusedThresholdHour = 0;
        }

        public string Id { get; set; }

        public StaffView Staff { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 作息表
        /// </summary>
        public WorkAndRest WorkAndRest { get; set; }

        /// <summary>
        /// 考勤统计记录
        /// </summary>
        public AttendanceRecordCalView AttendanceRecordCal
        {
            get { return attendRecord; }
            set
            {
                this.attendRecord = value;
            }
        }

        /// <summary>
        /// 是否被减放班小时
        /// </summary>
        public bool IsMinusedHolidayWorkHour { get { return this.MinusedHolidayWorkHour > 0; } }

        /// <summary>
        /// 被减掉的放班小时数
        /// </summary>
        public double MinusedHolidayWorkHour { get; set; }

        /// <summary>
        /// 是否被减阀值小时
        /// </summary>
        public bool IsMinusedThresholdHour { get { return this.MinusedThresholdHour > 0; } }

        /// <summary>
        /// 被减掉的阀值小时数
        /// </summary>
        public double MinusedThresholdHour { get; set; }

        /// <summary>
        /// 是否 新进
        /// </summary>
        public bool IsNew
        {
            get
            {
                return this.Staff.companyEmployAt.HasValue && this.Staff.companyEmployAt.Value > this.DateTime;
            }
        }

        /// <summary>
        /// 是否 离职
        /// </summary>
        public bool IsResign
        {
            get
            {
                return this.Staff.workStatus == (int)WorkStatus.OffWork && this.Staff.resignAt.HasValue && this.Staff.resignAt <= this.DateTime;
            }
        }

        /// <summary>
        /// 减掉放班剩下的加班小时数
        /// </summary>
        public double MinuseHolidayWorkLeftExtraHour
        {
            get
            {
                if (this.IsHasExtraWork)
                {
                    return this.AttendanceRecordCal.actExtraWorkingHour.Value - this.MinusedHolidayWorkHour;
                }
                else
                {
                    return 0;
                }
            }
        }


        /// <summary>
        /// 减掉放班+阀值剩下的加班小时数
        /// </summary>
        public double MinuseHolidayWorkAndThresHoldLeftExtraHour
        {
            get
            {
                if (this.IsHasExtraWork)
                {
                    return this.AttendanceRecordCal.actExtraWorkingHour.Value - this.MinusedHolidayWorkHour - this.MinusedThresholdHour;
                }
                else
                {
                    return 0;
                }
            }
        }



        /// <summary>
        /// 缺勤记录
        /// </summary>
        public AbsenceRecordView AbsenceRecord { get; set; }

        public bool IsHasExtraWork
        {
            get
            {
                return this.AttendanceRecordCal != null && this.AttendanceRecordCal.actExtraWorkingHour.HasValue && this.AttendanceRecordCal.actExtraWorkingHour.Value > 0;
            }
        }

        public string DisplayValue(WorkSumExcelType type)
        {

            if (this.IsHasExtraWork)
            {
                double value = 0;
                //return Math.Round(this.AttendanceRecordCal.actExtraWorkingHour.Value, 1).ToString();
                switch (type)
                {
                    case WorkSumExcelType.Orign:
                        value = Math.Round(this.AttendanceRecordCal.actExtraWorkingHour.Value, 1);
                        break;
                    case WorkSumExcelType.MinusHolidayWork:
                        value = Math.Round(this.MinuseHolidayWorkLeftExtraHour, 1);
                       //value = Math.Round(this.AttendanceRecordCal.actExtraWorkingHour.Value, 1);
                        break;
                    case WorkSumExcelType.MinusThreasholdWork:
                        value = Math.Round(this.MinuseHolidayWorkAndThresHoldLeftExtraHour, 1);
                        break;
                    case WorkSumExcelType.JobAward:
                        value = this.MinusedThresholdHour;
                        break;
                    default:
                        break;
                }
                return value <= 0 ? string.Empty : value.ToString();
            }
            else if (this.AbsenceRecord != null && this.AbsenceRecord.duration > 0)
            {
                return string.Format("{0}{1}", this.AbsenceRecord.code, this.AbsenceRecord.duration);
            }
            else if (IsResign)
            {
                return "离职";
            }
            else if (IsNew)
            {
                return "新进";
            }
            return string.Empty;
        }
    }

    public enum WorkSumExcelType
    {
        /// <summary>
        /// 原始表(A)
        /// </summary>
        [Description("原始表(A)")]
        Orign = 100,

        /// <summary>
        /// 减掉放班数据表(B)
        /// </summary>
        [Description("减掉放班数据表(B)")]
        MinusHolidayWork = 200,

        /// <summary>
        /// 减掉多余加班数据表(C)
        /// </summary>
        [Description("减掉多余加班数据表(C)")]
        MinusThreasholdWork = 300,

        /// <summary>
        /// 岗位奖金表(D)
        /// </summary>
        [Description("岗位奖金表(D)")]
        JobAward = 400

    }
}
