using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{ 
    interface ISysUserDataAuthRepository
    {
        IQueryable<SysUserDataAuth> Search(SysUserDataAuthSearchModel searchModel);

        bool Create(SysUserDataAuth sysRole);

        SysUserDataAuth FindById(int id);

        bool Update(SysUserDataAuth sysRole);

        bool DeleteById(int id);

        //List<SysRole> FindByAbsenceType(int id);

        List<SysUserDataAuth> GetAll();
    }
}
