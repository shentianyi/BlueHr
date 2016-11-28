using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{ 
    interface ISysRoleRepository
    {
        IQueryable<SysRole> Search(SysRoleSearchModel searchModel);

        bool Create(SysRole sysRole);

        SysRole FindById(int id);

        bool Update(SysRole sysRole);

        bool DeleteById(int id);

        //List<SysRole> FindByAbsenceType(int id);

        List<SysRole> GetAll();
        List<SysRole> GetAllTableName();
    }
}