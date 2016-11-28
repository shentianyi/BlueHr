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

    public class AbsenceTypeService : ServiceBase, IAbsenceTypeService
    {
        private IAbsenceTypeRepository rep;

        public AbsenceTypeService(string dbString) : base(dbString)
        {
            rep = new AbsenceTypeRepository(this.Context);
        }

        public IQueryable<AbsenceType> Search(AbsenceTypeSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(AbsenceType model)
        {
            return rep.Create(model);
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public AbsenceType FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(AbsenceType model)
        {
            return rep.Update(model);
        }

        public AbsenceTypeInfoModel GetAbsenceTypeInfo(AbsenceTypeSearchModel searchModel)
        {
            AbsenceTypeInfoModel info = new AbsenceTypeInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IAbsenceTypeRepository rep = new AbsenceTypeRepository(dc);
            IQueryable<AbsenceType> results = rep.Search(searchModel);

            info.absenceTypeCount = dc.Context.GetTable<ResignType>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }

        public List<AbsenceType> GetAll()
        {
            return rep.GetAll();
        }

        public List<AbsenceType> GetSystemTypes()
        {
        return    new DataContext(this.DbString).Context.GetTable<AbsenceType>().Where(s => s.systemCode > 0).ToList();
        }

        public List<AbsenceType> GetAllTableName()
        {
            return rep.GetAllTableName();
        }
    }
}
