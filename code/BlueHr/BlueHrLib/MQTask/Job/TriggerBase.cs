using BlueHrLib.Data;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.MQTask.Job
{
    public class TriggerBase
    {
        public string DbString { get; set; }
        public TriggerBase() { }
        public TriggerBase(List<QuartzJob> jobs) { this.Jobs = jobs; }
        
        
        /// <summary>
        /// 任务配置列表
        /// </summary>
        public List<QuartzJob> Jobs { get; set; }

        /// <summary>
        /// 任务
        /// </summary>
        public IJobDetail Job { get; set; }

        /// <summary>
        /// 触发器
        /// </summary>
        public List<ICronTrigger> Triggers { get; set; }

    }
}
