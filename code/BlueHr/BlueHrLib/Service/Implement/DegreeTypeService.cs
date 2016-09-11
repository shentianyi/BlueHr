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

        public bool Create(DegreeType degreeType)
        {
            DataContext dc = new DataContext(this.DbString);
            IDegreeTypeRepository degreeTypeRep = new DegreeTypeRepository(dc);
            return degreeTypeRep.Create(degreeType);
        }

        public bool Update(DegreeType degreeType)
        {
            DataContext dc = new DataContext(this.DbString);
            IDegreeTypeRepository degreeTypeRep = new DegreeTypeRepository(dc);
            return degreeTypeRep.Update(degreeType);
        }

        public bool DeleteById(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            IDegreeTypeRepository degreeTypeRep = new DegreeTypeRepository(dc);
            return degreeTypeRep.DeleteById(id);
        }

        public DegreeType FindById(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            IDegreeTypeRepository degreeTypeRep = new DegreeTypeRepository(dc);
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
            DataContext dc = new DataContext(this.DbString);
            IDegreeTypeRepository degreeTypeRep = new DegreeTypeRepository(dc);
            return degreeTypeRep.GetAll();
        }
    }
}
