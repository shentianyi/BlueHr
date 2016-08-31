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

namespace BlueHrLib.Service.Implement
{
    public class InSureTypeService : ServiceBase, IInSureTypeService
    {
        public InSureTypeService(string dbString) : base(dbString) { }

        public IQueryable<InsureType> Search(InSureTypeSearchModel searchModel)
        {
            DataContext dc = new DataContext(this.DbString);
            IInSureTypeRepository inSureTypeRep = new InSureTypeRepository(dc);
            return inSureTypeRep.Search(searchModel);
        }
    }
}
