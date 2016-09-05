using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.MQTask;

namespace BlueHrLib.Service.Interface
{
    public interface IShiftService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        List<Shift> All();

        
    }
}
