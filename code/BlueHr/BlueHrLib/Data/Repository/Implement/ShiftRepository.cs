using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Data.Repository.Implement
{
    public class ShiftRepository : RepositoryBase<Shift>, IShiftRepository
    {
        private BlueHrDataContext context;

        public ShiftRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public List<Shift> All()
        {
            return this.context.GetTable<Shift>().ToList();
        }
    }
}
