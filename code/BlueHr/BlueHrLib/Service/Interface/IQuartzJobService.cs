using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.MQTask;
using BlueHrLib.Data.Enum;

namespace BlueHrLib.Service.Interface
{
    public interface IQuartzJobService
    {
        /// <summary>
        /// 根据任务类型获取所有计划
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<QuartzJob> GetByType(CronJobType type);

        bool Create(QuartzJob job);

        bool Delete(int id);
        List<QuartzJob> GetAllTableName();
    }
}
