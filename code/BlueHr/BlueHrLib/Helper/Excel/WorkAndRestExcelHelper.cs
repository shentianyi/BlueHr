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
    public class WorkAndRestExcelHelper : ExcelHelperBase
    {
        public string BankCardName = string.Empty;
        public WorkAndRestExcelHelper() { }
        public WorkAndRestExcelHelper(string dbString):base(dbString)
        {
            this.DbString = dbString;
        }

        public WorkAndRestExcelHelper(string dbString, string filePath):base(dbString,filePath)
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
                List<WorkAndRestExcelModel> records = new List<WorkAndRestExcelModel>();
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
                            records.Add(new WorkAndRestExcelModel()
                            {
                                DateAtStr = ws.Cells[i, 1].Value == null ? string.Empty : ws.Cells[i, 1].Value.ToString().Trim(),
                                DateTypeStr = ws.Cells[i, 2].Value == null ? string.Empty : ws.Cells[i, 2].Value.ToString().Trim(),
                                Remark = ws.Cells[i, 3].Value == null ? string.Empty : ws.Cells[i, 3].Value.ToString().Trim()
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
                                for (int i = 0; i < WorkAndRestExcelModel.Headers.Count(); i++)
                                {
                                    sheet.Cells[1, i + 1].Value = WorkAndRestExcelModel.Headers[i];
                                }
                                ///写入错误数据
                                for (int i = 0; i < records.Count(); i++)
                                {
                                    sheet.Cells[i + 2, 1].Value = records[i].DateAtStr;
                                    sheet.Cells[i + 2, 2].Value = records[i].DateTypeStr;
                                    sheet.Cells[i + 2, 3].Value = records[i].Remark;
                                    sheet.Cells[i + 2, 4].Value = records[i].ValidateMessage.ToString();
                                }

                                /// 保存
                                ep.Save();
                            }
                        }
                        else
                        {
                            List<WorkAndRest> details = WorkAndRestExcelModel.Convert(records);

                            for (var i = 0; i < details.Count; i++)
                            {
                                //新增员工
                                IWorkAndRestService wrs = new WorkAndRestService(this.DbString);
                                //TODO:Something

                                var WorkAndRestResult = wrs.Create(details[i]);

                                if (WorkAndRestResult)
                                {
                                    msg.Content = "导入成功";
                                }else
                                {
                                    msg.Content = "日期已经存在或者数据有问题";
                                }
                            }
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

        public List<WorkAndRestExcelModel> Validates(List<WorkAndRestExcelModel> models)
        {
            foreach (var m in models)
            {
                m.Validate(this.DbString);
            }
            return models;
        }

     
    }
}
