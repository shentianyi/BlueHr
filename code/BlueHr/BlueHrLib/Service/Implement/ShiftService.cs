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
using BlueHrLib.MQTask;
using BlueHrLib.CusException;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Model.PageViewModel;

namespace BlueHrLib.Service.Implement
{
    public class ShiftService : ServiceBase, IShiftService
    {
        //public ShiftService(string dbString) : base(dbString) { }
        private IShiftRepository rep;

        public ShiftService(string dbString) : base(dbString)
        {
            rep = new ShiftRepository(this.Context);
        }

        public List<Shift> All()
        {
            IShiftRepository rep = new ShiftRepository(new DataContext(this.DbString));

            return rep.All();
        } 

        public IQueryable<Shift> Search(ShiftSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(Shift model)
        {
            return rep.Create(model);
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public Shift FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(Shift model)
        {
            return rep.Update(model);
        }

        public ShiftInfoModel GetShiftInfo(ShiftSearchModel searchModel)
        {
            ShiftInfoModel info = new ShiftInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IShiftRepository rep = new ShiftRepository(dc);
            IQueryable<Shift> results = rep.Search(searchModel);

            info.shiftCount = dc.Context.GetTable<Shift>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }

        public Shift FindByCode(string code)
        {
            DataContext dc = new DataContext(this.DbString);
            return dc.Context.GetTable<Shift>().FirstOrDefault(s => s.code.Equals(code));
        }

        public List<Shift> GetAllTableName()
        {
            return rep.GetAllTableName();
        }
    }
}
