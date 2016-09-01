using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Implement
{
    public class AttendanceRecordDetailViewRepository : RepositoryBase<Company>, IAttendanceRecordDetailViewRepository
    {
        private BlueHrDataContext context;

        public AttendanceRecordDetailViewRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public IQueryable<AttendanceRecordDetailView> Search(AttendanceRecordDetailSearchModel searchModel)
        {
            IQueryable<AttendanceRecordDetailView> q = this.context.AttendanceRecordDetailView;
            if (!string.IsNullOrEmpty(searchModel.StaffNr))
            {
                q = q.Where(s => s.staffNr.Contains(searchModel.StaffNr));
            }

            if (!string.IsNullOrEmpty(searchModel.StaffNrAct))
            {
                q = q.Where(s => s.staffNr.Equals(searchModel.StaffNrAct));
            }

            if (searchModel.companyId.HasValue)
            {
                q = q.Where(s => s.companyId.Equals(searchModel.companyId.Value));
            }

            
            if (searchModel.departmentId.HasValue)
            {
                q = q.Where(s => s.departmentId.Equals(searchModel.departmentId.Value));
            }

            if (searchModel.RecordAtFrom.HasValue)
            {
                q = q.Where(s => s.recordAt >= searchModel.RecordAtFrom);
            }

            if (searchModel.RecordAtEnd.HasValue)
            {
                q = q.Where(s => s.recordAt <= searchModel.RecordAtEnd);
            }
            // AS [t0] ORDER BY [t0].[recordAt] DESC, [t0].[staffNr]
            //return q.OrderBy(s => s.staffNr).OrderByDescending(s => s.recordAt);

            //   AS[t0] ORDER BY[t0].[staffNr], [t0].[recordAt] DESC
            // 因为建立了staffnr asc + recordat desc 的索引
            return q.OrderByDescending(s => s.recordAt).OrderBy(s => s.staffNr);
        }
    }
}
