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
        SendMail=200,

        /// <summary>
        /// 发送考勤异常邮件
        /// </summary>
        [Description("发送考勤异常邮件")]
        SendAttExceptionMail = 201,

        /// <summary>
        /// 员工转正提醒
        /// </summary>
        [Description("员工转正提醒")]
        ToFullMemeberWarn=300,

        /// <summary>
        /// 重启后台服务
        /// </summary>
        [Description("重启后台服务")]
        ReStartSvc = 9999
    }
}
