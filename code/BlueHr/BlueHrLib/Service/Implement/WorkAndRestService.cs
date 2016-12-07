using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.CusException;
using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Interface;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;

namespace BlueHrLib.Service.Implement
{
    public class WorkAndRestService : ServiceBase, IWorkAndRestService
    {
        private IWorkAndRestRepository workAndRestRep;

        public WorkAndRestService(string dbString) : base(dbString) {
            workAndRestRep = new WorkAndRestRepository(this.Context);
        }

        public IQueryable<WorkAndRest> AdvancedSearch(string allTableName, string searchConditions, string searchValueFirst)
        {
            return workAndRestRep.AdvancedSearch(allTableName, searchConditions, searchValueFirst);

        }

        public bool Create(WorkAndRest wr)
        {
            return workAndRestRep.Create(wr);
        }

        public bool DeleteById(int id)
        {
            return workAndRestRep.DeleteById(id);
        }

        public WorkAndRest FindByDate(DateTime datetime)
        {
            DataContext dc = new DataContext(this.DbString);
            WorkAndRest wr = dc.Context.GetTable<WorkAndRest>().FirstOrDefault(s => s.dateAt.Equals(datetime.Date));
            if (wr == null)
                throw new WorkAndRestNotFoundException();
            return wr;
        }

        public WorkAndRest FindById(int id)
        {
            return workAndRestRep.FindById(id);
        }

        public List<WorkAndRest> GetAllTableName()
        {
            return workAndRestRep.GetAllTableName();
        }

        public List<WorkAndRest> GetByDateSpan(DateTime startDate, DateTime endDate)
        {
            return new DataContext(this.DbString).Context.GetTable<WorkAndRest>().Where(s => s.dateAt >= startDate && s.dateAt <= endDate).ToList();
        }

        public bool HasDateAtExist(DateTime? datetime)
        {
            return workAndRestRep.HasDateAtExist(datetime);
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

        public IQueryable<WorkAndRest> Search(WorkAndRestSearchModel searchModel) {
            return workAndRestRep.Search(searchModel);
        }

        public bool Update(WorkAndRest wr)
        {
            return workAndRestRep.Update(wr);
        }
    }
}
