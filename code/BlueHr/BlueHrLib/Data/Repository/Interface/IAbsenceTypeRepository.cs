using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IAbsenceTypeRepository
    {
        IQueryable<AbsenceType> Search(AbsenceTypeSearchModel searchModel);

        bool Create(AbsenceType resignType);

        AbsenceType FindById(int id);

        bool Update(AbsenceType resignType);

        bool DeleteById(int id);
    }
}