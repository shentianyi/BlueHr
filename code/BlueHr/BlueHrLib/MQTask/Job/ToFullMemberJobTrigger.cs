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
        
        public ToFullMemberJobTrigger(List<QuartzJob> jobs,string dbString):base(jobs,dbString)
        {
            this.Job  = JobBuilder.Create<ToFullMemberJob>()
                    .WithIdentity("ToFullMemberJob", groupId)
                    .Build();
            this.Job.JobDataMap.Add("dbString", this.DbString);

            this.Triggers = new List<ICronTrigger>();
            int i = 1;
            foreach (var j in jobs)
            {
                ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                          .WithIdentity("ToFullMemberJobTrigger-" + i.ToString(), groupId)
                                                          .WithCronSchedule(j.cronSchedule)
                                                          .Build();
                this.Triggers.Add(trigger);
                i += 1;
            }
        }
    }
}