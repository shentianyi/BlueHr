using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
    public interface IResignTypeService
    {
        IQueryable<ResignType> Search(ResignTypeSearchModel searchModel);

        ResignType FindById(int id);

        bool Create(ResignType title);

        bool Update(ResignType title);

        bool DeleteById(int id);

        ResignTypeInfoModel GetResignTypeInfo(ResignTypeSearchModel searchModel);

        List<ResignType> GetAll();
    }
}
