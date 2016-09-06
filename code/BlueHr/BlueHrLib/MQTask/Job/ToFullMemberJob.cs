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
    public class ToFullMemberJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            { 
                IMessageRecordService mrs = new MessageRecordService(context.JobDetail.JobDataMap["dbString"].ToString());
                mrs.CreateToFullMemberMessage(DateTime.Now);
            }
            catch (Exception ex)
            {

                LogUtil.Logger.Error("转正提醒任务错误！", ex);
            }
        }
    }
}
