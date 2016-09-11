using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IStaffTypeRepository
    {
        IQueryable<StaffType> Search(StaffTypeSearchModel searchModel);

        IQueryable<StaffType> FindByAll();

        bool Create(StaffType staffType);

        StaffType FindById(int id);

        bool Update(StaffType staffType);

        bool DeleteById(int id);

        List<StaffType> GetAll();
        
    }
}
