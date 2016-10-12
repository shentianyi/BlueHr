using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{

    public interface ISysAuthorizationService
    {
        IQueryable<SysAuthorization> Search(SysAuthorizationSearchModel searchModel);

        SysAuthorization FindById(int id);

        bool Create(SysAuthorization title);

        bool Update(SysAuthorization title);

        bool DeleteById(int id);

        SysAuthorizationInfoModel GetSysAuthorizationInfo(SysAuthorizationSearchModel searchModel);

        bool Creates(List<SysAuthorization> records);

        List<SysAuthorization> GetAll();

    }
}

