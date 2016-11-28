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
        private IDegreeTypeRepository degreeTypeRep;
        public DegreeTypeService(string dbString) : base(dbString) {
            degreeTypeRep = new DegreeTypeRepository(this.Context);
        }

        public IQueryable<DegreeType> Search(DegreeTypeSearchModel searchModel)
        {
            return degreeTypeRep.Search(searchModel);
        }

        public bool Create(DegreeType degreeType)
        {
            return degreeTypeRep.Create(degreeType);
        }

        public bool Update(DegreeType degreeType)
        {
            return degreeTypeRep.Update(degreeType);
        }

        public bool DeleteById(int id)
        {
            return degreeTypeRep.DeleteById(id);
        }

        public DegreeType FindById(int id)
        {
            return degreeTypeRep.FindById(id);
        }

        public DegreeTypeInfoModel GetDegreeTypeInfo(DegreeTypeSearchModel searchModel)
        {
            DegreeTypeInfoModel info = new DegreeTypeInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IDegreeTypeRepository degreeTypeRep = new DegreeTypeRepository(dc);
            IQueryable<DegreeType> degreeType = degreeTypeRep.Search(searchModel);

            info.degreeTypeCount = dc.Context.GetTable<DegreeType>().Where(c => c.id.Equals(degreeType.Count() > 0 ? degreeType.First().id : -1)).Count();

            return info;
        }

        public List<DegreeType> GetAll()
        {
            return degreeTypeRep.GetAll();
        }

        public DegreeType FindByName(string name)
        {
            return degreeTypeRep.FindByName(name);
        }

        public List<DegreeType> GetAllTableName()
        {
            return degreeTypeRep.GetAllTableName();
        }
    }
}
