using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{

    public interface ISysRoleAuthorizationService
    {
        IQueryable<SysRoleAuthorization> Search(SysRoleAuthorizationSearchModel searchModel);

        SysRoleAuthorization FindById(int id);

        bool Create(SysRoleAuthorization title);

        bool Update(SysRoleAuthorization title);

        bool DeleteById(int id);

        SysRoleAuthorizationInfoModel GetSysRoleAuthorizationInfo(SysRoleAuthorizationSearchModel searchModel);


        bool Creates(List<SysRoleAuthorization> records);

        List<SysRoleAuthorization> GetAll();

    }
}
