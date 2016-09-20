using BlueHrLib.Data;
using Brilliantech.Framwork.Utils.LogUtil;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.MQTask.Job
{
    /// <summary>
    /// 计算工时
    /// </summary>
    public class CalAttJobTrigger : TriggerBase
    {
        private static string groupId = "CalAttJobGroup";
        
        public CalAttJobTrigger(List<QuartzJob> jobs,string dbString,string queuePath):base(jobs)
        {
            //LogUtil.Logger.Info(dbString);
            //LogUtil.Logger.Info(queuePath);
            this.TriggerJobs = new List<IJobDetail>();
            
            this.Triggers = new List<ICronTrigger>();
            int i = 1;
            foreach (var j in jobs)
            {
                if (!string.IsNullOrEmpty(j.paramsStr)){
                    ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                             .WithIdentity("CalAttJobTrigger-" + i.ToString(), groupId)
                                                             .WithCronSchedule(j.cronSchedule)
                                                             .Build();
                    
                  
                    IJobDetail jj = JobBuilder.Create<CalAttJob>()
                    .WithIdentity("CalAttJob-"+i.ToString(), groupId)
                    .Build();
                    jj.JobDataMap.Add("queuePath", queuePath);
                    jj.JobDataMap.Add("dbString", dbString);
                    jj.JobDataMap.Add("code", j.paramsStr);
                    
                    this.TriggerJobs.Add(jj);
                    this.Triggers.Add(trigger);

                }
                i += 1;
            }
        }
    }
}