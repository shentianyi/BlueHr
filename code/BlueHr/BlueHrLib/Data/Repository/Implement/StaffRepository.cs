using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Data.Repository.Implement
{
  public class StaffRepository:RepositoryBase<Staff>, IStaffRepository
    {
        private BlueHrDataContext context;

        public StaffRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public IQueryable<Staff> Search(StaffSearchModel searchModel)
        {
            IQueryable<Staff> staffs = this.context.Staff;
            staffs = staffs.Where(c => c.name.Contains(searchModel.Name));
            return staffs;
        }
    }
}
