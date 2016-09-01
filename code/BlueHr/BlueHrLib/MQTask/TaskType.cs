using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.MQTask
{
    public enum TaskType
    {
        /// <summary>
        /// 计算考勤信息
        /// </summary>
        CalAtt,

        /// <summary>
        /// 发送邮件
        /// </summary>
        SendMail
    }
}
