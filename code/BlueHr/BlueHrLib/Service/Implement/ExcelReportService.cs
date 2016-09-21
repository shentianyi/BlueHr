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
        public ExcelReportService(string dbString) : base(dbString) { }

        public List<WorkSumExcelModel> GetSumExcelModels(DateTime startDate, DateTime endDate, StaffSearchModel searchModel)
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

            attQ = attQ.Where(s => s.attendanceDate >= startDate && s.attendanceDate <= endDate);
            abQ = abQ.Where(s => s.absenceDate >= startDate && s.absenceDate <= endDate);

            List<StaffView> staffs = staffQ.ToList();
            List<AttendanceRecordCalView> attends = attQ.ToList();
            List<AbsenceRecordView> abs = abQ.ToList();

            foreach (var staff in staffs)
            {
                WorkSumExcelModel sumModel = new WorkSumExcelModel() { Staff = staff, Items = new List<WorkSumExcelItemModel>() };
                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    AttendanceRecordCalView at = attends.Where(s => s.staffNr.Equals(staff.nr) && s.attendanceDate == date).FirstOrDefault();
                    AbsenceRecordView ab = abs.Where(s => s.staffNr.Equals(staff.nr) && s.absenceDate == date).FirstOrDefault();

                    sumModel.Items.Add(new WorkSumExcelItemModel() { Staff = staff, DateTime = date, AttendanceRecordCal = at, AbsenceRecord = ab });
                }

                IEnumerable<AttendanceRecordCalView> baseATQ = attends.Where(s => s.staffNr.Equals(staff.nr));
                IEnumerable<AbsenceRecordView> baseABQ = abs.Where(s => s.staffNr.Equals(staff.nr));

                double? WorkExtraHour = baseATQ.Where(s => s.extraworkType == (int)SystemExtraType.WorkExtra).Sum(s => s.actExtraWorkingHour);
                sumModel.WorkExtraHour = WorkExtraHour.HasValue ? WorkExtraHour.Value : 0;


                double? WeekendExtraHour = baseATQ.Where(s => s.extraworkType == (int)SystemExtraType.WeekendExtra).Sum(s => s.actExtraWorkingHour);
                sumModel.WeekendExtraHour = WeekendExtraHour.HasValue ? WeekendExtraHour.Value : 0;


                double? HolidayExtraHour = baseATQ.Where(s => s.extraworkType == (int)SystemExtraType.HolidayExtra).Sum(s => s.actExtraWorkingHour);
                sumModel.HolidayExtraHour = HolidayExtraHour.HasValue ? HolidayExtraHour.Value : 0;


            }





            throw new NotImplementedException();
        }
    }
}
