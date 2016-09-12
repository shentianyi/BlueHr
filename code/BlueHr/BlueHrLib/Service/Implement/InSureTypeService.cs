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
using BlueHrLib.Data.Model.PageViewModel;

namespace BlueHrLib.Service.Implement
{
    public class InSureTypeService : ServiceBase, IInSureTypeService
    {
        private IInSureTypeRepository inSureTypeRep;

        public InSureTypeService(string dbString) : base(dbString) {

            inSureTypeRep = new InSureTypeRepository(this.Context);
        }

        public IQueryable<InsureType> Search(InSureTypeSearchModel searchModel)
        { 
            return inSureTypeRep.Search(searchModel);
        }
        public bool Create(InsureType insureType)
        {
            return inSureTypeRep.Create(insureType);
        }

        public bool DeleteById(int id)
        {
            return inSureTypeRep.DeleteById(id);
        }

        public InsureType FindById(int id)
        {
            return inSureTypeRep.FindById(id);
        }

        public bool Update(InsureType insureType)
        {
            return inSureTypeRep.Update(insureType);
        }

        public InSureTypeInfoModel GetInsureTypeInfo(InSureTypeSearchModel searchModel)
        {
            InSureTypeInfoModel info = new InSureTypeInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IInSureTypeRepository inSureTypeRep = new InSureTypeRepository(dc);
            IQueryable<InsureType> insureTypes = inSureTypeRep.Search(searchModel);

            info.insureTypeCount = dc.Context.GetTable<InsureType>().Where(c => c.id.Equals(insureTypes.Count() > 0 ? insureTypes.First().id : -1)).Count();

            return info;  
        }

        public List<InsureType> GetAll()
        {
            return inSureTypeRep.GetAll();
        }

        public InsureType FindByName(string name)
        {
            return inSureTypeRep.FindByName(name);
        }
    }
}
