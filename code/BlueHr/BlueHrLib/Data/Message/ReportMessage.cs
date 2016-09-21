using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Message
{
    public class ReportMessage
    {
        public ReportMessage()
        {
            this.Success = false;
        }


        /// <summary>
        /// 成功标记
        /// </summary>
        public bool Success { get; set; }
         
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
    }
}
