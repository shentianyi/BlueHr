using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class ExtraWorkTypeRepository : RepositoryBase<ExtraWorkType>, IExtraWorkTypeRepository
    {
        private BlueHrDataContext context;

        public ExtraWorkTypeRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public List<ExtraWorkType> All()
        {
            return this.context.GetTable<ExtraWorkType>().ToList();
        }

        public List<ExtraWorkType> GetAllTableName()
        {
            try
            {
                return (this.context.GetTable<ExtraWorkType>()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
