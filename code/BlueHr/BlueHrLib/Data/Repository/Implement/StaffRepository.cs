using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Interface;
using BlueHrLib.Service.Implement;
using ALinq.Dynamic;
using BlueHrLib.Helper;

namespace BlueHrLib.Data.Repository.Implement
{
    public class StaffRepository : RepositoryBase<Staff>, IStaffRepository
    {
        private BlueHrDataContext context;

        public StaffRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public bool Create(Staff staff)
        {
            if (staff != null)
            {
                try
                {
                    this.context.GetTable<Staff>().InsertOnSubmit(staff);
                    this.context.SubmitChanges();
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    return false;
                }
            }
            return true;
        }

        public bool DeleteByNr(string nr)
        {
            Staff sf = this.context.GetTable<Staff>().FirstOrDefault(c => c.nr.Equals(nr));

            if (sf != null)
            {
                try
                {
                    this.context.GetTable<Staff>().DeleteOnSubmit(sf);
                    this.context.SubmitChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Staff FindById(int id)
        {
            try
            {
                Staff sf = this.context.GetTable<Staff>().FirstOrDefault(c => c.id.Equals(id));
                return sf;
            }
            catch (Exception e)
            {
                Console.Write(e);

                return null;
            }
        }

        public IQueryable<Staff> Search(StaffSearchModel searchModel)
        {
            IQueryable<Staff> staffs = this.context.Staffs;

            if (!string.IsNullOrWhiteSpace(searchModel.Nr))
            {
                staffs = staffs.Where(c => c.nr.Contains(searchModel.Nr.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.NrAct))
            {
                staffs = staffs.Where(c => c.nr.Equals(searchModel.NrAct.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                staffs = staffs.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Id))
            {
                staffs = staffs.Where(c => c.id.Contains(searchModel.Id.Trim()));
            }

            if (searchModel.Sex.HasValue)
            {
                staffs = staffs.Where(c => c.sex.Equals(searchModel.Sex));
            }

            if (searchModel.JobTitleId.HasValue)
            {
                staffs = staffs.Where(c => c.jobTitleId.Equals(searchModel.JobTitleId));
            }

            if (searchModel.companyId.HasValue)
            {
                staffs = staffs.Where(c => c.companyId.Equals(searchModel.companyId));
            }

            if (searchModel.departmentId.HasValue)
            {
                staffs = staffs.Where(c => c.departmentId.Equals(searchModel.departmentId));
            }

            if (searchModel.CompanyEmployAtFrom.HasValue)
            {
                staffs = staffs.Where(c => c.companyEmployAt > searchModel.CompanyEmployAtFrom);
            }

            if (searchModel.CompanyEmployAtTo.HasValue)
            {
                staffs = staffs.Where(c => c.companyEmployAt < searchModel.CompanyEmployAtTo);
            }
            if (searchModel.BirthdayFrom.HasValue)
            {
                staffs = staffs.Where(c => c.birthday > searchModel.BirthdayFrom);
            }

            if (searchModel.BirthdayTo.HasValue)
            {
                staffs = staffs.Where(c => c.birthday < searchModel.BirthdayTo);
            }

            if (searchModel.IsOnTrial.HasValue)
            {
                staffs = staffs.Where(c => c.isOnTrial.Equals(searchModel.IsOnTrial));
            }

            if (!string.IsNullOrEmpty(searchModel.companyIds))
            {
                List<string> ids = searchModel.companyIds.TrimEnd().TrimEnd(',').Split(',').Where(s=>s!="").ToList();
                if (ids.Count > 0)
                {
                    staffs = staffs.Where(c => ids.Contains(c.companyId.ToString()));

                }
            }

            if (!string.IsNullOrEmpty(searchModel.departmentIds))
            {
                List<string> ids = searchModel.departmentIds.TrimEnd().TrimEnd(',').Split(',').Where(s => s != "").ToList();
                if (ids.Count > 0)
                {
                    staffs = staffs.Where(c => ids.Contains(c.departmentId.ToString()));

                }
            }
            //在员工管理-员工列表、排班管理-排班管理、缺勤管理、加班管理的列表中，用户如果有权限查看列表，那么只可以查看他所管理部门中的所有员工(员工中已有部门、公司)
            if (searchModel.loginUser != null)
            {
                User lgUser = searchModel.loginUser;

                List<SysUserDataAuth> allDataAuths = this.context.GetTable<SysUserDataAuth>().Where(p => p.userId == lgUser.id).ToList();

                List<string> cmpIds = new List<string>();
                List<string> depIds = new List<string>();

                allDataAuths.ForEach(p =>
                {
                    if (!cmpIds.Contains(p.cmpId.ToString()))
                    {
                        cmpIds.Add(p.cmpId.ToString());
                    }

                    p.departId.Split(',').ToList().ForEach(pp =>
                    {
                        if(!string.IsNullOrEmpty(pp))
                        {
                            depIds.Add(pp);
                        }
                    });
                    //if (!depIds.Contains(p.departId.ToString()))
                    //{
                    //    depIds.Add(p.departId.ToString());
                    //}
                });

                if (cmpIds.Count > 0)
                {
                    staffs = staffs.Where(c => cmpIds.Contains(c.companyId.ToString()));
                }

                if (depIds.Count > 0)
                {
                    staffs = staffs.Where(c => depIds.Contains(c.departmentId.ToString()));
                }

                if (searchModel.WorkStatus.HasValue)
                {
                        staffs = staffs.Where(c => c.workStatus.Equals(searchModel.WorkStatus));
                }
            }

            return staffs;
        }
        public List<Staff> SearchPermanentStaff(StaffSearchModel searchModel)
        {
            List<Staff> staffs = this.context.Staffs.Where(s => s.isOnTrial == false).ToList(); ;
            IQueryable<MessageRecord> messages = this.context.MessageRecord.Where(m => m.messageType == 201);
            foreach (var a in messages.ToList())
            {
                staffs.Add( FindByNr(a.staffNr));
            }
            return staffs;
        }
        public Staff FindByNr(string nr)
        {
            return this.context.GetTable<Staff>().FirstOrDefault(s => s.nr == nr);
        }
        public bool Update(Staff staff)
        {
            try
            {
                Staff sf = this.context.GetTable<Staff>().FirstOrDefault(c => c.nr.Equals(staff.nr));
                if (sf != null)
                {
                    sf.OperatorId = staff.OperatorId;
                    sf.PropertyChanged += Sf_PropertyChanged;
                    sf.nr = staff.nr;
                    sf.name = staff.name;
                    sf.sex = staff.sex;
                    sf.birthday = staff.birthday;
                    sf.firstCompanyEmployAt = staff.firstCompanyEmployAt;
                    sf.totalCompanySeniority = staff.totalCompanySeniority;
                    sf.companyEmployAt = staff.companyEmployAt;
                    sf.companySeniority = staff.companySeniority;
                    sf.workStatus = staff.workStatus;
                    sf.isOnTrial = staff.isOnTrial;
                    sf.trialOverAt = staff.trialOverAt;
                    sf.staffTypeId = staff.staffTypeId;
                    sf.degreeTypeId = staff.degreeTypeId;
                    sf.speciality = staff.speciality;
                    sf.residenceAddress = staff.residenceAddress;
                    sf.address = staff.address;
                    sf.id = staff.id;
                    sf.phone = staff.phone;
                    sf.contactName = staff.contactName;
                    sf.contactPhone = staff.contactPhone;
                    sf.contactFamilyMemberType = staff.contactFamilyMemberType;
                    sf.domicile = staff.domicile;
                    sf.residenceType = staff.residenceType;
                    sf.insureTypeId = staff.insureTypeId;
                    sf.isPayCPF = staff.isPayCPF;
                    sf.contractExpireAt = staff.contractExpireAt;
                    sf.contractExpireStr = staff.contractExpireStr;
                    sf.contractCount = staff.contractCount;
                    sf.totalSeniority = staff.totalSeniority;
                    sf.remark = staff.remark;
                    sf.ethnic = staff.ethnic;
                    sf.photo = staff.photo;
                    sf.workingYearsAt = staff.workingYearsAt;
                    sf.resignAt = staff.resignAt;
                    sf.jobTitleId = staff.jobTitleId;
                    sf.companyId = staff.companyId;
                    sf.departmentId = staff.departmentId;

                    this.context.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Sf_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                string propertyName = e.PropertyName;
                if ((!string.IsNullOrEmpty(propertyName)) && Staff.ValueName.ContainsKey(propertyName))
                {
                    Staff s = sender as Staff;
                    object oldOValue = s.GetType().GetProperty(e.PropertyName + "_Was").GetValue(s, null);
                    string oldValue = oldOValue == null ? "" : oldOValue.ToString();
                    object newOValue = s.GetType().GetProperty(e.PropertyName).GetValue(s, null);
                    string newValue = newOValue == null ? "" : newOValue.ToString();

                    IMessageRecordService mrs = new MessageRecordService(this.context.Connection.ConnectionString);
                    mrs.CreateStaffBasicEdited(s.nr, s.OperatorId, Staff.ValueName[propertyName], oldValue, newValue);
                }
            }
            catch { }
        }

        //functions for validation

        public List<Staff> FindByJobTitleId(int id)
        {
            return this.context.GetTable<Staff>().Where(p => p.jobTitleId.Equals(id)).ToList();
        }

        public List<Staff> FindByStaffType(int id)
        {
            return this.context.GetTable<Staff>().Where(p => p.staffTypeId.Equals(id)).ToList();
        }

        public List<Staff> FindByDegreeType(int id)
        {
            return this.context.GetTable<Staff>().Where(p => p.degreeTypeId.Equals(id)).ToList();
        }

        public List<Staff> FindByInsureType(int id)
        {
            return this.context.GetTable<Staff>().Where(p => p.insureTypeId.Equals(id)).ToList();
        }

        public bool IsStaffExist(string nr)
        {
            try
            {
                var Staff = this.context.GetTable<Staff>().FirstOrDefault(p => p.nr.Equals(nr));
                if (Staff != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Staff> FindByCompanyAndDepartment(int companyId, int? departmentId)
        {
            try
            {
                IQueryable<Staff> staff;

                staff = this.context.GetTable<Staff>().Where(c => c.companyId.Equals(companyId));

                if (departmentId.HasValue)
                {
                    staff = staff.Where(c => c.departmentId.Equals(departmentId));
                }

                if (staff != null)
                {
                    return staff.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Staff> GetAllTableName()
        {
            try
            {
                return (this.context.GetTable<Staff>()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<Staff> AdvancedSearch(string AllTableName, string SearchConditions, string SearchValueFirst, string SearchValueSecond)
        {
            string strWhere = string.Empty;

            try
            {
                if (SearchValueSecond != string.Empty)
                {
                    strWhere += SearchConditionsHelper.GetStrWhere("Staff", AllTableName, SearchConditions, SearchValueFirst);
                    strWhere += SearchConditionsHelper.GetStrWhere("Staff", AllTableName, SearchConditions, SearchValueSecond);
                }
                else
                {
                    strWhere = SearchConditionsHelper.GetStrWhere("Staff", AllTableName, SearchConditions, SearchValueFirst);
                }
                var q = this.context.CreateQuery<Staff>(strWhere);
                return q;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int CountStaff(int workStatus)
        {
            throw new NotImplementedException();
        }

        public List<Staff> SearchOnTrialStaff(StaffSearchModel q)
        {
            List<Staff> staffs = this.context.Staffs.Where(s => s.isOnTrial == true).ToList(); ;
            //IQueryable<MessageRecord> messages = this.context.MessageRecord.Where(m => m.messageType == 201);
            //foreach (var a in messages.ToList())
            //{
            //    staffs.Add(FindByNr(a.staffNr));
            //}
            return staffs;
        }

        public List<Staff> StaffBirthday()
        {
            string a = System.DateTime.Today.ToString("M/d");
            List<Staff> result = new List<Staff>();
            //var q = this.context.Staffs.Where(s => s.birthday.ToString().Contains(a)).Count();
            var q = this.context.Staffs.ToList();
            foreach (var i in q)
            {
                string show = i.birthday.ToString();
                if (show.Contains(a)) result.Add(i);
            }
            return result;
        }

        public Dictionary<string, string> StaffCount()
        {
            Dictionary<string, string> Result = new Dictionary<string, string>();

            Result.Add("试用期员工", countStaffOntrail().ToString());
            Result.Add("正式员工", countStaffOn().ToString());
            Result.Add("离职员工", countStaffOff().ToString());

            return Result;
        }

        private int countStaffOntrail()
        {
            int q = this.context.Staffs.Where(s => s.isOnTrial).Count();
            return q;
        }

        private int countStaffOff()
        {
            int q = this.context.Staffs.Where(s => s.workStatus == 200).Count();
            return q;
        }

        private int countStaffOn()
        {
            int q = this.context.Staffs.Where(s => s.workStatus == 100).Where(s => s.isOnTrial == false).Count();
            return q;
        }

        public List<Staff> ContractExpiredDetail(int v)
        {
            try
            {
                //var q = this.context.Staffs.Where(s => s.birthday.ToString().Contains(a)).Count();
                if (v != -1)
                {
                    string a = System.DateTime.Today.AddMonths(v).ToString("yyyyMM");
                    var q = this.context.Staffs.ToList();
                    List<Staff> count = new List<Staff>();
                    foreach (var i in q)
                    {
                        if (i.contractExpireStr == null ? false : i.contractExpireStr.Length >= 6)
                        {
                            string show = i.contractExpireStr.Substring(0, 6);
                            if (show == a) count.Add(i);
                        }
                    }
                    return count;
                }
                else
                {
                    string a = System.DateTime.Today.AddMonths(v).ToString("yyyyMM");
                    var q = this.context.Staffs.Where(s => s.workStatus == 100).ToList();
                    List<Staff> count = new List<Staff>();
                    foreach (var i in q)
                    {
                        if (i.contractExpireStr == null ? false : i.contractExpireStr.Length >= 6)
                        {
                            string show = i.contractExpireStr.Substring(0, 6);
                            if (show == a) count.Add(i);
                        }
                    }
                    return count;
                }
                //switch (v)
                //{
                //    case 0:
                //        {
                //            string a = System.DateTime.Today.ToString("yyyyMM");
                //            foreach (var i in q)
                //            {
                //                if (i.contractExpireStr==null? false :i.contractExpireStr.Length>=6)
                //                {
                //                    string show = i.contractExpireStr.Substring(0, 6);
                //                    if (show == a) count.Add(i);
                //                }
                //            }
                //            return count;
                //        }
                //    case 1:
                //        {
                //            string a = System.DateTime.Today.AddMonths(1).ToString("yyyyMM");
                //            foreach (var i in q)
                //            {
                //                if (i.contractExpireStr == null ? false : i.contractExpireStr.Length >= 6)
                //                {
                //                    string show = i.contractExpireStr.Substring(0, 6);
                //                    if (show == a) count.Add(i);
                //                }
                //            }
                //            return count;
                //        }
                //    case 2:
                //        {
                //            string a = System.DateTime.Today.ToString("yyyyMM");
                //            foreach (var i in q)
                //            {
                //                if (i.contractExpireStr == null ? false : i.contractExpireStr.Length >= 6)
                //                {
                //                    string show = i.contractExpireStr.Substring(4, 1);
                //                    if (show == a) count.Add(i);
                //                }
                //            }
                //            return count;
                //        }
                //}
            }
            catch
            {
            return null;
            }
        }

        public List<Staff> ToEmployeesDetail(int v)
        {
            throw new NotImplementedException();
        }
    }
}
