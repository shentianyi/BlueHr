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
    /// 转正提醒
    /// </summary>
    public class ToFullMemberJobTrigger:TriggerBase
    {
        private static string groupId = "ToFullMemberJobGroup";
        
        public ToFullMemberJobTrigger(List<QuartzJob> jobs,string dbString,string queuePath):base(jobs)
        {
            //LogUtil.Logger.Info(dbString);
            //LogUtil.Logger.Info(queuePath);

            //this.Job  = JobBuilder.Create<ToFullMemberJob>()
            //        .WithIdentity("ToFullMemberJob", groupId)
            //        .Build();
            //this.Job.JobDataMap.Add("queuePath",  queuePath);
            //this.Job.JobDataMap.Add("dbString", dbString);
            this.TriggerJobs = new List<IJobDetail>();

            this.Triggers = new List<ICronTrigger>();
            int i = 1;
            foreach (var j in jobs)
            {
                ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                          .WithIdentity("ToFullMemberJobTrigger-" + i.ToString(), groupId)
                                                          .WithCronSchedule(j.cronSchedule)
                                                          .Build();
                IJobDetail jj = JobBuilder.Create<CalAttJob>()
                   .WithIdentity("ToFullMemberJob-" + i.ToString(), groupId)
                   .Build();
                jj.JobDataMap.Add("queuePath", queuePath);
                jj.JobDataMap.Add("dbString", dbString);
                jj.JobDataMap.Add("code", j.paramsStr);

                this.TriggerJobs.Add(jj);

                this.Triggers.Add(trigger);
                i += 1;
            }
        }
    }
}