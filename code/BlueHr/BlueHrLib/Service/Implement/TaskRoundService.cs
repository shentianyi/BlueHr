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
    public class TaskRoundService : ServiceBase, ITaskRoundService
    {
        public TaskRoundService(string dbString) : base(dbString) { }

        public TaskRound Create(TaskType type)
        {
            DataContext dc = new DataContext(this.DbString);
            TaskRound tr = new TaskRound()
            {
                createdAt = DateTime.Now,
                taskStatus = (int)TaskRoundStatus.Waiting,
                taskType = (int)type,
                uuid = Guid.NewGuid()
            };
            dc.Context.GetTable<TaskRound>().InsertOnSubmit(tr);
            dc.Context.SubmitChanges();
            return tr;
        }

        public void FinishTaskByUniqId(Guid uuid,string result, bool error = false)
        {
            DataContext dc = new DataContext(this.DbString);
            TaskRound tr = dc.Context.GetTable<TaskRound>().Where(s => s.uuid.Equals(uuid)).FirstOrDefault();
            if (tr == null)
            {
                throw new TaskRoundNotFoundException();
            }

            tr.taskStatus = error ? (int) TaskRoundStatus.Cancel : (int)TaskRoundStatus.Finish;
            tr.finishAt = DateTime.Now;
            tr.result = result;

            dc.Context.SubmitChanges();
        }

        public IQueryable<TaskRound> List()
        {
            ITaskRoundRepository rep = new TaskRoundRepository(new DataContext(this.DbString));
            return rep.List();
        }
    }
}
