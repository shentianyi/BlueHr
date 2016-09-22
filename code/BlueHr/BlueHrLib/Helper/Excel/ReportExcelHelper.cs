using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Excel;
using BlueHrLib.Data.Enum;
using OfficeOpenXml;

namespace BlueHrLib.Helper.Excel
{
    public class ReportExcelHelper : ExcelHelperBase
    {
        public ReportExcelHelper() { }

        public ReportExcelHelper(string dbString) : base(dbString)
        {
            this.DbString = dbString;
        }

        public object Color { get; private set; }

        public ReportMessage ExportExtraSumReport(DateTime startDate, DateTime endDate, StaffSearchModel searchModel)
        {

            ReportMessage msg = new ReportMessage();
            string fileName = "考勤加班原始总表.xlsx";
            //try
            //{
                IStaffService ss = new StaffService(this.DbString);


                string tmpFile = FileHelper.CreateFullTmpFilePath(fileName, false);
                msg.Content = FileHelper.GetDownloadTmpFilePath(tmpFile);

                List<WorkSumExcelModel> records = new ExcelReportService(this.DbString).GetSumExcelModels(startDate, endDate, searchModel);
                List<WorkSumExcelType> reportTypes = new List<WorkSumExcelType>() { WorkSumExcelType.Orign, WorkSumExcelType.MinusHolidayWork, WorkSumExcelType.MinusThreasholdWork, WorkSumExcelType.JobAward };
                FileInfo fileInfo = new FileInfo(tmpFile);
                List<WorkAndRest> wrs = new WorkAndRestService(this.DbString).GetByDateSpan(startDate, endDate);

                int OriginAttendDays = wrs.Where(s => s.IsWorkDay).Count();

            using (ExcelPackage ep = new ExcelPackage(fileInfo))
            {
                foreach (var type in reportTypes)
                {
                    ExcelWorksheet sheet = ep.Workbook.Worksheets.Add(EnumHelper.GetDescription(type));
                    List<int> holidaysColoms = new List<int>();


                    // 写Title
                    sheet.Cells[1, 1].Value = startDate.Year;
                    sheet.Cells[1, 2].Value = startDate.Month;
                    sheet.Cells[1, 4].Value = startDate.ToString("yyyy-M-d");
                    sheet.Cells[1, 5].Value = endDate.ToString("yyyy-M-d");
                    sheet.Cells[1, 6].Value = "法定节假日";

                    sheet.Cells[2, 1].Value = "年";
                    sheet.Cells[2, 2].Value = "月";
                    sheet.Cells[2, 4].Value = "出勤天数";
                    sheet.Cells[2, 5].Value = OriginAttendDays;
                    sheet.Cells[2, 6].Value = "休息天数";

                    sheet.Cells[3, 1].Value = "工号";
                    sheet.Cells[3, 2].Value = "姓名";
                    sheet.Cells[3, 3].Value = "所属公司";
                    sheet.Cells[3, 4].Value = "部门";
                    sheet.Cells[3, 5].Value = "单位";
                    sheet.Cells[3, 6].Value = "职务";
                    sheet.Cells[3, 7].Value = "人员类别";
                    int i = 8; 
                    for (var s = startDate; s <= endDate; s = s.AddDays(1))
                    {
                        bool isWorkday = wrs.Where(q => q.dateAt == s).FirstOrDefault().IsWorkDay;
                        if (!isWorkday)
                        {
                            holidaysColoms.Add(i);
                        }
                        sheet.Cells[1, i].Value = isWorkday ? "N" : "S";
                        sheet.Cells[2, i].Value = DateTimeHelper.GetShortDayOfWeek(s);
                        sheet.Cells[3, i].Value = s.Day;
                        i += 1;
                    }

                    // 写表A
                    foreach (var v in sheet.Cells)
                    {
                        v.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        v.AutoFitColumns();
                        v.Style.ShrinkToFit = true;
                    }

                    for (int r = 1; r <= sheet.Dimension.End.Row; r++)
                    {
                        // 设置节假日的背景
                        for (int c = 1; c <= sheet.Dimension.End.Column; c++)
                        {
                            if (holidaysColoms.Contains(c))
                            {
                                sheet.Cells[r, c].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                                sheet.Cells[r, c].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Orange);

                            }
                        }
                    }
                }
                /// 保存
                ep.Save();
            }


                msg.Success = true;
            //}
            //catch (Exception ex)
            //{
            //    msg.Content = ex.Message;
            //}

            return msg;
        }
        
    }
}
