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
        IQueryable<SysAuthorization> SearchByRoleAndAuth(SysRoleAuthorizationSearchModel searchModel);

        SysRoleAuthorization FindById(int id);

        bool Create(SysRoleAuthorization title);

        bool Update(SysRoleAuthorization title);

        bool DeleteById(int id);

        SysAuthorizationInfoModel GetSysAuthorizationInfo(SysRoleAuthorizationSearchModel searchModel);

        bool Creates(List<SysRoleAuthorization> records);

        List<SysRoleAuthorization> GetAll();

        List<SysRoleAuthorization> GetSysRoleAuthListByRoleName(string roleName);

        bool AuthUrl(int roleId, string controller, string action);

        List<SysRoleAuthorization> FindByRoleId(int roleId);

        bool DeleteByRoleId(int roleId);
        List<SysRoleAuthorization> GetAllTableName();
    }
}
