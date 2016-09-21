using BlueHrLib.Data.Message;
using BlueHrLib.Helper;
using BlueHrLib.Helper.Excel;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using Brilliantech.Framwork.Utils.LogUtil;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Excel
{
    public class ExtraWorkTypeExcelHelper : ExcelHelperBase
    {
        public ExtraWorkTypeExcelHelper() { }

        public ExtraWorkTypeExcelHelper(string dbString) : base(dbString)
        {
            this.DbString = dbString;
        }

        public ExtraWorkTypeExcelHelper(string dbString, string filePath) : base(dbString, filePath)
        {
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <returns></returns>
        public ImportMessage Import()
        {
            ImportMessage msg = new ImportMessage() { Success = true };
            try
            {
                FileInfo fileInfo = new FileInfo(this.FilePath);
                List<ExtraWorkRecordExcelModel> records = new List<ExtraWorkRecordExcelModel>();
                string sheetName = "Tmp";
                /// 读取excel文件
                using (ExcelPackage ep = new ExcelPackage(fileInfo))
                {
                    if (ep.Workbook.Worksheets.Count > 0)
                    {
                        ExcelWorksheet ws = ep.Workbook.Worksheets.First();
                        sheetName = ws.Name;
                        for (int i = 2; i <= ws.Dimension.End.Row; i++)
                        {
                            records.Add(new ExtraWorkRecordExcelModel()
                            {
                                OtTimeStr = ws.Cells[i, 1].Value == null ? string.Empty : ws.Cells[i, 1].Value.ToString(),
                                StaffNr = ws.Cells[i, 2].Value == null ? string.Empty : ws.Cells[i, 2].Value.ToString(),
                                Name = ws.Cells[i, 3].Value == null ? string.Empty : ws.Cells[i, 3].Value.ToString(),
                                StartHourStr = ws.Cells[i, 4].Value == null ? string.Empty : ws.Cells[i, 4].Value.ToString(),
                                EndHourStr = ws.Cells[i, 5].Value == null ? string.Empty : ws.Cells[i, 5].Value.ToString(),
                                Duration = ws.Cells[i, 6].Value == null ? string.Empty : ws.Cells[i, 6].Value.ToString(),
                                ExtraWorkTypeStr = ws.Cells[i, 7].Value == null ? string.Empty : ws.Cells[i, 7].Value.ToString(),
                                OtReason = ws.Cells[i, 8].Value == null ? string.Empty : ws.Cells[i, 8].Value.ToString()
                            });
                        }
                    }
                    else
                    {
                        msg.Success = false;
                        msg.Content = "文件不包含数据表，请检查";
                    }
                }
                if (msg.Success)
                {
                    /// 验证数据
                    if (records.Count > 0)
                    {
                        Validates(records);
                        if (records.Where(s => s.ValidateMessage.Success == false).Count() > 0)
                        {
                            /// 创建错误文件
                            msg.Success = false;
                            /// 写入文件夹，然后返回
                            string tmpFile = FileHelper.CreateFullTmpFilePath(Path.GetFileName(this.FilePath), true);
                            msg.Content = FileHelper.GetDownloadTmpFilePath(tmpFile);
                            msg.ErrorFileFeed = true;

                            FileInfo tmpFileInfo = new FileInfo(tmpFile);
                            using (ExcelPackage ep = new ExcelPackage(tmpFileInfo))
                            {
                                ExcelWorksheet sheet = ep.Workbook.Worksheets.Add(sheetName);
                                ///写入Header
                                for (int i = 0; i < ExtraWorkRecordExcelModel.Headers.Count(); i++)
                                {
                                    sheet.Cells[1, i + 1].Value = ExtraWorkRecordExcelModel.Headers[i];
                                }
                                ///写入错误数据
                                for (int i = 0; i < records.Count(); i++)
                                {
                                    sheet.Cells[i + 2, 1].Value = records[i].OtTimeStr;
                                    sheet.Cells[i + 2, 2].Value = records[i].StaffNr;
                                    sheet.Cells[i + 2, 3].Value = records[i].Name;
                                    sheet.Cells[i + 2, 4].Value = records[i].StartHourStr;
                                    sheet.Cells[i + 2, 5].Value = records[i].EndHourStr;
                                    sheet.Cells[i + 2, 6].Value = records[i].Duration;
                                    sheet.Cells[i + 2, 7].Value = records[i].DurationTypeStr;
                                    sheet.Cells[i + 2, 8].Value = records[i].OtReason; 
                                    sheet.Cells[i + 2, 9].Value = records[i].ValidateMessage.ToString();

                                }

                                /// 保存
                                ep.Save();
                            }
                        }
                        else
                        {

                            /// 数据写入数据库
                            List<ExtraWorkRecord> details = ExtraWorkRecordExcelModel.Convert(records);
                            IExtraWorkRecordService ss = new ExtraWorkRecordService(this.DbString);
                            ss.Creates(details);

                        }
                    }
                    else
                    {
                        msg.Success = false;
                        msg.Content = "文件不包含数据，请检查";
                    }
                }
            }
            catch (Exception e)
            {
                msg.Success = false;
                msg.Content = "导入失败：" + e.Message + "，请联系系统管理员";
                LogUtil.Logger.Error(e.Message);
                LogUtil.Logger.Error(e.StackTrace);
            }
            return msg;
        }

        public List<ExtraWorkRecordExcelModel> Validates(List<ExtraWorkRecordExcelModel> models)
        {
            foreach (var m in models)
            {
                m.Validate(this.DbString);
            }
            return models;
        }


    }
}
