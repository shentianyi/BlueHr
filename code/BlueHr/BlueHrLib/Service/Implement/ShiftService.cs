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

namespace BlueHrLib.Service.Implement
{
    public class ShiftService : ServiceBase, IShiftService
    {
        public ShiftService(string dbString) : base(dbString) { }

        public List<TaskRound> All()
        {
            IShiftRepository rep = new ShiftRepository(new DataContext(this.DbString));

            return rep.All();
        }
    }
}
