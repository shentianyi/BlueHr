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
    public class AbsenceRecordService : ServiceBase, IAbsenceRecordService
    {
        private IAbsenceRecrodRepository rep;

        public AbsenceRecordService(string dbString) : base(dbString)
        {
            rep = new AbsenceRecrodRepository(this.Context);
        }

        public IQueryable<AbsenceRecrod> Search(AbsenceRecrodSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(AbsenceRecrod model)
        {
            return rep.Create(model);
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public AbsenceRecrod FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(AbsenceRecrod model)
        {
            return rep.Update(model);
        }

        public AbsenceRecrodInfoModel GetAbsenceRecrodInfo(AbsenceRecrodSearchModel searchModel)
        {
            AbsenceRecrodInfoModel info = new AbsenceRecrodInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IAbsenceRecrodRepository rep = new AbsenceRecrodRepository(dc);
            IQueryable<AbsenceRecrod> results = rep.Search(searchModel);

            info.absenceRecordCount = dc.Context.GetTable<AbsenceRecrod>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }

        public List<AbsenceRecrod> FindByAbsenceType(int id)
        {
            return rep.FindByAbsenceType(id);
        }

        public bool Creates(List<AbsenceRecrod> records)
        {
            DataContext dc = new DataContext(this.DbString);
            dc.Context.GetTable<AbsenceRecrod>().InsertAllOnSubmit(records);
            dc.Context.SubmitChanges();
            return true;
        }

        public List<AbsenceRecrod> GetAll()
        {
            return rep.GetAll();
        }

        public AbsenceRecordView FindViewByStaffNrAndDate(string staffNr, DateTime date)
        {
            return new DataContext(this.DbString).Context.GetTable<AbsenceRecordView>().Where(s => s.staffNr.Equals(staffNr) && s.absenceDate == date).FirstOrDefault();
        }
    }
}
