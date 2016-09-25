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
using Brilliantech.Framwork.Utils.LogUtil;

namespace BlueHrLib.Helper.Excel
{
    public class ReportExcelHelper : ExcelHelperBase
    {
        public ReportExcelHelper() { }

        public ReportExcelHelper(string dbString) : base(dbString)
        {
            this.DbString = dbString;
        }


        /// <summary>
        /// 导出汇总表ABCD表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public ReportMessage ExportExtraSumReport(DateTime startDate, DateTime endDate, StaffSearchModel searchModel)
        {

            ReportMessage msg = new ReportMessage();
            string fileName = "考勤加班汇总表.xlsx";
            try
            {
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
                        #region 写Title

                        // 基本信息title
                        #region 基本信息Title
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
                        #endregion

                        //  日期title
                        #region 日期title
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
                        #endregion


                        //   统计部分的title
                        #region   统计部分的title
                        sheet.Cells[3, i].Value = "延时加班工时";
                        sheet.Cells[3, i + 1].Value = "双休加班工时";
                        sheet.Cells[3, i + 2].Value = "假日加班工时";
                        sheet.Cells[3, i + 3].Value = "加班合计";
                        sheet.Cells[3, i + 4].Value = "白班";
                        sheet.Cells[3, i + 5].Value = "夜班";
                        sheet.Cells[3, i + 6].Value = "轮班费";
                        sheet.Cells[3, i + 7].Value = "出勤工时";
                        sheet.Cells[3, i + 8].Value = "事假";
                        sheet.Cells[3, i + 9].Value = "病假";
                        sheet.Cells[3, i + 10].Value = "产假";
                        sheet.Cells[3, i + 11].Value = "丧假";
                        sheet.Cells[3, i + 12].Value = "轮休";
                        sheet.Cells[3, i + 13].Value = "公假";
                        sheet.Cells[3, i + 14].Value = "年休";
                        sheet.Cells[3, i + 15].Value = "新进";
                        sheet.Cells[3, i + 16].Value = "旷工";
                        sheet.Cells[3, i + 17].Value = "离职";
                        sheet.Cells[3, i + 18].Value = "放班";
                        sheet.Cells[3, i + 19].Value = "延时费";
                        sheet.Cells[3, i + 20].Value = "双休费";
                        sheet.Cells[3, i + 21].Value = "节日费";
                        sheet.Cells[3, i + 22].Value = "加班费合计";
                        sheet.Cells[3, i + 23].Value = "独生子女费";
                        sheet.Cells[3, i + 24].Value = "病假扣款";
                        sheet.Cells[3, i + 25].Value = "工龄";
                        sheet.Cells[3, i + 26].Value = "事假扣款";
                        sheet.Cells[3, i + 27].Value = "漏打卡";
                        sheet.Cells[3, i + 28].Value = "迟到早退";
                        sheet.Cells[3, i + 29].Value = "用工";
                        sheet.Cells[3, i + 30].Value = "备注";
                        #endregion

                        #endregion

                        /// 写表内容
                        #region 写表内容
                        for (int j = 0; j < records.Count; j++)
                        {
                            WorkSumExcelModel record = records[j];
                            // 写 员工基本信息
                            #region 写 员工基本信息
                            sheet.Cells[4 + j, 1].Value = record.Staff.nr;
                            sheet.Cells[4 + j, 2].Value = record.Staff.name;
                            sheet.Cells[4 + j, 3].Value = record.Staff.companyName;
                            sheet.Cells[4 + j, 4].Value = record.Staff.parenDepartName;
                            sheet.Cells[4 + j, 5].Value = record.Staff.departmentName;
                            sheet.Cells[4 + j, 6].Value = record.Staff.jobTitleName;
                            sheet.Cells[4 + j, 7].Value = record.Staff.staffTypeName;
                            #endregion

                            // 写 日期数据
                            #region 写 日期数据
                            for (int jj = 0; jj < record.Items.Count; jj++)
                            {
                                sheet.Cells[4 + j, 8 + jj].Value = record.Items[jj].DisplayValue(type);
                            }
                            #endregion

                            /// 写汇总信息
                            #region 写汇总信息

                            // 加班汇总
                            sheet.Cells[4 + j, i].Value = record.WorkExtraHourDisplay(type);
                            sheet.Cells[4 + j, i + 1].Value = record.WeekendExtraHourDisplay(type);
                            sheet.Cells[4 + j, i + 2].Value = record.HolidayExtraHourDisplay(type);
                            sheet.Cells[4 + j, i + 3].Value = record.TotalExtraHourDisplay(type);


                            // 白班、夜班、轮班费
                            sheet.Cells[4 + j, i + 4].Value = string.Empty;
                            sheet.Cells[4 + j, i + 5].Value = string.Empty;
                            sheet.Cells[4 + j, i + 6].Value = string.Empty;
                            // 出勤工时
                            sheet.Cells[4 + j, i + 7].Value = record.AttendHour;

                            // 基本假期
                            sheet.Cells[4 + j, i + 8].Value = record.HolidayAbsence;
                            sheet.Cells[4 + j, i + 9].Value = record.SickAbsence;
                            sheet.Cells[4 + j, i + 10].Value = record.MaternityAbsence;
                            sheet.Cells[4 + j, i + 11].Value = record.FuneralAbsence;
                            sheet.Cells[4 + j, i + 12].Value = record.TurnAbsence;
                            sheet.Cells[4 + j, i + 13].Value = record.PublicAbsence;
                            sheet.Cells[4 + j, i + 14].Value = record.YearAbsence;

                            // 新进、旷工、离职、放班
                            sheet.Cells[4 + j, i + 15].Value = 0 - record.NewAbsence;
                            sheet.Cells[4 + j, i + 16].Value = string.Empty; //record.WorkAbsence;
                            sheet.Cells[4 + j, i + 17].Value = 0 - record.ResignAbsence;
                            sheet.Cells[4 + j, i + 18].Value = record.HolidayWork;

                            // 费用
                            sheet.Cells[4 + j, i + 19].Value = string.Empty; //record.WorkExtraSalary;
                            sheet.Cells[4 + j, i + 20].Value = string.Empty; //record.WeekendExtraSalary;
                            sheet.Cells[4 + j, i + 21].Value = string.Empty; //record.HolidayExtraSalary;
                            sheet.Cells[4 + j, i + 22].Value = string.Empty; //record.TotalExtraSalary;

                            sheet.Cells[4 + j, i + 23].Value = string.Empty;//record.ChildSalary;
                            sheet.Cells[4 + j, i + 24].Value = string.Empty;//record.SickMinSalary;

                            // 工龄
                            sheet.Cells[4 + j, i + 25].Value = string.Empty;

                            // 其它 事假扣款	漏打卡	迟到早退	用工	备注
                            sheet.Cells[4 + j, i + 26].Value = string.Empty; //record.HolidayMinSalary;
                            sheet.Cells[4 + j, i + 27].Value = string.Empty;
                            sheet.Cells[4 + j, i + 28].Value = string.Empty;
                            sheet.Cells[4 + j, i + 29].Value = string.Empty;
                            sheet.Cells[4 + j, i + 30].Value = "1";

                            #endregion
                        }
                        #endregion


                        //设置格式
                        #region 设置格式
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
                        #endregion
                    }
                    /// 保存
                    ep.Save();
                }


