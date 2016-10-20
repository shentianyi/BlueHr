using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{

    public interface ISysRoleService
    {
        IQueryable<SysRole> Search(SysRoleSearchModel searchModel);

        SysRole FindById(int id);

        bool Create(SysRole title);

        bool Update(SysRole title);

        bool DeleteById(int id);

        SysRoleInfoModel GetSysRoleInfo(SysRoleSearchModel searchModel);


        bool Creates(List<SysRole> records);

        List<SysRole> GetAll();

    }
}
