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
    public class DepartmentExcelHelper : ExcelHelperBase
    {
        public DepartmentExcelHelper() { }

        public DepartmentExcelHelper(string dbString) : base(dbString)
        {
            this.DbString = dbString;
        }

        public DepartmentExcelHelper(string dbString, string filePath) : base(dbString, filePath)
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
                List<DepartmentExcelModel> records = new List<DepartmentExcelModel>();
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
                            records.Add(new DepartmentExcelModel()
                            {
                                Name = ws.Cells[i, 1].Value == null ? string.Empty : ws.Cells[i, 1].Value.ToString(),
                                Remark = ws.Cells[i, 2].Value == null ? string.Empty : ws.Cells[i, 2].Value.ToString(),
                                ParentDepartmentName = ws.Cells[i, 3].Value == null ? string.Empty : ws.Cells[i, 3].Value.ToString(),
                                CompanyName = ws.Cells[i, 4].Value == null ? string.Empty : ws.Cells[i, 4].Value.ToString()
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
                                for (int i = 0; i < DepartmentExcelModel.Headers.Count(); i++)
                                {
                                    sheet.Cells[1, i + 1].Value = DepartmentExcelModel.Headers[i];
                                }
                                ///写入错误数据
                                for (int i = 0; i < records.Count(); i++)
                                {
                                    sheet.Cells[i + 2, 1].Value = records[i].Name;
                                    sheet.Cells[i + 2, 2].Value = records[i].Remark;
                                    sheet.Cells[i + 2, 3].Value = records[i].ParentDepartmentName;
                                    sheet.Cells[i + 2, 4].Value = records[i].CompanyName;
                                    sheet.Cells[i + 2, 5].Value = records[i].ValidateMessage.ToString();

                                }

                                /// 保存
                                ep.Save();
                            }
                        }
                        else
                        {

                            foreach (var r in records)
                            {
                                 
                                IDepartmentService ds = new DepartmentService(this.DbString);

                                if (!string.IsNullOrEmpty(r.ParentDepartmentName))
                                {
                                    BlueHrLib.Data.Department pd = ds.FindByIdWithCompanyId(r.Company.id, r.ParentDepartmentName);
                                    if (pd == null)
                                    {
                                    }
                                    else
                                    {

                                        BlueHrLib.Data.Department d = ds.FindByIdWithCompanyIdAndParentId(r.Company.id, r.Name,pd.id);

                                        if (d == null)
                                        {
                                            r.ParentDepartment = pd;

                                            BlueHrLib.Data.Department dd = new BlueHrLib.Data.Department();
                                            dd.name = r.Name;
                                            dd.remark = r.Remark;
                                            dd.companyId = r.Company.id;
                                            dd.parentId = r.ParentDepartment.id;
                                            ds.Create(dd);
                                        }
                                    }
                                }
                                else
                                {
                                    BlueHrLib.Data.Department d = ds.FindByIdWithCompanyId(r.Company.id, r.Name);
                                    if (d == null)
                                    {
                                        BlueHrLib.Data.Department dd = new BlueHrLib.Data.Department();
                                        dd.name = r.Name;
                                        dd.remark = r.Remark;
                                        dd.companyId = r.Company.id;

                                        ds.Create(dd);

                                    }
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

        public List<DepartmentExcelModel> Validates(List<DepartmentExcelModel> models)
        {
            foreach (var m in models)
            {
                m.Validate(this.DbString);
            }
            return models;
        }


    }
}
