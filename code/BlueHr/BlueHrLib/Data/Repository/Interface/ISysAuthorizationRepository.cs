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

        //根据roleid 获取所有权限列表
        List<SysAuthorization> GetSysAuthByRoleId(string roleId);
        List<SysAuthorization> GetAllTableName();
        IQueryable<SysAuthorization> AdvancedSearch(string allTableName, string searchConditions, string searchValueFirst);
    }
}
