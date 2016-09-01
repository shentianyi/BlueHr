using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.MQTask
{
    [Serializable]
    public class TaskSetting
    {
        /// <summary>
        /// 任务类型
        /// 包含：
        /// CalAtt - 计算考勤
        /// </summary>
        public string TaskType { get; set; }

        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime TaskCreateAt { get; set; }

        /// <summary>
        /// Json参数
        /// </summary>
        public string JsonParameter { get; set; }
    }
}
