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

        public bool Create(StaffType staffType)
        {
            try
            {
                this.context.GetTable<StaffType>().InsertOnSubmit(staffType);
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
            var staffType = this.context.GetTable<StaffType>().FirstOrDefault(c => c.id.Equals(id));
            if (staffType != null)
            {
                this.context.GetTable<StaffType>().DeleteOnSubmit(staffType);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<StaffType> FindByAll()
        {
            return this.context.GetTable<StaffType>().Where(s => true);
        }

        public StaffType FindById(int id)
        {
            return this.context.GetTable<StaffType>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(StaffType staffType)
        {
            var st = this.context.GetTable<StaffType>().FirstOrDefault(c => c.id.Equals(staffType.id));
            if (st != null)
            {
                st.name = staffType.name;
                st.remark = staffType.remark;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
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
