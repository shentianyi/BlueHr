using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface ISysAuthorizationRepository
    {
        IQueryable<SysAuthorization> Search(SysAuthorizationSearchModel searchModel);
        bool Create(SysAuthorization sysAuth);
        bool DeleteById(int id);
        SysAuthorization FindById(int id);
        bool Update(SysAuthorization sysAuth);
        List<SysAuthorization> GetAll();
    }
}
