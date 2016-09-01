using BlueHrLib.Helper;
using BlueHrLib.MQTask.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Service.Interface;
using BlueHrLib.Service.Implement;
using BlueHrLib.Data;
using BlueHrLib.CusException;
using System.Messaging;
using Brilliantech.Framwork.Utils.LogUtil;

namespace BlueHrLib.MQTask
{
    public class TaskDispatcher
    {
        public string DbString { get; set; }
        public string MQPath{get;set;}

        public TaskDispatcher() { }
        public TaskDispatcher(string mqPath) { this.MQPath = mqPath; }
        public TaskDispatcher(string dbString,string mqPath) { this.DbString = dbString; this.MQPath=mqPath;}

        public void SendMQMessage(TaskSetting ts)
        {
            if (!MessageQueue.Exists(this.MQPath))
            {
                throw new MQPathNotFoundException();
            }

            MessageQueue mq = new MessageQueue(this.MQPath);
            Message msg = new Message();
            msg.Body = ts;
            msg.Formatter= new XmlMessageFormatter(new Type[1] { typeof(TaskSetting) });
            mq.Send(msg);
        }

        public void FetchMQMessage()
        {
            MessageQueue mq = new MessageQueue(this.MQPath);
            mq.Formatter = new XmlMessageFormatter(new Type[1] { typeof(TaskSetting) });
            Message msg = mq.Receive();

            if (msg != null)
            {
                LogUtil.Logger.Info("获取到任务信息：");
                LogUtil.Logger.Info(msg);
                TaskSetting ts = msg.Body as TaskSetting;
                this.Dispatch(ts);
            }

        }

        public void Dispatch(TaskSetting ts)
        {
            ITaskRoundService trs = new TaskRoundService(this.DbString);
            TaskRound taskRound = null;
            try
            {
                  taskRound = trs.Create(ts.TaskType);

                switch (ts.TaskType)
                {
                    case TaskType.CalAtt:
                        CalAtt calAtt = JSONHelper.parse<CalAtt>(ts.JsonParameter);
                        IAttendanceRecordService ars = new AttendanceRecordService(this.DbString);
                        ars.CalculateAttendRecord(calAtt.AttDateTime, calAtt.ShiftCodes);
                        break;
                    case TaskType.SendMail:
                        break;
                    default:
                        throw new TaskTypeNotSupportException();
                }
                trs.FinishTaskByUniqId(taskRound.uuid, "任务运行成功");
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}: {1}", ex.Message, ex.StackTrace);
                try
                {
                    if (taskRound != null)
                    {
                        trs.FinishTaskByUniqId(taskRound.uuid,msg,true);
                    }
                }
                catch
                {
                }
                throw new Exception(msg, ex);
            }
        }
    }
}
