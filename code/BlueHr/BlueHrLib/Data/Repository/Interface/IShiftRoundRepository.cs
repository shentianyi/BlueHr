using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IShiftRepository
    {
        ///// <summary>
        ///// 创建任务
        ///// </summary>
        ///// <param name="tr"></param>
        //void Create(TaskRound tr);

        List<Shift> All();

    }
}
