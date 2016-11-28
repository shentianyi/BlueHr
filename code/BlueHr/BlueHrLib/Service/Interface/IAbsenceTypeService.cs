using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
  
   public interface IAbsenceTypeService
    {
        IQueryable<AbsenceType> Search(AbsenceTypeSearchModel searchModel);

        AbsenceType FindById(int id);

        bool Create(AbsenceType title);

        bool Update(AbsenceType title);

        bool DeleteById(int id);

        AbsenceTypeInfoModel GetAbsenceTypeInfo(AbsenceTypeSearchModel searchModel);

        List<AbsenceType> GetAll();

        List<AbsenceType> GetSystemTypes();
        List<AbsenceType> GetAllTableName();
    }
}
