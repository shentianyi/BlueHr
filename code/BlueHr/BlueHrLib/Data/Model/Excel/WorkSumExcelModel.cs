using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;

namespace BlueHrLib.Data.Model.Excel
{
    public class WorkSumExcelModel
    {
        public StaffView Staff { get; set; }

        public List<WorkSumExcelItemModel> Items { get; set; }

        /// <summary>
        /// 延时加班总计
        /// </summary>
        public double WorkExtraHour { get; set; }

        /// <summary>
        /// 双休加班总计
        /// </summary>
        public double WeekendExtraHour { get; set; }

        /// <summary>
        /// 节假日加班总计
        /// </summary>
        public double HolidayExtraHour { get; set; }

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
        /// 新进，暂时不用
        /// </summary>
        public double NewAbsence { get; set; }

        /// <summary>
        /// 旷工，暂时不用
        /// </summary>
        public double WorkAbsence { get; set; }

        /// <summary>
        /// 离职，暂时不用
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
    }

    public class WorkSumExcelItemModel
    {
        public StaffView Staff { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 考勤统计记录
        /// </summary>
        public AttendanceRecordCalView AttendanceRecordCal { get; set; }

        /// <summary>
        /// 缺勤记录
        /// </summary>
        public AbsenceRecordView AbsenceRecord { get; set; }


        public string DisplayValue
        {
            get
            {
                if (this.AttendanceRecordCal != null && this.AttendanceRecordCal.actExtraWorkingHour.HasValue && this.AttendanceRecordCal.actExtraWorkingHour.Value > 0)
                {
                    return Math.Round(this.AttendanceRecordCal.actExtraWorkingHour.Value, 1).ToString();
                }
                else if (this.AbsenceRecord != null && this.AbsenceRecord.duration > 0)
                {
                    return string.Format("{0}{1}", this.AbsenceRecord.code, this.AbsenceRecord.duration);
                }
                else if(this.Staff.workStatus==(int)WorkStatus.OffWork && this.Staff.resignAt.HasValue && this.Staff.resignAt<=this.DateTime)
                {
                    return "离职";
                }
                return string.Empty;
            }
        }
    }
}
