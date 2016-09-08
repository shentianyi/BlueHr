using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Implement
{
    public class AttendanceRecordCalRepository : RepositoryBase<AttendanceRecordDetail>, IAttendanceRecordCalRepository
    {
        private BlueHrDataContext context;

        public AttendanceRecordCalRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public AttendanceRecordCal FindById(int id)
        {
            return this.context.GetTable<AttendanceRecordCal>().FirstOrDefault(s => s.id.Equals(id));
        }
    }
}
