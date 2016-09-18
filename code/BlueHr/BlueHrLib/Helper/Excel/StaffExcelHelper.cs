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
    public class StaffExcelHelper : ExcelHelperBase
    {
        public string BankCardName = string.Empty;
        public StaffExcelHelper() { }
        public StaffExcelHelper(string dbString):base(dbString)
        {
            this.DbString = dbString;
        }

        public StaffExcelHelper(string dbString, string filePath):base(dbString,filePath)
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
                List<StaffExcelModel> records = new List<StaffExcelModel>();
                string sheetName = "Tmp";
                /// 读取excel文件
                using (ExcelPackage ep = new ExcelPackage(fileInfo))
                {
                    if (ep.Workbook.Worksheets.Count > 0)
                    {
                        ExcelWorksheet ws = ep.Workbook.Worksheets.First();
                        sheetName = ws.Name;
                        BankCardName =     ws.Cells[1, 34].Value == null ? string.Empty : ws.Cells[1, 34].Value.ToString();

                        for (int i = 2; i <= ws.Dimension.End.Row; i++)
                        {
                            records.Add(new StaffExcelModel()
                            {
                                NoStr = ws.Cells[i, 1].Value == null? string.Empty:ws.Cells[i,1].Value.ToString().Trim(),
                                Nr = ws.Cells[i, 2].Value == null? string.Empty:ws.Cells[i, 2].Value.ToString().Trim(),
                                Name = ws.Cells[i, 3].Value == null ? string.Empty : ws.Cells[i, 3].Value.ToString().Trim(),
                                SexStr = ws.Cells[i, 4].Value == null ? string.Empty : ws.Cells[i, 4].Value.ToString().Trim(),
                                BirthdayStr = ws.Cells[i, 5].Value == null ? string.Empty : ws.Cells[i, 5].Value.ToString().Trim(),
                                //年龄  数据库中没有这个字段
                                AgeStr = ws.Cells[i, 6].Value == null ? string.Empty : ws.Cells[i, 6].Value.ToString().Trim(),

                                FirstCompanyEmployAtStr = ws.Cells[i, 7].Value == null ? string.Empty : ws.Cells[i, 7].Value.ToString().Trim(),
                                TotalCompanySeniorityStr = ws.Cells[i, 8].Value == null ? string.Empty : ws.Cells[i, 8].Value.ToString().Trim(),
                                CompanyEmployAtStr = ws.Cells[i, 9].Value == null ? string.Empty : ws.Cells[i, 9].Value.ToString().Trim(),
                                CompanySeniorityStr = ws.Cells[i, 10].Value == null ? string.Empty : ws.Cells[i, 10].Value.ToString().Trim(),
                                //试用期到期时间， Excel没有IsOnTrial 字段
                                TrialOverAtStr = ws.Cells[i, 11].Value == null ? string.Empty : ws.Cells[i, 11].Value.ToString().Trim(),
                                DepartmentIdStr = ws.Cells[i, 12].Value == null ? string.Empty : ws.Cells[i, 12].Value.ToString().Trim(),
                                CompanyIdStr = ws.Cells[i, 13].Value == null ? string.Empty : ws.Cells[i, 13].Value.ToString().Trim(),
                                JobTitleIdStr = ws.Cells[i, 14].Value == null ? string.Empty : ws.Cells[i, 14].Value.ToString().Trim(),
                                StaffTypeIdStr = ws.Cells[i, 15].Value == null ? string.Empty : ws.Cells[i, 15].Value.ToString().Trim(),
                                DegreeTypeIdStr = ws.Cells[i, 16].Value == null ? string.Empty : ws.Cells[i, 16].Value.ToString().Trim(),
                                Speciality = ws.Cells[i, 17].Value == null ? string.Empty : ws.Cells[i, 17].Value.ToString().Trim(),
                                //职业证书， 只有name ， 没法进行添加
                                JobCertificate = ws.Cells[i, 18].Value == null ? string.Empty : ws.Cells[i, 18].Value.ToString().Trim(),

                                ResidenceAddress = ws.Cells[i, 19].Value == null ? string.Empty : ws.Cells[i, 19].Value.ToString().Trim(),
                                Address = ws.Cells[i, 20].Value == null ? string.Empty : ws.Cells[i, 20].Value.ToString().Trim(),
                                //身份证是否验证， 默认为false Excel没有 IsIdChecked字段
                                Id = ws.Cells[i, 21].Value == null ? string.Empty : ws.Cells[i, 21].Value.ToString().Trim(),
                                Phone = ws.Cells[i, 22].Value == null ? string.Empty : ws.Cells[i, 22].Value.ToString().Trim(),
                                ContactName = ws.Cells[i, 23].Value == null ? string.Empty : ws.Cells[i, 23].Value.ToString().Trim(),
                                ContactPhone = ws.Cells[i, 24].Value == null ? string.Empty : ws.Cells[i, 24].Value.ToString().Trim(),
                                ContactFamilyMemberType = ws.Cells[i, 25].Value == null ? string.Empty : ws.Cells[i, 25].Value.ToString().Trim(),
                                Domicile = ws.Cells[i, 26].Value == null ? string.Empty : ws.Cells[i, 26].Value.ToString().Trim(),
                                ResidenceTypeStr = ws.Cells[i, 27].Value == null ? string.Empty : ws.Cells[i, 27].Value.ToString().Trim(),
                                InsureTypeIdStr = ws.Cells[i, 28].Value == null ? string.Empty : ws.Cells[i, 28].Value.ToString().Trim(),
                                IsPayCPFStr = ws.Cells[i, 29].Value == null ? string.Empty : ws.Cells[i, 29].Value.ToString().Trim(),
                                ContractExpireStr = ws.Cells[i, 30].Value == null ? string.Empty : ws.Cells[i, 30].Value.ToString().Trim(),
                                ContractCountStr = ws.Cells[i, 31].Value == null ? string.Empty : ws.Cells[i, 31].Value.ToString().Trim(),
                                //photo 字段 在Excel中， 但是无法将之转化到数据库中
                                Photo = ws.Cells[i, 32].Value == null ? string.Empty : ws.Cells[i, 32].Value.ToString().Trim(),
                                //健康证发证日期， 因为没有nr， 所以无法将之添加到证照表中
                                HealthCertificateEffectiveFromStr = ws.Cells[i, 33].Value == null ? string.Empty : ws.Cells[i, 33].Value.ToString().Trim(),
                                BankCardNrStr = ws.Cells[i, 34].Value == null ? string.Empty : ws.Cells[i, 34].Value.ToString().Trim(),
                                WorkingYearsAtStr = ws.Cells[i, 35].Value == null ? string.Empty : ws.Cells[i, 35].Value.ToString().Trim(),
                                TotalSeniorityStr = ws.Cells[i, 36].Value == null ? string.Empty : ws.Cells[i, 36].Value.ToString().Trim(),
                                //子女信息， excel只记录了 子女的出生日期和子女年龄， 没有子女姓名， 所以没法写入数据库
                                FamilyMemberBirthdayStr = ws.Cells[i, 37].Value == null ? string.Empty : ws.Cells[i, 37].Value.ToString().Trim(),
                                FamilyMemberAgeStr = ws.Cells[i, 38].Value == null ? string.Empty : ws.Cells[i, 38].Value.ToString().Trim(),

                                Remark = ws.Cells[i, 39].Value == null ? string.Empty : ws.Cells[i, 39].Value.ToString().Trim(),
                                WorkStatusStr = ws.Cells[i, 40].Value == null ? string.Empty : ws.Cells[i, 40].Value.ToString().Trim()
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
                                for (int i = 0; i < StaffExcelModel.Headers.Count(); i++)
                                {
                                    sheet.Cells[1, i + 1].Value = StaffExcelModel.Headers[i];
                                }
                                ///写入错误数据
                                for (int i = 0; i < records.Count(); i++)
                                {
                                    sheet.Cells[i + 2, 1].Value = records[i].NoStr;
                                    sheet.Cells[i + 2, 2].Value = records[i].Nr;
                                    sheet.Cells[i + 2, 3].Value = records[i].Name;
                                    sheet.Cells[i + 2, 4].Value = records[i].SexStr;
                                    sheet.Cells[i + 2, 5].Value = records[i].BirthdayStr;
                                    sheet.Cells[i + 2, 6].Value = records[i].AgeStr;
                                    //sheet.Cells[i + 2, 7].Value = records[i].Ethnic;
                                    sheet.Cells[i + 2, 7].Value = records[i].FirstCompanyEmployAt;
                                    sheet.Cells[i + 2, 8].Value = records[i].TotalCompanySeniorityStr;
                                    sheet.Cells[i + 2, 9].Value = records[i].CompanyEmployAtStr;
                                    sheet.Cells[i + 2, 10].Value = records[i].CompanySeniorityStr;
                                    //sheet.Cells[i + 2, 11].Value = records[i].isOnTrial;
                                    sheet.Cells[i + 2, 11].Value = records[i].TrialOverAtStr;
                                    sheet.Cells[i + 2, 12].Value = records[i].DepartmentIdStr;
                                    sheet.Cells[i + 2, 13].Value = records[i].CompanyIdStr;
                                    sheet.Cells[i + 2, 14].Value = records[i].JobTitleIdStr;
                                    sheet.Cells[i + 2, 15].Value = records[i].StaffTypeIdStr;
                                    sheet.Cells[i + 2, 16].Value = records[i].DegreeTypeIdStr;
                                    sheet.Cells[i + 2, 17].Value = records[i].Speciality;
                                    sheet.Cells[i + 2, 18].Value = records[i].JobCertificate;
                                    sheet.Cells[i + 2, 19].Value = records[i].ResidenceAddress;
                                    sheet.Cells[i + 2, 20].Value = records[i].Address;
                                    //sheet.Cells[i + 2, 20].Value = records[i].IsIdChecked;
                                    sheet.Cells[i + 2, 21].Value = records[i].Id;
                                    sheet.Cells[i + 2, 22].Value = records[i].Phone;
                                    sheet.Cells[i + 2, 23].Value = records[i].ContactName;
                                    sheet.Cells[i + 2, 24].Value = records[i].ContactPhone;
                                    sheet.Cells[i + 2, 25].Value = records[i].ContactFamilyMemberType;
                                    sheet.Cells[i + 2, 26].Value = records[i].Domicile;
                                    sheet.Cells[i + 2, 27].Value = records[i].ResidenceTypeStr;
                                    sheet.Cells[i + 2, 28].Value = records[i].InsureTypeIdStr;
                                    sheet.Cells[i + 2, 29].Value = records[i].IsPayCPFStr;
                                    sheet.Cells[i + 2, 30].Value = records[i].ContractExpireStr;
                                    sheet.Cells[i + 2, 31].Value = records[i].ContractCountStr;
                                    sheet.Cells[i + 2, 32].Value = records[i].Photo;
                                    sheet.Cells[i + 2, 33].Value = records[i].HealthCertificateEffectiveFromStr;
                                    sheet.Cells[i + 2, 34].Value = records[i].BankCardNrStr;
                                    sheet.Cells[i + 2, 35].Value = records[i].WorkingYearsAtStr;
                                    sheet.Cells[i + 2, 36].Value = records[i].TotalSeniorityStr;
                                    sheet.Cells[i + 2, 37].Value = records[i].FamilyMemberBirthdayStr;
                                    sheet.Cells[i + 2, 38].Value = records[i].FamilyMemberAgeStr;
                                    sheet.Cells[i + 2, 39].Value = records[i].Remark;
                                    sheet.Cells[i + 2, 40].Value = records[i].WorkStatusStr;
                                    sheet.Cells[i + 2, 41].Value = records[i].ValidateMessage.ToString();
                                }

                                /// 保存
                                ep.Save();
                            }
                        }
                        else
                        {
                            /// 数据写入数据库

                            for (var i = 0; i < records.Count; i++)
                            {
                                //公司
                                if (!string.IsNullOrWhiteSpace(records[i].CompanyIdStr))
                                {
                                    try
                                    {
                                        ICompanyService cs = new CompanyService(this.DbString);
                                        Company company = cs.FindByName(records[i].CompanyIdStr);

                                        records[i].CompanyId = company.id;
                                    }
                                    catch (Exception e)
                                    {
                                        Console.Write(e);
                                        records[i].CompanyId = null;
                                    }
                                }

                                //部门
                                if (!string.IsNullOrWhiteSpace(records[i].DepartmentIdStr))
                                {
                                    try
                                    {
                                        IDepartmentService ds = new DepartmentService(this.DbString);
                                        Data.Department department = ds.FindByIdWithCompanyId(records[i].CompanyId, records[i].DepartmentIdStr);

                                        records[i].DepartmentId = department.id;
                                    }
                                    catch
                                    {
                                        records[i].DepartmentId = null;
                                    }
                                }

                                //职位
                                if (!string.IsNullOrWhiteSpace(records[i].JobTitleIdStr))
                                {
                                    try
                                    {
                                        IJobTitleService jts = new JobTitleService(this.DbString);
                                        JobTitle jobtitle = jts.FindByName(records[i].JobTitleIdStr);
                                        records[i].JobTitleId = jobtitle.id;
                                    }
                                    catch (Exception e)
                                    {
                                        Console.Write(e);
                                        records[i].JobTitleId = null;
                                    }
                                }

                                //人员类别
                                if (!string.IsNullOrWhiteSpace(records[i].StaffTypeIdStr))
                                {
                                    try
                                    {
                                        IStaffTypeService sts = new StaffTypeService(this.DbString);
                                        StaffType staffType = sts.FindByName(records[i].StaffTypeIdStr);
                                        records[i].StaffTypeId = staffType.id;
                                    }
                                    catch
                                    {
                                        records[i].StaffTypeId = null;
                                    }
                                }

                                //最高学历
                                if (!string.IsNullOrWhiteSpace(records[i].DegreeTypeIdStr))
                                {
                                    try
                                    {
                                        IDegreeTypeService dts = new DegreeTypeService(this.DbString);
                                        DegreeType degreeType = dts.FindByName(records[i].DegreeTypeIdStr);
                                        records[i].DegreeTypeId = degreeType.id;
                                    }
                                    catch
                                    {
                                        records[i].DegreeTypeId = null;
                                    }
                                }

                                //保险种类
                                if (!string.IsNullOrWhiteSpace(records[i].InsureTypeIdStr))
                                {
                                    try
                                    {
                                        IInSureTypeService ists = new InSureTypeService(this.DbString);
                                        InsureType insuretype = ists.FindByName(records[i].InsureTypeIdStr);
                                        records[i].InsureTypeId = insuretype.id;
                                    }
                                    catch
                                    {
                                        records[i].InsureTypeId = null;
                                    }
                                }

                            }
                            List<Staff> details = StaffExcelModel.Convert(records);

                            for (var i = 0; i < details.Count; i++)
                            {
                                //新增员工
                                IStaffService ss = new StaffService(this.DbString);
                                var StaffResult = ss.Create(details[i]);

                                //在新建 员工 成功之后， 进行银行卡添加
                                if (StaffResult)
                                {
                                    bool bankCardResult = true;
                                    //如果填写了交通银行卡 卡号， 进行添加
                                    if (!string.IsNullOrWhiteSpace(records[i].BankCardNrStr))
                                    {
                                        BankCard bankCard = new BankCard();

                                        bankCard.nr = records[i].BankCardNrStr;
                                        bankCard.bank = BankCardName;
                                        bankCard.staffNr = records[i].Nr;

                                        IBankCardService bcs = new BankCardService(this.DbString);
                                        bankCardResult = bcs.Create(bankCard);
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

        public List<StaffExcelModel> Validates(List<StaffExcelModel> models)
        {
            foreach (var m in models)
            {
                m.Validate(this.DbString);
            }
            return models;
        }

     
    }
}
