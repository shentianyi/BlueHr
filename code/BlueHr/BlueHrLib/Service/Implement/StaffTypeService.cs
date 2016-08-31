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
    public class StaffTypeService : ServiceBase, IStaffTypeService
    {
        public StaffTypeService(string dbString) : base(dbString) { }

        public IQueryable<StaffType> Search(StaffTypeSearchModel searchModel)
        {
            DataContext dc = new DataContext(this.DbString);
            IStaffTypeRepository staffTypeRep = new StaffTypeRepository(dc);
            return staffTypeRep.Search(searchModel);
        }
    }
}
