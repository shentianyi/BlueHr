using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    
    interface ISysRoleAuthorizationRepository
    {
        IQueryable<SysRoleAuthorization> Search(SysRoleAuthorizationSearchModel searchModel);

        bool Create(SysRoleAuthorization roleAuth);

        SysRoleAuthorization FindById(int id);

        bool Update(SysRoleAuthorization roleAuth);

        bool DeleteById(int id);

        //List<AbsenceRecrod> FindByAbsenceType(int id);

        List<SysRoleAuthorization> GetAll();
    }
}