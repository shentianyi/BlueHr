using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;

namespace BlueHrLib.Service.Implement
{
    public class DegreeTypeService : ServiceBase, IDegreeTypeService
    {
        public DegreeTypeService(string dbString) : base(dbString) { }

        public IQueryable<DegreeType> Search(DegreeTypeSearchModel searchModel)
        {
            DataContext dc = new DataContext(this.DbString);
            IDegreeTypeRepository degreeTypeRep = new DegreeTypeRepository(dc);
            return degreeTypeRep.Search(searchModel);
        }
    }
}
