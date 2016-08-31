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
    }
}
