using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Enum;
using BlueHrLib.CusException;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Message;

namespace BlueHrLib.Service.Implement
{
    public class StaffService : ServiceBase, IStaffService
    {
        private IStaffRepository staffRep;
        public StaffService(string dbString) : base(dbString)
        {
            staffRep = new StaffRepository(this.Context);
        }

        public Staff FindByStaffId(string id)
        {
            DataContext dc = new DataContext(this.DbString);
            return dc.Context.GetTable<Staff>().FirstOrDefault(s => s.id.Equals(id));
        }

        public bool CheckStaffById(string id)
        {
            throw new NotImplementedException();
        }

        public bool CheckStaffAndUpdateInfo(StaffIdCard card)
        {
            DataContext dc = new DataContext(this.DbString);
            Staff staff = dc.Context.GetTable<Staff>().FirstOrDefault(s => s.id.Equals(card.id));
            if (staff != null)
            {
                if (string.IsNullOrEmpty(staff.name))
                {
                    staff.name = card.name;
                }
                if (string.IsNullOrEmpty(staff.sex))
                {
                    staff.sex = card.sex;
                }
                if (string.IsNullOrEmpty(staff.ethnic))
                {
                    staff.ethnic = card.ethnic;
                }
                if (!staff.birthday.HasValue)
                {
                    staff.birthday = card.birthday;
                }
                if (string.IsNullOrEmpty(staff.residenceAddress))
                {
                    staff.residenceAddress = card.residenceAddress;
                }
                if (string.IsNullOrEmpty(staff.photo))
                {
                    staff.photo = card.photo;
                }
                // 将是否已经身份证验证置为true
                staff.isIdChecked = true;
                this.CreateOrUpdateIdCertificate(dc, staff, card);

                dc.Context.SubmitChanges();

                try
                {
                    IMessageRecordService mrs = new MessageRecordService(this.DbString);
                    mrs.CreateStaffIdCheckMessage(staff.nr);
                }
                catch { }

                return true;
            }
            return false;
        }

        public bool CreateInfoAndSetCheck(StaffIdCard card, bool isIdChecked = false)
        {
            Staff staff = new Staff()
            {
                nr = Guid.NewGuid().ToString(),
                name = card.name,
                sex = card.sex,
                ethnic = card.ethnic,
                birthday = card.birthday,
                residenceAddress = card.residenceAddress,
                id = card.id,
                isIdChecked = isIdChecked
            };
            DataContext dc = new DataContext(this.DbString);
            //  this.CreateOrUpdateIdCertificate(dc, staff, card);
            dc.Context.GetTable<Staff>().InsertOnSubmit(staff);
            dc.Context.SubmitChanges();
            return true;
        }

        private void CreateOrUpdateIdCertificate(DataContext dc, Staff staff, StaffIdCard card)
        {
            // 建立身份证的证照信息
            CertificateType ct = dc.Context.GetTable<CertificateType>().FirstOrDefault(s => s.systemCode.Equals(SystemCertificateType.IdCard));
            if (ct == null)
            {
                throw new SystemCertificateTypeNotFoundException();
            }
            Certificate cer = dc.Context.GetTable<Certificate>().FirstOrDefault(c => c.certificateTypeId.Equals(ct.id) && c.staffNr.Equals(staff.nr));
            if (cer == null)
            {
                cer = new Certificate()
                {
                    certificateTypeId = ct.id,
                    staffNr = staff.nr,
                    effectiveFrom = card.effectiveFrom,
                    effectiveEnd = card.effectiveEnd,
                    institution = card.institution,
                    remark = string.Format("在{0}系统自动创建", DateTime.Now)
                };
                dc.Context.GetTable<Certificate>().InsertOnSubmit(cer);
            }
        }

        public List<string> GetOnShiftStaffs(DateTime date)
        {
            DataContext dc = new DataContext(this.DbString);
            return dc.Context.GetTable<ShiftScheduleView>().Where(s => s.scheduleAt.Date.Equals(date.Date) || (s.scheduleAt.Equals(date.Date.AddDays(-1)) && s.shiftType.Equals(ShiftType.Tommorrow))).Select(s => s.staffNr).Distinct().ToList();
        }

        public IQueryable<Staff> Search(StaffSearchModel searchModel)
        {
            return staffRep.Search(searchModel);
        }

