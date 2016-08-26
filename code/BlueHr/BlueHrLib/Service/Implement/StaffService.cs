using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;

namespace BlueHrLib.Service.Implement
{
    public class StaffService : ServiceBase, IStaffService
    {
        public StaffService(string dbString) : base(dbString) { }

        public Staff FindByStaffId(string id)
        {
            DataContext dc = new DataContext(this.DbString);
            return dc.Context.GetTable<Staff>().FirstOrDefault(s => s.id.Equals(id));
        }
    }
}
