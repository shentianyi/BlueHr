using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IDegreeTypeRepository
    {
        IQueryable<DegreeType> Search(DegreeTypeSearchModel searchModel);

        bool Create(DegreeType degreeType);

        DegreeType FindById(int id);

        DegreeType FindByName(string name);

        bool Update(DegreeType degreeType);

        bool DeleteById(int id);

        List<DegreeType> GetAll();
    }
}
