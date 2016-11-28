using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Service.Interface
{
    public interface IInSureTypeService
    {
        IQueryable<InsureType> Search(InSureTypeSearchModel searchModel);

        InsureType FindById(int id);

        InsureType FindByName(string name);

        bool Create(InsureType title);

        bool Update(InsureType title);

        bool DeleteById(int id);

        InSureTypeInfoModel GetInsureTypeInfo(InSureTypeSearchModel searchModel);

        List<InsureType> GetAll();
        List<InsureType> GetAllTableName();
    }
}
