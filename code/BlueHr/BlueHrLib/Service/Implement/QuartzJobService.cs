using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.MQTask;
using BlueHrLib.CusException;
using BlueHrLib.Data.Enum;

namespace BlueHrLib.Service.Implement
{
    public class QuartzJobService : ServiceBase, IQuartzJobService
    {
        public QuartzJobService(string dbString) : base(dbString) { }

        public bool Create(QuartzJob job)
        {
            DataContext dc = new DataContext(this.DbString);
            dc.Context.GetTable<QuartzJob>().InsertOnSubmit(job);
            dc.Context.SubmitChanges();
            return true;
        }

        public bool Delete(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            QuartzJob job = dc.Context.GetTable<QuartzJob>().FirstOrDefault(s => s.id.Equals(id));
            if (job!=null) {
                dc.Context.GetTable<QuartzJob>().DeleteOnSubmit(job);
                dc.Context.SubmitChanges();
            }
            return true;
        }

        public List<QuartzJob> GetByType(CronJobType type)
        {
            DataContext dc = new DataContext(this.DbString);
            return dc.Context.GetTable<QuartzJob>().Where(s => s.jobType.Equals((int)type)).ToList();
        }
    }
}
