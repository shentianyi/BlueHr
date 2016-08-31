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
    }
}
