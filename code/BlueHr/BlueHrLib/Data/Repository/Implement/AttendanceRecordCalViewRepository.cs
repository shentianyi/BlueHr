using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Implement
{
    public class AttendanceRecordCalViewRepository : RepositoryBase<AttendanceRecordCalView>, IAttendanceRecordCalViewRepository
    {
        private BlueHrDataContext context;

        public AttendanceRecordCalViewRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public AttendanceRecordCalView FindById(int id)
        {
            return this.context.GetTable<AttendanceRecordCalView>().FirstOrDefault(s => s.id.Equals(id));
        }

        public IQueryable<AttendanceRecordCalView> Search(AttendanceRecordCalSearchModel searchModel)
        {
            IQueryable<AttendanceRecordCalView> q = this.context.AttendanceRecordCalView;
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

            if (searchModel.AttendanceDateFrom.HasValue)
            {
                q = q.Where(s => s.attendanceDate >= searchModel.AttendanceDateFrom);
            }

            if (searchModel.AttendanceDateEnd.HasValue)
            {
                q = q.Where(s => s.attendanceDate <= searchModel.AttendanceDateEnd);
            }

            if (searchModel.IsException.HasValue)
            {
                q = q.Where(s => s.isException.Equals(searchModel.IsException.Value));
            }

            if (searchModel.IsExceptionHandled.HasValue)
            {
                q = q.Where(s => s.isExceptionHandled.Equals(searchModel.IsExceptionHandled.Value));
            }
            // AS [t0] ORDER BY [t0].[recordAt] DESC, [t0].[attendanceDate]
            //return q.OrderBy(s => s.staffNr).OrderByDescending(s => s.attendanceDate);

            //   AS[t0] ORDER BY[t0].[staffNr], [t0].[attendanceDate] DESC
            // 因为建立了staffnr asc + recordat desc 的索引
            return q.OrderByDescending(s => s.attendanceDate).OrderBy(s => s.staffNr);
        }
    }
}
