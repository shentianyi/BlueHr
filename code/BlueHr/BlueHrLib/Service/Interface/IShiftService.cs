using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.MQTask;
using BlueHrLib.Data.Model.PageViewModel;

namespace BlueHrLib.Service.Interface
{
    public interface IShiftService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        List<Shift> All();

        IQueryable<Shift> Search(ShiftSearchModel searchModel);

        Shift FindById(int id);

        bool Create(Shift title);

        bool Update(Shift title);

        bool DeleteById(int id);

        Shift FindByCode(string code);

        ShiftInfoModel GetShiftInfo(ShiftSearchModel searchModel);
    }
}
