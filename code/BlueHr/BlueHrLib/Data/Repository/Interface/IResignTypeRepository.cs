using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IResignTypeRepository
    {
        IQueryable<ResignType> Search(ResignTypeSearchModel searchModel);

        bool Create(ResignType resignType);

        ResignType FindById(int id);

        bool Update(ResignType resignType);

        bool DeleteById(int id);

        List<ResignType> GetAll();

        ResignType IsResignTypeExit(string resignType);
        List<ResignType> GetAllTableName();
    }
}
