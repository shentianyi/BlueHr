using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Service.Interface
{
   public interface IWorkAndRestService
    {
        /// <summary>
        /// 是否是休息日
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        bool IsRestDay(DateTime datetime);
        bool IsRestDay(WorkAndRest wr);

        /// <summary>
        /// 是否是工作日
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        bool IsWorkDay(DateTime datetime);
        bool IsWorkDay(WorkAndRest wr);

        /// <summary>
        /// 根据时间找到作息表
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        WorkAndRest FindByDate(DateTime datetime);

        IQueryable<WorkAndRest> Search(WorkAndRestSearchModel wrs);

        bool Create(WorkAndRest wr);

        bool Update(WorkAndRest wr);

        WorkAndRest FindById(int id);

        bool DeleteById(int id);

        bool HasDateAtExist(DateTime? datetime);

        List<WorkAndRest> GetByDateSpan(DateTime startDate, DateTime endDate);
        List<WorkAndRest> GetAllTableName();
    }
}
