using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{

    public class ShiftScheduleRepository : RepositoryBase<ShiftSchedule>, IShiftScheduleRepository
    {
        private BlueHrDataContext context;

        public ShiftScheduleRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<ShiftSchedule> Search(ShiftScheduleSearchModel searchModel)
        {
            //TODO
            IQueryable<ShiftSchedule> q = this.context.ShiftSchedule;
            if (!string.IsNullOrEmpty(searchModel.StaffNr))
            {
                q = q.Where(c => c.staffNr.Contains(searchModel.StaffNr.Trim()));
            }
            if (!string.IsNullOrEmpty(searchModel.StaffNrAct))
            {
                q = q.Where(c => c.staffNr.Equals(searchModel.StaffNrAct));
            }
            if (searchModel.ScheduleAtFrom.HasValue)
            {
                q = q.Where(s => s.scheduleAt >= searchModel.ScheduleAtFrom.Value);
            }


            if (searchModel.ScheduleAtEnd.HasValue)
            {
                q = q.Where(s => s.scheduleAt <= searchModel.ScheduleAtEnd.Value);
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

                    p.departId.Split(',').ToList().ForEach(pp =>
                    {
                        if (!string.IsNullOrEmpty(pp))
                        {
                            depIds.Add(pp);
                        }
                    });
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


            return q.OrderByDescending(s => s.scheduleAt);
        }

        public bool Create(ShiftSchedule parModel)
        {
            try
            {
                this.context.GetTable<ShiftSchedule>().InsertOnSubmit(parModel);
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
            var dep = this.context.GetTable<ShiftSchedule>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<ShiftSchedule>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ShiftSchedule FindById(int id)
        {
            return this.context.GetTable<ShiftSchedule>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(ShiftSchedule parModel)
        {
            var dep = this.context.GetTable<ShiftSchedule>().FirstOrDefault(c => c.id.Equals(parModel.id));
            if (dep != null)
            {
                dep.shiftId = parModel.shiftId;
                dep.staffNr = parModel.staffNr;
                dep.scheduleAt = parModel.scheduleAt;

                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //根据班次或取排班信息
        public ShiftSchedule FindShiftScheduleByShiftId(int id)
        {
            return this.context.GetTable<ShiftSchedule>().FirstOrDefault(c => c.shiftId.Equals(id));
        }

        public bool EasyCreate(ShiftSchedule parModel, DateTime startTime, DateTime endTime)
        {
            try
            {
                DateTime startTimeSet = startTime;
                int i = startTime.Day;
                for (; i <= endTime.Day; i++)
                {
                    ShiftSchedule current = new ShiftSchedule();
                    int id = parModel.id;
                    current.id = id;
                    int shiftId = parModel.shiftId;
                    current.shiftId = shiftId;
                    string staffNr = parModel.staffNr;
                    current.staffNr = staffNr;
                    parModel.scheduleAt = startTimeSet;
                    DateTime scheduleAt = parModel.scheduleAt;
                    current.scheduleAt = scheduleAt;
                    if (!IsDup(current))
                    {
                        this.context.GetTable<ShiftSchedule>().InsertOnSubmit(current);
                        this.context.SubmitChanges();
                    }
                    startTimeSet=startTimeSet.AddDays(1);
                    parModel.id= parModel.id+1;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool IsDup(ShiftSchedule current)
        {
            var q = this.context.GetTable<ShiftSchedule>().Where(s => s.scheduleAt.Equals(current.scheduleAt) && s.staffNr.Equals(current.staffNr) && s.shiftId.Equals(current.shiftId));
            if (current.id > 0)
            {
                q = q.Where(s => s.id != current.id);
            }


            var m = q.FirstOrDefault();

            return m != null;
        }

        public List<ShiftScheduleView> GetAllShiftSchedule()
        {
            return this.context.GetTable<ShiftScheduleView>().ToList();
        }

        public ShiftSchedule FindForEdit(string staffNr, DateTime scheduleAt, int shiftId)
        {
            return this.context.GetTable<ShiftSchedule>().FirstOrDefault(s => s.scheduleAt.Equals(scheduleAt) && s.staffNr.Equals(staffNr) && s.shiftId.Equals(shiftId));
        }
    }
}