        public void Creates(List<Staff> staffs)
        {
            DataContext dc = new DataContext(this.DbString);
            dc.Context.GetTable<Staff>().InsertAllOnSubmit(staffs);
            dc.Context.SubmitChanges();
        }


        public Staff FindByNr(string nr)
        {
            return new DataContext(this.DbString).Context.GetTable<Staff>().FirstOrDefault(s => s.nr.Equals(nr));
        }
        public Staff FindByNrThis(string nr)
        {
            return this.Context.Context.GetTable<Staff>().FirstOrDefault(s => s.nr == nr);
        }
        public bool Create(Staff staff)
        {
            if ((!string.IsNullOrEmpty(staff.id)) && (FindByStaffId(staff.id)!= null))
            {
                return false;
            }
            return staffRep.Create(staff);
        }

        public Staff FindById(int id)
        {
            return staffRep.FindById(id);
        }

        public bool DeleteByNr(string nr)
        {
            return staffRep.DeleteByNr(nr);
        }

        public bool Update(Staff staff)
        {
            return staffRep.Update(staff);
        }

        /// <summary>
        /// 员工转正
        /// </summary>
        /// <param name="record"></param>
        public ResultMessage ToFullMember(FullMemberRecord record)
        {
            ResultMessage msg = new ResultMessage();
            try
            {
                DataContext dc = new DataContext(this.DbString);
                Staff staff = dc.Context.GetTable<Staff>().FirstOrDefault(s => s.nr.Equals(record.staffNr));
                if (staff != null)
                {
                    if (staff.canTobeFullMember)
                    {
                        if (record.isPassCheck)
                        {
                            staff.isOnTrial = false;
                        }
                        dc.Context.GetTable<FullMemberRecord>().InsertOnSubmit(record);
                        dc.Context.SubmitChanges();
                        msg.Success = true;
                    }
                    else
                    {
                        msg.Content = "员工不可转正";
                    }
                }
                else
                {
                    throw new DataNotFoundException();
                }
            }
            catch (Exception ex)
            {
                msg.Content = ex.Message;
            }
            return msg;
        }

        /// <summary>
        /// 获取需要被转中的员工，
        /// 如果员工的计划转正时间小于参数datetime，则需要被转
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public List<Staff> GetToBeFullsLessThanDate(DateTime datetime)
        {
            DataContext dc = new DataContext(this.DbString);
            SystemSetting setting = dc.Context.GetTable<SystemSetting>().FirstOrDefault();
            if (setting == null)
                throw new SystemSettingNotSetException();

            return dc.Context.GetTable<Staff>().Where(s => s.trialOverAt <= datetime.AddDays(setting.daysBeforeAlertStaffGoFull.Value)
            && s.isOnTrial == true && s.workStatus.Equals((int)WorkStatus.OnWork)).ToList();
        }

        public bool ChangeJob(string[] changeJob)
        {
            //是否要判断
            string StaffNr = changeJob[0];
            int CompanyId = Convert.ToInt16(changeJob[1]);
            int DepartmentId = Convert.ToInt16(changeJob[2]);
            int JobTitleId = Convert.ToInt16(changeJob[3]);

            DataContext dc = new DataContext(this.DbString);
            Staff staff = dc.Context.GetTable<Staff>().FirstOrDefault(s => s.nr.Equals(StaffNr));

            if (staff != null)
            {
                staff.companyId = CompanyId;
                staff.departmentId = DepartmentId;
                staff.jobTitleId = JobTitleId;

                try
                {
                    dc.Context.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        public List<Staff> FindByJobTitleId(int id)
        {
            return staffRep.FindByJobTitleId(id);
        }

        public List<Staff> FindByStaffType(int id)
        {
            return staffRep.FindByStaffType(id);
        }

        public List<Staff> FindByDegreeType(int id)
        {
            return staffRep.FindByDegreeType(id);
        }

        public List<Staff> FindByInsureType(int id)
        {
            return staffRep.FindByInsureType(id);
        }

        public bool IsStaffExist(string nr)
        {
            return staffRep.IsStaffExist(nr);
        }

        public List<Staff> SearchPermanentStaff(StaffSearchModel searchModel)
        {
            return staffRep.SearchPermanentStaff(searchModel);
        }

        //public List<Staff> getStaffUserIDCard()
        //{
        //    return staffRep.getStaffUserIDCard();
        //}
    }
}
