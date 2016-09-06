using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data;
using BlueHrLib.Helper;
using Brilliantech.Framwork.Utils.LogUtil;

namespace BlueHrLib.Service.Implement
{
    public class MessageRecordService : ServiceBase, IMessageRecordService
    {
        public MessageRecordService(string dbString) : base(dbString) { }

        /// <summary>
        /// 创建信息
        /// </summary>
        /// <param name="staffNr"></param>
        /// <param name="operatorId"></param>
        /// <param name="type"></param>
        /// <param name="text"></param>
        /// <param name="isSystem"></param>
        public void Create(string staffNr, int? operatorId, MessageRecordType type, string text, bool isSystem = true)
        {
            try
            {
                DataContext dc = new DataContext(this.DbString);
                bool isUniq = MessageRecordTypeHelper.UniqTypes.Contains(type);
                if (isUniq)
                {
                    MessageRecord unreadRecord = dc.Context.GetTable<MessageRecord>().FirstOrDefault(s => s.staffNr.Equals(staffNr) && s.isRead == false && s.messageType.Equals((int)type));
                    if (unreadRecord != null)
                    {
                        return;
                    }
                }

                bool isUrl = MessageRecordTypeHelper.UrlTypes.Contains(type);
                MessageRecord record = new MessageRecord()
                {
                    staffNr = staffNr,
                    messageType = (int)type,
                    text = text,
                    operatorId = operatorId,
                    isRead = false,
                    isHandled = false,
                    isUrl = isUrl
                };
                dc.Context.GetTable<MessageRecord>().InsertOnSubmit(record);
                dc.Context.SubmitChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Logger.Error("创建消息记录失败", ex);
            }
        }
    }
}
