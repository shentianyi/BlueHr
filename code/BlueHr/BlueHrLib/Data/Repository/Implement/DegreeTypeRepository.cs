using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Data.Repository.Implement
{
    public class DegreeTypeRepository : RepositoryBase<DegreeType>, IDegreeTypeRepository
    {
        private BlueHrDataContext context;

        public DegreeTypeRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<DegreeType> Search(DegreeTypeSearchModel searchModel)
        {
            IQueryable<DegreeType> degreetypes = this.context.DegreeType;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                degreetypes = degreetypes.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }
            return degreetypes;
        }

        public bool Create(DegreeType degreeType)
        { 
            try
            {
                this.context.GetTable<DegreeType>().InsertOnSubmit(degreeType);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception)
            { 
                return false;
            } 
        }

        public bool DeleteById(int id)
        {
            DegreeType cp = this.context.GetTable<DegreeType>().FirstOrDefault(c => c.id.Equals(id));

            if (cp != null)
            {
                this.context.GetTable<DegreeType>().DeleteOnSubmit(cp);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public DegreeType FindById(int id)
        {
            DegreeType cp = this.context.GetTable<DegreeType>().FirstOrDefault(c => c.id.Equals(id));
            return cp;
        } 

        public bool Update(DegreeType degreeType)
        {
            DegreeType cp = this.context.GetTable<DegreeType>().FirstOrDefault(c => c.id.Equals(degreeType.id));

            if (cp != null)
            {
                cp.name = degreeType.name;
                cp.remark = degreeType.remark;
                //cp.Staff = degreeType.Staff;

                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            } 
        }

        public List<DegreeType> GetAll()
        {
            return this.context.GetTable<DegreeType>().ToList();
        }

        public DegreeType FindByName(string name)
        {
            return this.context.GetTable<DegreeType>().FirstOrDefault(c => c.name.Equals(name.Trim()));
        }

        public List<DegreeType> GetAllTableName()
        {
            try
            {
                return (this.context.GetTable<DegreeType>()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
} 