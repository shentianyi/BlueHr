using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
   
    public class LeaveRecordRepository : RepositoryBase<LeaveRecord>, ILeaveRecordRepository
    {
        private BlueHrDataContext context;

        public LeaveRecordRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public bool Create(LeaveRecord rsr)
        {
            try
            {
                this.context.GetTable<LeaveRecord>().InsertOnSubmit(rsr);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            LeaveRecord cp = this.context.GetTable<LeaveRecord>().FirstOrDefault(c => c.id.Equals(id));

            if (cp != null)
            {
                this.context.GetTable<LeaveRecord>().DeleteOnSubmit(cp);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public LeaveRecord FindById(int id)
        {
            LeaveRecord cp = this.context.GetTable<LeaveRecord>().FirstOrDefault(c => c.id.Equals(id));
            return cp;
        }

        public IQueryable<LeaveRecord> Search(LeaveRecordSearchModel searchModel)
        {
            IQueryable<LeaveRecord> leaveRecords = this.context.LeaveRecord;
            if (!string.IsNullOrEmpty(searchModel.staffNr))
            {
                leaveRecords = leaveRecords.Where(c => c.staffNr.Contains(searchModel.staffNr.Trim()));
            }
            return leaveRecords;
        }

        public bool Update(LeaveRecord lr)
        {
            LeaveRecord updatedlr = this.context.GetTable<LeaveRecord>().FirstOrDefault(c => c.id.Equals(lr.id));

            if (updatedlr != null)
            {
                updatedlr.staffNr = lr.staffNr == null ? updatedlr.staffNr : lr.staffNr;
                updatedlr.leaveStart = lr.leaveStart == null ? updatedlr.leaveStart : lr.leaveStart;
                updatedlr.leaveEnd = lr.leaveEnd == null ? updatedlr.leaveEnd : lr.leaveEnd;
                updatedlr.remark = lr.remark == null ? updatedlr.remark : lr.remark;
                updatedlr.approvalUserId = lr.approvalUserId == null ? updatedlr.approvalUserId : lr.approvalUserId;
                updatedlr.approvalAt = lr.approvalAt == null ? updatedlr.approvalAt : lr.approvalAt;
                updatedlr.approvalStatus = lr.approvalStatus == null ? updatedlr.approvalStatus : lr.approvalStatus;
                updatedlr.approvalRemark = lr.approvalRemark == null ? updatedlr.approvalRemark : lr.approvalRemark;
                updatedlr.isDelete = lr.isDelete == null ? updatedlr.isDelete : lr.isDelete;
               

                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}