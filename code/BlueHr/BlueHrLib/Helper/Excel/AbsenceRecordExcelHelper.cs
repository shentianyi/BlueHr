using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.Excel;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using Brilliantech.Framwork.Utils.LogUtil;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlueHrLib.Helper.Excel
{
    public class AbsenceRecordExcelHelper : ExcelHelperBase
    {
        public AbsenceRecordExcelHelper() { }

        public AbsenceRecordExcelHelper(string dbString) : base(dbString)
        {
            this.DbString = dbString;
        }

        public AbsenceRecordExcelHelper(string dbString, string filePath) : base(dbString, filePath)
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
                List<AbsenceRecordExcelModel> records = new List<AbsenceRecordExcelModel>();
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
                            records.Add(new AbsenceRecordExcelModel()
                            {
                                AbsenceDateStr = ws.Cells[i, 1].Value == null ? string.Empty : ws.Cells[i, 1].Value.ToString(),
                                StaffNr = ws.Cells[i, 2].Value == null ? string.Empty : ws.Cells[i, 2].Value.ToString(),
                                Name = ws.Cells[i, 3].Value == null ? string.Empty : ws.Cells[i, 3].Value.ToString(),
                                AbsenceTypeStr = ws.Cells[i, 4].Value == null ? string.Empty : ws.Cells[i, 4].Value.ToString(),
                                Remark = ws.Cells[i, 5].Value == null ? string.Empty : ws.Cells[i, 5].Value.ToString(),
                                Duration = ws.Cells[i, 6].Value == null ? string.Empty : ws.Cells[i, 6].Value.ToString(),
                                DurationTypeStr = ws.Cells[i, 7].Value == null ? string.Empty : ws.Cells[i, 7].Value.ToString()
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
                                for (int i = 0; i < AbsenceRecordExcelModel.Headers.Count(); i++)
                                {
                                    sheet.Cells[1, i + 1].Value = AbsenceRecordExcelModel.Headers[i];
                                }
                                ///写入错误数据
                                for (int i = 0; i < records.Count(); i++)
                                {
                                    sheet.Cells[i + 2, 1].Value = records[i].AbsenceDateStr;
                                    sheet.Cells[i + 2, 2].Value = records[i].StaffNr;
                                    sheet.Cells[i + 2, 3].Value = records[i].Name;
                                    sheet.Cells[i + 2, 4].Value = records[i].AbsenceTypeStr;
                                    sheet.Cells[i + 2, 5].Value = records[i].Remark;
                                    sheet.Cells[i + 2, 6].Value = records[i].Duration;
                                    sheet.Cells[i + 2, 7].Value = records[i].DurationTypeStr;
                                    sheet.Cells[i + 2, 8].Value = records[i].ValidateMessage.ToString();

                                }

                                /// 保存
                                ep.Save();
                            }
                        }
                        else
                        {

                            /// 数据写入数据库
                            List<AbsenceRecrod> details = AbsenceRecordExcelModel.Convert(records);
                            IAbsenceRecordService ss = new AbsenceRecordService(this.DbString);
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

        public List<AbsenceRecordExcelModel> Validates(List<AbsenceRecordExcelModel> models)
        {
            foreach (var m in models)
            {
                m.Validate(this.DbString);
            }
            return models;
        }


    }
}
