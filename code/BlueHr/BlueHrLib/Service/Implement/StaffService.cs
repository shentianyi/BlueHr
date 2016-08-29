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

namespace BlueHrLib.Service.Implement
{
    public class StaffService : ServiceBase, IStaffService
    {
        public StaffService(string dbString) : base(dbString) { }

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
                // 将是否已经身份证验证置为true
                staff.isIdChecked = true;
                this.CreateOrUpdateIdCertificate(dc, staff, card);
               
                dc.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool CreateInfoAndSetCheck(StaffIdCard card, bool isIdChecked = true)
        {
            Staff staff = new Staff()
            {
                nr = Guid.NewGuid().ToString(),
                name = card.name,
                sex = card.sex,
                ethnic = card.ethnic,
                birthday = card.birthday,
                residenceAddress = card.residenceAddress,
                isIdChecked=isIdChecked
            };
            DataContext dc = new DataContext(this.DbString);
            this.CreateOrUpdateIdCertificate(dc, staff, card);
            dc.Context.GetTable<Staff>().InsertOnSubmit(staff);
            dc.Context.SubmitChanges();
            return true;
        }

        private void CreateOrUpdateIdCertificate(DataContext dc,   Staff staff,StaffIdCard card)
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
            return dc.Context.GetTable<ShiftScheduleView>().Where(s => s.scheduleAt.Date.Equals(date.Date) || (s.scheduleAt.Equals(date.Date.AddDays(-1)) && s.shiftType.Equals(ShiftType.Tommorrow))).Select(s => s.staffNr).ToList();
        }
    }
}
