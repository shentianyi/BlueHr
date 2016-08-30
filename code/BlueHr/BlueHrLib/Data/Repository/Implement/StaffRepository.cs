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

            if (!string.IsNullOrWhiteSpace(searchModel.Nr))
            {
                staffs = staffs.Where(c => c.nr.Contains(searchModel.Nr.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                staffs = staffs.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Id))
            {
                staffs = staffs.Where(c => c.id.Contains(searchModel.Id.Trim()));
            }

            if (searchModel.Sex.HasValue)
            {
                staffs = staffs.Where(c => c.sex.Equals(searchModel.Sex));
            }

            if (searchModel.JobTitleId.HasValue)
            {
                staffs = staffs.Where(c => c.jobTitleId.Equals(searchModel.JobTitleId));
            }

            if (searchModel.CompanyId.HasValue)
            {
                staffs = staffs.Where(c => c.companyId.Equals(searchModel.CompanyId));
            }

            if (searchModel.DepartmentId.HasValue)
            {
                staffs = staffs.Where(c => c.departmentId.Equals(searchModel.DepartmentId));
            }

            if (searchModel.CompanyEmployAtFrom.HasValue)
            {
                staffs = staffs.Where(c => c.companyEmployAt > searchModel.CompanyEmployAtFrom);
            }

            if (searchModel.CompanyEmployAtTo.HasValue)
            {
                staffs = staffs.Where(c => c.companyEmployAt < searchModel.CompanyEmployAtTo);
            }

            if (searchModel.IsOnTrial.HasValue)
            {
                staffs = staffs.Where(c => c.isOnTrial.Equals(searchModel.IsOnTrial));
            }

            return staffs;
        }
    }
}
