using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Data.Repository.Implement
{
    public class TaskRoundRepository : RepositoryBase<TaskRound>, ITaskRoundRepository
    {
        private BlueHrDataContext context;

        public TaskRoundRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<TaskRound> List()
        {
            return this.context.GetTable<TaskRound>().OrderByDescending(s => s.createdAt);
        }

        //public void Create(TaskRound tr)
        //{
        //    this.context.GetTable<TaskRound>().InsertOnSubmit(tr);
        //    this.context.SubmitChanges();
        //}

    }
}
