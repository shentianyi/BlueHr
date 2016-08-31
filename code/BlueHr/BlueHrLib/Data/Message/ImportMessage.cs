using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Message
{
    public class ImportMessage
    {
        public ImportMessage()
        {
            this.Success = false;
            this.ErrorFileFeed = false;
        }


        /// <summary>
        /// 成功标记
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 是否反馈错误文件
        /// </summary>
        public bool ErrorFileFeed { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
         


    }
}
