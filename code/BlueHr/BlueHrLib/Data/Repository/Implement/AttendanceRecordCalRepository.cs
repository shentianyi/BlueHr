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

        public bool Create(AttendanceRecordCal attendanceRecordCal)
        {
            try
            {
                attendanceRecordCal.oriWorkingHour = attendanceRecordCal.actWorkingHour;
                attendanceRecordCal.createdAt = DateTime.Now;
                attendanceRecordCal.isManualCal = true;
                attendanceRecordCal.isException = false;

                //做判断， 如果 加班时间为空， 则默认填写0
                if (string.IsNullOrWhiteSpace(attendanceRecordCal.actExtraWorkingHour.ToString()))
                {
                    attendanceRecordCal.actExtraWorkingHour = 0;
                }

                attendanceRecordCal.oriExtraWorkingHour = attendanceRecordCal.actExtraWorkingHour;

                this.context.GetTable<AttendanceRecordCal>().InsertOnSubmit(attendanceRecordCal);
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
            AttendanceRecordCal arc = this.context.GetTable<AttendanceRecordCal>().FirstOrDefault(c => c.id.Equals(id));

            if (arc != null)
            {
                this.context.GetTable<AttendanceRecordCal>().DeleteOnSubmit(arc);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public AttendanceRecordCal FindById(int id)
        {
            return this.context.GetTable<AttendanceRecordCal>().FirstOrDefault(s => s.id.Equals(id));
        }
    }
}
