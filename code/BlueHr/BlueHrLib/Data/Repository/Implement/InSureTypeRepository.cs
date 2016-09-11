using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Data.Repository.Implement
{
    public class InSureTypeRepository : RepositoryBase<InsureType>, IInSureTypeRepository
    {
        private BlueHrDataContext context;

        public InSureTypeRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<InsureType> Search(InSureTypeSearchModel searchModel)
        {
            IQueryable<InsureType> inSuretypes = this.context.InsureType;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                inSuretypes = inSuretypes.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }
            return inSuretypes;
        }
        public bool Create(InsureType title)
        {
            try
            {
                this.context.GetTable<InsureType>().InsertOnSubmit(title);
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
            var dep = this.context.GetTable<InsureType>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<InsureType>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public InsureType FindById(int id)
        {
            return this.context.GetTable<InsureType>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(InsureType title)
        {
            var dep = this.context.GetTable<InsureType>().FirstOrDefault(c => c.id.Equals(title.id));
            if (dep != null)
            {
                dep.name = title.name;
                dep.remark = title.remark;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<InsureType> GetAll()
        {
            return this.context.GetTable<InsureType>().ToList();
        }
    }
}
