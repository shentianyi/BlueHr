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
    
    public class ResignTypeService : ServiceBase, IResignTypeService
    {
        private IResignTypeRepository resignTypeRep;

        public ResignTypeService(string dbString) : base(dbString)
        {
            resignTypeRep = new ResignTypeRepository(this.Context);
        }

        public IQueryable<ResignType> Search(ResignTypeSearchModel searchModel)
        {
            return resignTypeRep.Search(searchModel);
        }

        public bool Create(ResignType resignType)
        {
            return resignTypeRep.Create(resignType);
        }

        public bool DeleteById(int id)
        {
            return resignTypeRep.DeleteById(id);
        }

        public ResignType FindById(int id)
        {
            return resignTypeRep.FindById(id);
        }

        public bool Update(ResignType resignType)
        {
            return resignTypeRep.Update(resignType);
        }

        public ResignTypeInfoModel GetResignTypeInfo(ResignTypeSearchModel searchModel)
        {
            ResignTypeInfoModel info = new ResignTypeInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IResignTypeRepository resignTypeRep = new ResignTypeRepository(dc);
            IQueryable<ResignType> resignTypes = resignTypeRep.Search(searchModel);

            info.resignTypeCount = dc.Context.GetTable<ResignType>().Where(c => c.id.Equals(resignTypes.Count() > 0 ? resignTypes.First().id : -1)).Count();

            return info;
        }

        public List<ResignType> GetAll()
        {
            return resignTypeRep.GetAll();
        }

        public ResignType IsResignTypeExit(string resignType)
        {
            return resignTypeRep.IsResignTypeExit(resignType);
        }

        public List<ResignType> GetAllTableName()
        {
            return resignTypeRep.GetAllTableName();
        }
    }
}
