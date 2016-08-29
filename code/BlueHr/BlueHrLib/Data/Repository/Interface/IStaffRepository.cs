using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IStaffRepository
    {
        IQueryable<Staff> Search(StaffSearchModel searchModel);
    }
}
