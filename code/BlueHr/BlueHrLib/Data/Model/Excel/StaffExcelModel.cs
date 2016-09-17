using BlueHrLib.Data.Message;
using BlueHrLib.Properties;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Excel
{
    public class StaffExcelModel : BaseExcelModel
    {

        public static List<string> Headers = new List<string>() { "计数", "工号", "姓名", "性别", "出生年月", "年龄", "最初进厂日期",
            "进厂总工领", "进厂日期", "工龄", "试用期到期日", "部门", "公司", "职位", "人员类别", "最高学历", "所学专业", "职业证书", "户口地址", "通信地址",
            "身份证号码", "联系电话", "紧急联络人", "紧急联络电话", "与员工关系", "户籍", "户口性质", "保险种类", "是否交公积金(T/F)", "合同到期", "2008年签订合同次数",
            "照片", "健康证发证日期", "交通银行卡号(可变为：交通银行卡号，农业银行卡号等)", "参加工作年限", "累计工龄", "子女出生日期", "子女年龄", "备注" ,"是否在职(T/F)"};

        public string NoStr { get; set; }

        public int? No {
            get
            {
                try
                {
                    return int.Parse(this.NoStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 员工号
        /// </summary>
        public string Nr { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 员工性别
        /// </summary>
        public string SexStr { get; set; }

        public string Sex {
            get
            {
                try
                {
                    // 男女 有可能不填写
                    if (SexStr == "男")
                    {
                        return "0";
                    }else if(SexStr == "女")
                    {
                        return "1";
                    }else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 出生年月
        /// </summary>
        public string BirthdayStr { get; set; }
        public DateTime? Birthday {
            get
            {
                try
                {
                    return DateTime.Parse(this.BirthdayStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        public string AgeStr { get; set; }

        public int? Age {
            get
            {
                try
                {
                    return int.Parse(this.AgeStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 民族
        /// </summary>
        public string Ethnic { get; set; }

        /// <summary>
        /// 最初进厂日期
        /// </summary>
        public string FirstCompanyEmployAtStr { get; set; }

        // public TimeSpan RecordAtTime { get; set; }

        public DateTime? FirstCompanyEmployAt
        {
            get
            {
                try
                {
                    return DateTime.Parse(this.FirstCompanyEmployAtStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 进厂总工龄
        /// </summary>
        public string TotalCompanySeniorityStr { get; set; }

        public float? TotalCompanySeniority
        {
            get
            {
                try
                {
                    return float.Parse(this.TotalCompanySeniorityStr);
                }
                catch
                {

                    return null;
                }
            }
        }

        /// <summary>
        /// 进厂日期
        /// </summary>
        public  string CompanyEmployAtStr { get; set; }

        public DateTime? CompanyEmployAt
        {
            get
            {
                try
                {
                    return DateTime.Parse(this.CompanyEmployAtStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 进厂工龄
        /// </summary>
        public string CompanySeniorityStr { get; set; }
        public float? CompanySeniority
        {
            get
            {
                try
                {
                    return float.Parse(this.CompanySeniorityStr);
                }
                catch
                {

                    return null;
                }
            }
        }

        public string WorkStatusStr { get; set; }

        //100在职， 200 离职
        public int WorkStatus
        {
            get
            {
                try
                {
                    return WorkStatusStr == "T" ? 100 : 200;
                }
                catch
                {
                    //字段不可空  
                    //出错的话，怎么处理? 
                    return 100;
                }
            }
        }

        /// <summary>
        /// 是否在试用，  写入字段
        /// </summary>
        public bool IsOnTrial { get; set; }

        /// <summary>
        /// 试用期到期日
        /// </summary>
        public string TrialOverAtStr { get; set; }

        public  DateTime? TrialOverAt
        {
            get
            {
                try
                {
                    return DateTime.Parse(this.TrialOverAtStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 单位  公司
        /// </summary>
        public string CompanyIdStr { get; set; }

        public int? CompanyId { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string DepartmentIdStr { get; set; }

        public int? DepartmentId { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string JobTitleIdStr { get; set; }

        public int? JobTitleId{ get; set; }

        /// <summary>
        /// 人员类别
        /// </summary>
        public string StaffTypeIdStr { get; set; }

        public int? StaffTypeId{ get; set;}

        /// <summary>
        /// 最高学历
        /// </summary>
        public string DegreeTypeIdStr { get; set; }

        public int? DegreeTypeId{ get; set;}

        /// <summary>
        /// 所学专业
        /// </summary>
        public string Speciality { get; set; }

        /// <summary>
        /// 职业证书
        /// </summary>
        public string JobCertificate { get; set; }

        /// <summary>
        /// 户口地址
        /// </summary>
        public string ResidenceAddress { get; set; }

        /// <summary>
        /// 通信地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 身份证是否验证，  写入字段
        /// </summary>
        public bool IsIdChecked { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 紧急联络人
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 与员工关系
        /// </summary>
        public string ContactFamilyMemberType { get; set; }

        /// <summary>
        /// 户籍
        /// </summary>
        public string Domicile { get; set; }

        /// <summary>
        /// 户口性质  0 是非农， 1 是农业
        /// </summary>
        public string ResidenceTypeStr { get; set; }

        public int? ResidenceType {
            get
            {
                try
                {
                    // 不是 农业 就是 非农  但是存在不填写情况
                    if (ResidenceTypeStr == "非农")
                    {
                        return 0;
                    }
                    else if (ResidenceTypeStr == "农业") {
                        return 1;
                    }else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 保险种类  需要进行计算判断
        /// </summary>
        public string InsureTypeIdStr { get; set; }

        public int? InsureTypeId{ get; set; }

        /// <summary>
        /// 是否交公积金
        /// </summary>
        public string IsPayCPFStr { get; set; }

        public bool IsPayCPF
        {
            get
            {
                try
                {
                    return IsPayCPFStr == "T" ? true : false;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 合同到期
        /// </summary>
        public string ContractExpireStr { get; set; }

        /// <summary>
        /// 合同到期日期 转化为日期
        /// </summary>
        public DateTime? ContractExpireAt
        {
            get
            {
                try
                {
                    return DateTime.Parse(this.ContractExpireStr);
                }
                catch
                {

                    return null;
                }
            }
        }

        /// <summary>
        /// 合同签订次数
        /// </summary>
        public string ContractCountStr { get; set; }

        public int? ContractCount {
            get
            {
                try
                {
                    return int.Parse(this.ContractCountStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 照片
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 健康证发证日期
        /// </summary>
        public string HealthCertificateEffectiveFromStr { get; set; }

        public DateTime? HealthCertificateEffectiveFrom {
            get
            {
                try
                {
                    return DateTime.Parse(this.HealthCertificateEffectiveFromStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 交通银行卡号
        /// </summary>
        public string BankCardNrStr { get; set; }

        /// <summary>
        /// 参加工作年限
        /// </summary>
        public string WorkingYearsAtStr { get; set; }

        public DateTime? WorkingYearsAt {
            get
            {
                try
                {
                    return DateTime.Parse(this.WorkingYearsAtStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 累计工龄
        /// </summary>
        public string TotalSeniorityStr { get; set; }

        public float? TotalSeniority {
            get
            {
                try
                {
                    return float.Parse(this.TotalSeniorityStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 子女出生日期
        /// </summary>
        public string FamilyMemberBirthdayStr{ get; set; }

        public DateTime? FamilyMemberBirthday
        {
            get
            {
                try
                {
                    return DateTime.Parse(this.FamilyMemberBirthdayStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 子女年龄
        /// </summary>
        public string FamilyMemberAgeStr { get; set; }

        public int? FamilyMemberAge
        {
            get
            {
                try
                {
                    return int.Parse(this.FamilyMemberAgeStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dbString"></param>
        public void Validate(string dbString)
        {
            ValidateMessage msg = new ValidateMessage();

            if ( string.IsNullOrEmpty(this.Nr))
            {
                msg.Contents.Add("员工号不可空");
            }

            //if (string.IsNullOrEmpty(this.Id))
            //{
            //    msg.Contents.Add("身份证号码不可空");
            //}

            msg.Success = msg.Contents.Count == 0;

            this.ValidateMessage = msg;
        }

        
        public static List<Staff> Convert(List<StaffExcelModel> models)
        {
            List<Staff> records = new List<Staff>();
            foreach (var m in models)
            {
                if (m.ValidateMessage != null && m.ValidateMessage.Success)
                {
                    //如果 试用期到期 有内容， 在试用
                    // 如果 试用期到期时间 在今天之前， 那么有可能已经转正
                    if (m.TrialOverAt.HasValue)
                    {
                        m.IsOnTrial = true;
                    }else
                    {
                        m.IsOnTrial = false;
                    }

                    //子女信息
                    //TODO:子女信息只有: 子女出生日期， 子女年龄， 没有 子女姓名， 所以 没法写入数据库

                    records.Add(new Staff()
                    {
                        nr = m.Nr,
                        name = m.Name,
                        sex = m.Sex,
                        birthday = m.Birthday,
                        ethnic = m.Ethnic,
                        firstCompanyEmployAt = m.FirstCompanyEmployAt,
                        totalCompanySeniority = m.TotalCompanySeniority,
                        companyEmployAt = m.CompanyEmployAt,
                        companySeniority = m.CompanySeniority,
                        isOnTrial = m.IsOnTrial,
                        trialOverAt = m.TrialOverAt,
                        workStatus = m.WorkStatus,
                        companyId = m.CompanyId,
                        departmentId = m.DepartmentId,
                        jobTitleId = m.JobTitleId,
                        staffTypeId = m.StaffTypeId,
                        degreeTypeId = m.DegreeTypeId,
                        speciality = m.Speciality,
                        residenceAddress = m.ResidenceAddress,
                        address = m.Address,
                        id = m.Id,
                        isIdChecked = false,
                        phone = m.Phone,
                        contactName = m.ContactName,
                        contactPhone = m.ContactPhone,
                        contactFamilyMemberType = m.ContactFamilyMemberType,
                        domicile = m.Domicile,
                        residenceType = m.ResidenceType,
                        insureTypeId = m.InsureTypeId,
                        isPayCPF = m.IsPayCPF,
                        contractExpireStr = m.ContractExpireStr,
                        contractCount = m.ContractCount,
                        workingYearsAt = m.WorkingYearsAt,
                        totalSeniority = m.TotalCompanySeniority,
                        remark = m.Remark
                    });
                }
            }
            return records;
        }
    }
}
