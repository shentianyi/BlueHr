using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.CusException;
using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Service.Interface;

namespace BlueHrLib.Service.Implement
{
    public class WorkAndRestService : ServiceBase, IWorkAndRestService
    {
        public WorkAndRestService(string dbString) : base(dbString) { }

        public WorkAndRest FindByDate(DateTime datetime)
        {
            DataContext dc = new DataContext(this.DbString);
            WorkAndRest wr = dc.Context.GetTable<WorkAndRest>().FirstOrDefault(s => s.dateAt.Equals(datetime.Date));
            if (wr == null)
                throw new WorkAndRestNotFoundException();
            return wr;
        }

        public bool IsRestDay(WorkAndRest wr)
        {
            return !IsWorkDay(wr);
        }

        public bool IsRestDay(DateTime datetime)
        {
            return !IsWorkDay(datetime);
        }

        public bool IsWorkDay(WorkAndRest wr)
        {
            return wr.dateType == (int)WorkAndRestType.WorkDay;
        }

        public bool IsWorkDay(DateTime datetime)
        {
            WorkAndRest wr = FindByDate(datetime.Date);
            return IsWorkDay(wr);
        }
    }
}
