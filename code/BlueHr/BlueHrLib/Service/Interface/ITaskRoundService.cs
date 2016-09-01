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
    public interface ITaskRoundService
    {
        /// <summary>
        /// 创建任务
        /// </summary>
        /// <returns></returns>
        TaskRound Create(TaskType type);

        /// <summary>
        /// 根据uuid结束任务
        /// </summary>
        void FinishTaskByUniqId(Guid uuid,string result,bool error=false);

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        IQueryable<TaskRound> List();
    }
}
