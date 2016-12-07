using System;
using System.Linq;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System.Collections.Generic;
using BlueHrLib.Helper;
using ALinq.Dynamic;

namespace BlueHrLib.Data.Repository.Implement
{
    public class WorkAndRestRepository : RepositoryBase<JobTitle>, IWorkAndRestRepository
    {
        private BlueHrDataContext context;

        public WorkAndRestRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public bool Create(WorkAndRest ws)
        {
            try
            {
                //判断 日期是否已经存在，已经存在则不添加
                bool IsExist = HasDateAtExist(ws.dateAt);

                if (IsExist)
                {
                    this.context.GetTable<WorkAndRest>().InsertOnSubmit(ws);
                    this.context.SubmitChanges();
                    return true;
                }else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<WorkAndRest> Search(WorkAndRestSearchModel searchModel)
        {
            IQueryable<WorkAndRest> workAndRests = this.context.WorkAndRest;

            if (searchModel.DateAtFrom.HasValue)
            {
                workAndRests = workAndRests.Where(c => c.dateAt >= searchModel.DateAtFrom);
            }

            if (searchModel.DateAtTo.HasValue)
            {
                workAndRests = workAndRests.Where(c => c.dateAt <= searchModel.DateAtTo);
            }

            if (searchModel.DateType.HasValue)
            {
                workAndRests = workAndRests.Where(c => c.dateType.Equals(searchModel.DateType));
            }

            return workAndRests.OrderBy(c => c.dateAt);
        }

        public bool HasDateAtExist(DateTime? datetime)
        {
            if (datetime.HasValue) { 
                try
                {
                    var workAndRest = this.context.GetTable<WorkAndRest>().FirstOrDefault(c => c.dateAt.Equals(datetime));

                    if (workAndRest == null)
                    {
                        // 数据库中不存在， 可以添加
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }else
            {
                return false;
            }
        }

        public bool Update(WorkAndRest ws)
        {
            WorkAndRest war = this.context.GetTable<WorkAndRest>().FirstOrDefault(c => c.id.Equals(ws.id));

            if (war != null)
            {
                war.dateAt = ws.dateAt;
                war.dateType = ws.dateType;
                war.remark = ws.remark;

                try
                {
                    this.context.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public WorkAndRest FindById(int id)
        {
            WorkAndRest war = this.context.GetTable<WorkAndRest>().FirstOrDefault(c => c.id.Equals(id));
            return war;
        }

        public bool DeleteById(int id)
        {
            WorkAndRest war = this.context.GetTable<WorkAndRest>().FirstOrDefault(c => c.id.Equals(id));

            if (war != null)
            {
                this.context.GetTable<WorkAndRest>().DeleteOnSubmit(war);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<WorkAndRest> GetAllTableName()
        {
            try
            {
                return (this.context.GetTable<WorkAndRest>()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<WorkAndRest> AdvancedSearch(string AllTableName, string SearchConditions, string SearchValueFirst)
        {
            string strWhere = string.Empty;

            try
            {
                strWhere = SearchConditionsHelper.GetStrWhere("WorkAndRest", AllTableName, SearchConditions, SearchValueFirst);
                var q = this.context.CreateQuery<WorkAndRest>(strWhere);
                return q;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