                msg.Success = true;
            }
            catch (Exception ex)
            {
                LogUtil.Logger.Error(ex.Message, ex);

                msg.Content = ex.Message;
            }

            return msg;
        }

        /// <summary>
        /// 导出处理过的考勤记录
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public ReportMessage ExportHandledAttendDetail(DateTime startDate, DateTime endDate, StaffSearchModel searchModel)
        {
            ReportMessage msg = new ReportMessage();
            string fileName = "考勤记录表.xlsx";
            try
            {
                string tmpFile = FileHelper.CreateFullTmpFilePath(fileName, false);
                msg.Content = FileHelper.GetDownloadTmpFilePath(tmpFile);
                FileInfo fileInfo = new FileInfo(tmpFile);

                List<WorkSumExcelModel> records = new ExcelReportService(this.DbString).GetSumExcelModels(startDate, endDate, searchModel);

                List<WorkAndRest> wrs = new WorkAndRestService(this.DbString).GetByDateSpan(startDate, endDate);

                int OriginAttendDays = wrs.Where(s => s.IsWorkDay).Count();

                List<string[]> excelRecords = new List<string[]>();

                foreach (var r in records)
                {
                    // 每个Items代表一天
                    foreach (var rr in r.Items)
                    {
                        List<AttendanceRecordDetailView> atts = new List<AttendanceRecordDetailView>();

                        // 是否被减过加班时间
                        if (rr.IsMinusedHolidayWorkHour || rr.IsMinusedThresholdHour)
                        {
                            if (rr.MinuseHolidayWorkAndThresHoldLeftExtraHour == 0)
                            {
                                if (rr.WorkAndRest.IsWorkDay)
                                {
                                    // 看看是不是有排班，根据排班的情况造数据，开始结束有25-45分钟的冗余，
                                    // 就是下班到打卡设备的时间
                                    IShiftScheduleService ss = new ShiftSheduleService(this.DbString);
                                    var shift = ss.GetFirstShiftByStaffAndDate(rr.Staff.nr, rr.DateTime);
                                    if (shift == null)
                                    {
                                        //   break;
                                    }
                                    else
                                    {
                                        DateTime startTime = shift.fullStartAt.Value.AddMinutes(0-new Random().Next(25,45));
                                        DateTime endTime = shift.fullEndAt.Value.AddHours(rr.MinuseHolidayWorkAndThresHoldLeftExtraHour).AddMinutes(new Random().Next(25, 45));

                                        excelRecords.Add(new string[6] { rr.Staff.departmentName, rr.Staff.nr, rr.Staff.name, startTime.ToString("yyyy-MM-dd"), startTime.ToString("HH:mm"), "001" });

                                        excelRecords.Add(new string[6] { rr.Staff.departmentName, rr.Staff.nr, rr.Staff.name, endTime.ToString("yyyy-MM-dd"), endTime.ToString("HH:mm"), "001" });
                                    }
                                }
                                else
                                {
                                    // 直接跳过这一天
                                    // break;
                                }
                            }
                        }
                        else
                        {
                            // 如果没有就使用原始的出勤时间
                            IAttendanceRecordDetailService s = new AttendanceRecordDetailService(this.DbString);
                            atts = s.GetDetailsViewByStaffAndDateWithExtrawork(rr.Staff.nr, rr.DateTime);
                            foreach(var a in atts)
                            {
                                excelRecords.Add(new string[6] { rr.Staff.departmentName, rr.Staff.nr, rr.Staff.name, a.recordAtDateStr, a.recordAtTimeStr, a.device });
                            }
                        }

                    }
                }
                using (ExcelPackage ep = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet sheet = ep.Workbook.Worksheets.Add("Sheet1");
                    string[] titles = new string[6] { "机构名称", "人员编号", "姓名", "刷卡日期", "刷卡时间", "设备编号" };
                    for (var i = 0; i < titles.Length; i++)
                    {
                        sheet.Cells[1, i + 1].Value = titles[i];
                    }
                    int row = 2;
                    foreach(var er in excelRecords)
                    {
                        for (var i = 0; i < titles.Length; i++)
                        {
                            sheet.Cells[row, i + 1].Value = er[i];
                        }
                        row += 1;
                    }

                    ep.Save();
                }
                msg.Success = true;
            }
            catch (Exception ex)
            {
                LogUtil.Logger.Error(ex.Message, ex);

                msg.Content = ex.Message;
            }
            return msg;
        }
    }
}
