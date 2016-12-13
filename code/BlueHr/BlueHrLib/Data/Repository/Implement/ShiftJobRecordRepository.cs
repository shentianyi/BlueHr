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
   
    public class ShiftJobRecordRepository : RepositoryBase<ShiftJobRecord>, IShiftJobRecordRepository
    {
        private BlueHrDataContext context;

        public ShiftJobRecordRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public IQueryable<ShiftJobRecord> AdvancedSearch(string v1, string v2, string v3)
        {
            string strWhere = string.Empty;

            try
            {
                strWhere = SearchConditionsHelper.GetStrWhere("ShiftJobRecord", v1, v2, v3);
                var q = this.context.CreateQuery<ShiftJobRecord>(strWhere);
                return q;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Create(ShiftJobRecord rsr)
        {
            try
            {
                this.context.GetTable<ShiftJobRecord>().InsertOnSubmit(rsr);
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
            ShiftJobRecord cp = this.context.GetTable<ShiftJobRecord>().FirstOrDefault(c => c.id.Equals(id));

            if (cp != null)
            {
                this.context.GetTable<ShiftJobRecord>().DeleteOnSubmit(cp);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ShiftJobRecord FindById(int id)
        {
            ShiftJobRecord cp = this.context.GetTable<ShiftJobRecord>().FirstOrDefault(c => c.id.Equals(id));
            return cp;
        }

        public IQueryable<ShiftJobRecord> Search(ShiftJobRecordSearchModel searchModel)
        {
            IQueryable<ShiftJobRecord> ShiftJobRecords = this.context.ShiftJobRecord;
            if (!string.IsNullOrEmpty(searchModel.staffNr))
            {
                ShiftJobRecords = ShiftJobRecords.Where(c => c.staffNr.Contains(searchModel.staffNr.Trim()));
            }
            return ShiftJobRecords;
        }

        public bool Update(ShiftJobRecord lr)
        {
            ShiftJobRecord updatedlr = this.context.GetTable<ShiftJobRecord>().FirstOrDefault(c => c.id.Equals(lr.id));

            if (updatedlr != null)
            {
                updatedlr.staffNr = lr.staffNr == null ? updatedlr.staffNr : lr.staffNr;
                updatedlr.remark = lr.remark == null ? updatedlr.remark : lr.remark;
                updatedlr.approvalUserId = lr.approvalUserId == null ? updatedlr.approvalUserId : lr.approvalUserId;
                updatedlr.approvalAt = lr.approvalAt == null ? updatedlr.approvalAt : lr.approvalAt;
                updatedlr.approvalStatus = lr.approvalStatus == null ? updatedlr.approvalStatus : lr.approvalStatus;
                updatedlr.approvalRemark = lr.approvalRemark == null ? updatedlr.approvalRemark : lr.approvalRemark;
                updatedlr.afterCompanyId = lr.afterCompanyId == null ? updatedlr.afterCompanyId : lr.afterCompanyId;
                updatedlr.afterDepartmentId = lr.afterDepartmentId == null ? updatedlr.afterDepartmentId : lr.afterDepartmentId;
                updatedlr.afterJobId = lr.afterJobId == null ? updatedlr.afterJobId : lr.afterJobId;
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