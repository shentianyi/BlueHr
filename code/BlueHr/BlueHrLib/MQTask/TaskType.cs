using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BlueHrLib.MQTask
{
    public enum TaskType
    {
        /// <summary>
        /// 计算考勤信息
        /// </summary>
        [Description("计算考勤")]
        CalAtt=100,

        /// <summary>
        /// 发送邮件
        /// </summary>
        [Description("发送邮件")]
        SendMail=200
    }
}
