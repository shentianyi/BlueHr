using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Data.Repository.Implement
{
    public class StaffTypeRepository : RepositoryBase<StaffType>, IStaffTypeRepository
    {
        private BlueHrDataContext context;

        public StaffTypeRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<StaffType> Search(StaffTypeSearchModel searchModel)
        {
            IQueryable<StaffType> stafftypes = this.context.StaffType;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                stafftypes = stafftypes.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }
            return stafftypes;
        }
    }
}
