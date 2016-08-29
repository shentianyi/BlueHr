using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Implement
{
    public class AttendanceRecordDetailRepository : RepositoryBase<Company>, IAttendanceRecordDetailRepository
    {
        private BlueHrDataContext context;

        public AttendanceRecordDetailRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public IQueryable<AttendanceRecordDetail> Search(AttendanceRecordDetailSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
