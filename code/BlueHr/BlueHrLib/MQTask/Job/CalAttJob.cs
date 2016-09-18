using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using Brilliantech.Framwork.Utils.LogUtil;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.MQTask.Job
{
    public class CalAttJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                LogUtil.Logger.Info(context.JobDetail.JobDataMap["dbString"].ToString());
                LogUtil.Logger.Info(context.JobDetail.JobDataMap["queuePath"].ToString());
                LogUtil.Logger.Info(context.JobDetail.JobDataMap["code"].ToString());

                TaskDispatcher td = new TaskDispatcher(context.JobDetail.JobDataMap["dbString"].ToString(),
                    context.JobDetail.JobDataMap["queuePath"].ToString());
                td.SendCalculateAttMessage(DateTime.Now, new List<string>() { context.JobDetail.JobDataMap["code"].ToString() });
            }
            catch (Exception ex)
            {

                LogUtil.Logger.Error("转正提醒任务错误！", ex);
            }
        }
    }
}
