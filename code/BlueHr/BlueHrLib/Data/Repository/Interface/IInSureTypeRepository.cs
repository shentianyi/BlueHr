using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IInSureTypeRepository
    {
        IQueryable<InsureType> Search(InSureTypeSearchModel searchModel);

        bool Create(InsureType jobTitle);

        InsureType FindById(int id);

        InsureType FindByName(string name);

        bool Update(InsureType jobTitle);

        bool DeleteById(int id);

        List<InsureType> GetAll();
        List<InsureType> GetAllTableName();
    }
}
