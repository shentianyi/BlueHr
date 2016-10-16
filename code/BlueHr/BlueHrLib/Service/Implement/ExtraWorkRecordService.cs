using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Implement
{ 
    public class ExtraWorkRecordService : ServiceBase, IExtraWorkRecordService
    {
        private IExtraWorkRecordRepository rep;

        public ExtraWorkRecordService(string dbString) : base(dbString)
        {
            rep = new ExtraWorkRecordRepository(this.Context);
        }

        public IQueryable<ExtraWorkRecord> Search(ExtraWorkRecordSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(ExtraWorkRecord model)
        {
            return rep.Create(model);
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public ExtraWorkRecord FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(ExtraWorkRecord model)
        {
            return rep.Update(model);
        }

        public ExtraWorkRecordInfoModel GetExtraWorkRecordInfo(ExtraWorkRecordSearchModel searchModel)
        {
            ExtraWorkRecordInfoModel info = new ExtraWorkRecordInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IExtraWorkRecordRepository rep = new ExtraWorkRecordRepository(dc);
            IQueryable<ExtraWorkRecord> results = rep.Search(searchModel);

            info.extraWorkRecordCount = dc.Context.GetTable<ExtraWorkRecord>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }

        public bool Creates(List<ExtraWorkRecord> records)
        {
            DataContext dc = new DataContext(this.DbString);
            dc.Context.GetTable<ExtraWorkRecord>().InsertAllOnSubmit(records);
            dc.Context.SubmitChanges();
            return true;
        }

        public ExtraWorkRecordView FindViewByStaffNrAndDate(string staffNr, DateTime date)
        {
            return new DataContext(this.DbString).Context.GetTable<ExtraWorkRecordView>().Where(s => s.staffNr.Equals(staffNr) && s.otTime == date).FirstOrDefault();

        }

        public ExtraWorkRecord FindByStaffNrAndDete(string staffNr, DateTime date)
        {
            return new DataContext(this.DbString).Context.GetTable<ExtraWorkRecord>().Where(s => s.staffNr.Equals(staffNr) && s.otTime == date).FirstOrDefault();
        }

        public bool ApprovalTheRecord(ExtraWorkRecordApproval extralApproval)
        {
            return rep.ApprovalTheRecord(extralApproval);
        }
    }
}
