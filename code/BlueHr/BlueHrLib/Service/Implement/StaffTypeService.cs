using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Model.PageViewModel;

namespace BlueHrLib.Service.Implement
{
    public class StaffTypeService : ServiceBase, IStaffTypeService
    {

        private IStaffTypeRepository staffTypeRep;

        public StaffTypeService(string dbString) : base(dbString)
        {
            staffTypeRep = new StaffTypeRepository(this.Context);
        }

        public bool Create(StaffType staffType)
        {
            return staffTypeRep.Create(staffType);
        }

        public bool DeleteById(int id)
        {
            return staffTypeRep.DeleteById(id);
        }

        public IQueryable<StaffType> FindAll()
        {
            return staffTypeRep.FindByAll();
        }

        public StaffType FindById(int id)
        {
            return staffTypeRep.FindById(id);
        }

        public IQueryable<StaffType> Search(StaffTypeSearchModel searchModel)
        {
            DataContext dc = new DataContext(this.DbString);
            IStaffTypeRepository staffTypeRep = new StaffTypeRepository(dc);
            return staffTypeRep.Search(searchModel);
        }

        public bool Update(StaffType staffType)
        {
            return staffTypeRep.Update(staffType);
        }

        public StaffTypeInfoModel GetStaffTypeInfo(StaffTypeSearchModel searchModel)
        {
            StaffTypeInfoModel info = new StaffTypeInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IStaffTypeRepository staffTypeRep = new StaffTypeRepository(dc);
            IQueryable<StaffType> staffTypes = staffTypeRep.Search(searchModel);

            info.staffTypeCount = dc.Context.GetTable<JobTitle>().Where(c => c.id.Equals(staffTypes.Count() > 0 ? staffTypes.First().id : -1)).Count();

            return info;
        }

        public List<StaffType> GetAll()
        {
            return staffTypeRep.GetAll();
        }

        public StaffType FindByName(string name)
        {
            return staffTypeRep.FindByName(name);
        }
    }
}
