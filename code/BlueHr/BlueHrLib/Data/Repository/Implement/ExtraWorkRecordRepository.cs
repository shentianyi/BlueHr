using ALinq.Dynamic;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class ExtraWorkRecordRepository : RepositoryBase<ExtraWorkRecord>, IExtraWorkRecordRepository
    {
        private BlueHrDataContext context;

        public ExtraWorkRecordRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<ExtraWorkRecord> Search(ExtraWorkRecordSearchModel searchModel)
        {
            IQueryable<ExtraWorkRecord> q = this.context.ExtraWorkRecord; 

            if (!string.IsNullOrEmpty(searchModel.staffNr))
            {
                q = q.Where(c => c.staffNr.Contains(searchModel.staffNr.Trim()));
            }

            if (!string.IsNullOrEmpty(searchModel.extraWorkTypeId))
            {
                q = q.Where(c => c.extraWorkTypeId.Equals(searchModel.extraWorkTypeId));
            }

            if (searchModel.durStart.HasValue)
            {
                q = q.Where(s => s.otTime >= searchModel.durStart.Value);
            }


            if (searchModel.durEnd.HasValue)
            {
                q = q.Where(s => s.otTime <= searchModel.durEnd.Value);
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


            return q.OrderByDescending(s => s.otTime);
        }

        public bool Create(ExtraWorkRecord parModel)
        {
            try
            {
                this.context.GetTable<ExtraWorkRecord>().InsertOnSubmit(parModel);
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
            var dep = this.context.GetTable<ExtraWorkRecord>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<ExtraWorkRecord>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ExtraWorkRecord FindById(int id)
        {
            return this.context.GetTable<ExtraWorkRecord>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(ExtraWorkRecord parModel)
        {
            var dep = this.context.GetTable<ExtraWorkRecord>().FirstOrDefault(c => c.id.Equals(parModel.id));
            if (dep != null)
            {
                dep.extraWorkTypeId = parModel.extraWorkTypeId;
                dep.staffNr = parModel.staffNr;
                dep.duration = parModel.duration;
                dep.durationType = parModel.durationType;
                dep.otReason = parModel.otReason;
                dep.startHour = parModel.startHour;
                dep.endHour = parModel.endHour;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //审批
        public bool ApprovalTheRecord(ExtraWorkRecordApproval extralApproval)
        {
            try
            {
                this.context.GetTable<ExtraWorkRecordApproval>().InsertOnSubmit(extralApproval);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ExtraWorkRecord> GetAllTableName()
        {
            try
            {
                return (this.context.GetTable<ExtraWorkRecord>()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<ExtraWorkRecordView> ExtraWorkViewSearch(ExtraWorkRecordSearchModel searchModel)
        {
            IQueryable<ExtraWorkRecordView> q = this.context.ExtraWorkRecordView;

            if (!string.IsNullOrEmpty(searchModel.staffNr))
            {
                q = q.Where(c => c.staffNr.Contains(searchModel.staffNr.Trim()));
            }

            if (!string.IsNullOrEmpty(searchModel.extraWorkTypeId))
            {
                q = q.Where(c => c.extraWorkTypeId.Equals(searchModel.extraWorkTypeId));
            }

            if (searchModel.durStart.HasValue)
            {
                q = q.Where(s => s.otTime >= searchModel.durStart.Value);
            }


            if (searchModel.durEnd.HasValue)
            {
                q = q.Where(s => s.otTime <= searchModel.durEnd.Value);
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


            return q.OrderByDescending(s => s.otTime);
        }

        public IQueryable<ExtraWorkRecordView> AdvancedSearch(string v1, string v2, string v3)
        {
            string strWhere = string.Empty;

            try
            {
                strWhere = SearchConditionsHelper.GetStrWhere("ExtraWorkRecordView", v1, v2, v3);
                var q = this.context.CreateQuery<ExtraWorkRecordView>(strWhere);
                return q;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
