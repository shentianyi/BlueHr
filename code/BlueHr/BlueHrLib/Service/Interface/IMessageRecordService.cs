using BlueHrLib.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
    public interface IMessageRecordService
    {
        /// <summary>
        /// 创建信息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        /// <param name="type"></param>
        /// <param name="text"></param>
        /// <param name="isSystem"></param>
        void Create(string staffNr, int? operatorId, MessageRecordType type, string text,bool isSystem=true);
    }
}
