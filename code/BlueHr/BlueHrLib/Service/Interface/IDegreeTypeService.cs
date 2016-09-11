using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Service.Interface
{
    public interface IDegreeTypeService
    {
        IQueryable<DegreeType> Search(DegreeTypeSearchModel searchModel);

        DegreeTypeInfoModel GetDegreeTypeInfo(DegreeTypeSearchModel searchModel);

        bool Create(DegreeType degreeType);

        DegreeType FindById(int id);

        bool Update(DegreeType degreeType);

        bool DeleteById(int id);

        List<DegreeType> GetAll();
    }
}
