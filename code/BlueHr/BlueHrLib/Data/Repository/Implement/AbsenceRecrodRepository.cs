using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class AbsenceRecrodRepository : RepositoryBase<AbsenceRecrod>, IAbsenceRecrodRepository
    {
        private BlueHrDataContext context;

        public AbsenceRecrodRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<AbsenceRecrod> Search(AbsenceRecrodSearchModel searchModel)
        {
            IQueryable<AbsenceRecrod> q = this.context.AbsenceRecrods;
            if (!string.IsNullOrEmpty(searchModel.staffNr))
            {
                q = q.Where(c => c.staffNr.Contains(searchModel.staffNr.Trim()));
            }
            if (!string.IsNullOrEmpty(searchModel.absenceTypeId))
            {
                q = q.Where(c => c.absenceTypeId.Equals(searchModel.absenceTypeId));
            }
            if (searchModel.durStart.HasValue)
            {
                q = q.Where(s => s.absenceDate >= searchModel.durStart.Value);
            }


            if (searchModel.durEnd.HasValue)
            {
                q = q.Where(s => s.absenceDate <= searchModel.durEnd.Value);
            }

            //在员工管理-员工列表、排班管理-排班管理、缺勤管理、加班管理的列表中，用户如果有权限查看列表，那么只可以查看他所管理部门中的所有员工(员工中已有部门、公司)
            if (searchModel.lgUser != null)
            {
                User lgUser = searchModel.lgUser;

                List<SysUserDataAuth> allDataAuths = this.context.GetTable<SysUserDataAuth>().Where(p => p.userId == lgUser.id).ToList();

                List<string> cmpIds = new List<string>();
                List<string> depIds = new List<string>();

                allDataAuths.ForEach(p =>
                {
                    if (!cmpIds.Contains(p.cmpId.ToString()))
                    {
                        cmpIds.Add(p.cmpId.ToString());
                    }

                    if (!depIds.Contains(p.departId.ToString()))
                    {
                        depIds.Add(p.departId.ToString());
                    }
                });

                IQueryable<Staff> staffs = this.context.Staffs;

                if (cmpIds.Count > 0)
                {
                    staffs = staffs.Where(c => cmpIds.Contains(c.companyId.ToString()));
                }

                if (depIds.Count > 0)
                {
                    staffs = staffs.Where(c => depIds.Contains(c.departmentId.ToString()));
                }

                List<string> staffNrs = new List<string>();

                staffs.ToList().ForEach(p =>
                {
                    if (!staffNrs.Contains(p.nr))
                    {
                        staffNrs.Add(p.nr);
                    }
                });

                //满足cmpid 和departmentid 的所有员工
                if (staffs.Count() > 0)
                {
                    q = q.Where(s => staffNrs.Contains(s.staffNr));
                }
            }


            return q.OrderByDescending(s => s.absenceDate);
        }

        public bool Create(AbsenceRecrod absRecord)
        {
            try
            {
                this.context.GetTable<AbsenceRecrod>().InsertOnSubmit(absRecord);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            var dep = this.context.GetTable<AbsenceRecrod>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<AbsenceRecrod>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public AbsenceRecrod FindById(int id)
        {
            return this.context.GetTable<AbsenceRecrod>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(AbsenceRecrod absRecord)
        {
            var dep = this.context.GetTable<AbsenceRecrod>().FirstOrDefault(c => c.id.Equals(absRecord.id));
            if (dep != null)
            {
                dep.absenceTypeId = absRecord.absenceTypeId;
                dep.duration = absRecord.duration;
                dep.durationType = absRecord.durationType;
                dep.remark = absRecord.remark;
                dep.staffNr = absRecord.staffNr;
                dep.absenceDate = absRecord.absenceDate;
                dep.startHour = absRecord.startHour;
                dep.endHour = absRecord.endHour;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<AbsenceRecrod> FindByAbsenceType(int id)
        {
            return this.context.GetTable<AbsenceRecrod>().Where(p => p.absenceTypeId.Equals(id)).ToList();
        }

        public List<AbsenceRecrod> GetAll()
        {
            return this.context.GetTable<AbsenceRecrod>().ToList();
        }

        //审批
        public bool ApprovalTheRecord(AbsenceRecordApproval absRecordApproval)
        {
            try
            {
                this.context.GetTable<AbsenceRecordApproval>().InsertOnSubmit(absRecordApproval);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
