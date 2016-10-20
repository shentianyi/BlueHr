using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{

    public interface ISysUserDataAuthService
    {
        IQueryable<SysUserDataAuth> Search(SysUserDataAuthSearchModel searchModel);

        SysUserDataAuth FindById(int id);

        bool Create(SysUserDataAuth title);

        bool Update(SysUserDataAuth title);

        bool DeleteById(int id);

        //SysUserDataAuthInfoModel GetSysUserDataAuthInfo(SysUserDataAuthSearchModel searchModel);


        bool Creates(List<SysUserDataAuth> records);

        List<SysUserDataAuth> GetAll();

    }
}