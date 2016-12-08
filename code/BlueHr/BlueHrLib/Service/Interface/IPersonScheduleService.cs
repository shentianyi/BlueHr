using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Service.Interface
{
    public interface IPersonScheduleService
    {
        IQueryable<PersonSchedule> Search(PersonScheduleSearchModel searchModel);

        /// <summary>
        /// 根据ID获取日程安排
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        PersonSchedule FindById(int id);

        PersonSchedule Create(PersonSchedule personSchedule);

        /// <summary>
        /// 更新日程安排
        /// </summary>
        /// <param name="PersonSchedule"></param>
        /// <returns></returns>

        bool Update(PersonSchedule personSchedule);

        /// <summary>
        /// 通过ID删除日程安排
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        bool DeleteById(int id);
    }
}
