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
        public string MQPath { get; set; }
        public bool IsRestartSvc { get; set; }

        public TaskDispatcher() { IsRestartSvc = false; }
        public TaskDispatcher(string mqPath) { this.MQPath = mqPath;IsRestartSvc = false; }
        public TaskDispatcher(string dbString, string mqPath) { this.DbString = dbString; this.MQPath = mqPath; IsRestartSvc = false; }

        private void SendMQMessage(TaskSetting ts)
        {
            if (!MessageQueue.Exists(this.MQPath))
            {
                throw new MQPathNotFoundException();
            }

            MessageQueue mq = new MessageQueue(this.MQPath);
            Message msg = new Message();
            msg.Body = ts;
            msg.Formatter = new XmlMessageFormatter(new Type[1] { typeof(TaskSetting) });
            mq.Send(msg);
        }

        /// <summary>
        /// 获取消息
        /// </summary>
        public void FetchMQMessage()
        {
            MessageQueue mq = new MessageQueue(this.MQPath);
            mq.Formatter = new XmlMessageFormatter(new Type[1] { typeof(TaskSetting) });
            Message msg = mq.Receive();

            if (msg != null)
            { 
                TaskSetting ts = msg.Body as TaskSetting;
                this.Dispatch(ts);
            }

        }

        /// <summary>
        /// 分发任务
        /// </summary>
        /// <param name="ts"></param>
        public void Dispatch(TaskSetting ts)
        {
            ITaskRoundService trs = new TaskRoundService(this.DbString);
            TaskRound taskRound = null;
            try
            {
                if (ts.LogTaskRound)
                {
                    taskRound = trs.Create(ts.TaskType);
                }
                LogUtil.Logger.Info("获取到任务信息：");
                LogUtil.Logger.Info(ts.TaskType);
                LogUtil.Logger.Info(ts.JsonParameter);
                switch (ts.TaskType)
                {
                    case TaskType.CalAtt:
                        CalAttParameter calAtt = JSONHelper.parse<CalAttParameter>(ts.JsonParameter);
                        IAttendanceRecordService ars = new AttendanceRecordService(this.DbString);
                        //ars.CalculateAttendRecord(calAtt.AttCalculateDateTime, calAtt.ShiftCodes);
                         ars.CalculateAttendRecordWithExtrawork(calAtt.AttCalculateDateTime);
                        // add send email to queue
                        SendAttWarnEmailMessage(calAtt.AttCalculateDateTime, calAtt.ShiftCodes);
                        // create message record
                        SendAttWarnMsgRecordMessage(calAtt.AttCalculateDateTime, calAtt.ShiftCodes);
                        break;
                    case TaskType.SendMail:
                        break;
                    case TaskType.SendAttExceptionMail:
                        AttWarnEmailParameter attWarn = JSONHelper.parse<AttWarnEmailParameter>(ts.JsonParameter);
                        IAttendanceRecordCalService arcs = new AttendanceRecordCalService(this.DbString);
                        arcs.SendWarnEmail(attWarn.AttWarnDate);
                        break;
                    case TaskType.ToFullMemeberWarn:
                        IMessageRecordService mrs = new MessageRecordService(this.DbString);
                        mrs.CreateToFullMemberMessage(ts.TaskCreateAt.Date);
                        break;
                    case TaskType.AttExceptionWarn:
                        AttWarnParameter attWarnP = JSONHelper.parse<AttWarnParameter>(ts.JsonParameter);
                        IMessageRecordService mrss = new MessageRecordService(this.DbString);
                        mrss.CreateAttExceptionMessage(attWarnP.AttWarnDate);
                        break;
                    case TaskType.ReStartSvc:
                        this.IsRestartSvc = true;
                        break;
                    default:
                        throw new TaskTypeNotSupportException();
                }
                if (ts.LogTaskRound && taskRound != null)
                {
                    trs.FinishTaskByUniqId(taskRound.uuid, "任务运行成功");
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}: {1}", ex.Message, ex.StackTrace);

                LogUtil.Logger.Error("任务执行错误：", ex);

                try
                {
                    if (ts.LogTaskRound && taskRound != null)
                    {
                        trs.FinishTaskByUniqId(taskRound.uuid, msg, true);
                    }
                }
                catch
                {
                }
                throw new Exception(msg, ex);
            }
        }

        /// <summary>
        /// 发送计算考勤的消息
        /// </summary>
        public void SendCalculateAttMessage(DateTime calculateAt, List<string> shiftCodes = null)
        {
            CalAttParameter calAttParam = new CalAttParameter()
            {
                AttCalculateDateTime = calculateAt,
                ShiftCodes = shiftCodes
            };

            TaskSetting task = new TaskSetting()
            {
                TaskCreateAt = DateTime.Now,
                TaskType = TaskType.CalAtt,
                JsonParameter = JSONHelper.stringify(calAttParam),
                LogTaskRound=true
            };

            SendMQMessage(task);
        }

        /// <summary>
        /// 发送考勤异常的邮件消息
        /// </summary>
        /// <param name="calculateAt"></param>
        public void SendAttWarnEmailMessage(DateTime calculateAt, List<string> shiftCodes)
        {
            AttWarnEmailParameter attWarnParam = new AttWarnEmailParameter()
            {
                AttWarnDate = calculateAt,
                ShiftCodes = shiftCodes
            };
            TaskSetting task = new TaskSetting()
            {
                TaskCreateAt = DateTime.Now,
                TaskType = TaskType.SendAttExceptionMail,
                JsonParameter = JSONHelper.stringify(attWarnParam),
                LogTaskRound = false
            };
            SendMQMessage(task);
            //IShiftScheduleService sss = new ShiftSheduleService(this.DbString);
            //List<ShiftScheduleView> shifts = sss.GetDetailViewByDateTime(calculateAt, shiftCodes);
            //if (shifts.Count > 0)
            //{
            //    List<DateTime> datetimes = shifts.Select(s => s.scheduleAt).Distinct().ToList();
            //    foreach (var dt in datetimes)
            //    {
            //        AttWarnEmailParameter attWarnParam = new AttWarnEmailParameter()
            //        {
            //            AttWarnDate = dt,
            //            ShiftCodes = shiftCodes
            //        };


            //        TaskSetting task = new TaskSetting()
            //        {
            //            TaskCreateAt = DateTime.Now,
            //            TaskType = TaskType.SendAttExceptionMail,
            //            JsonParameter = JSONHelper.stringify(attWarnParam),
            //            LogTaskRound = false
            //        };

            //        SendMQMessage(task);
            //    }
            //}
        }


        /// <summary>
        /// 发送考勤异常的消息记录消息
        /// </summary>
        /// <param name="calculateAt"></param>
        public void SendAttWarnMsgRecordMessage(DateTime calculateAt, List<string> shiftCodes)
        {
            AttWarnEmailParameter attWarnParam = new AttWarnEmailParameter()
            {
                AttWarnDate = calculateAt,
                ShiftCodes = shiftCodes
            };
            TaskSetting task = new TaskSetting()
            {
                TaskCreateAt = DateTime.Now,
                TaskType = TaskType.AttExceptionWarn,
                JsonParameter = JSONHelper.stringify(attWarnParam),
                LogTaskRound = false
            };
            SendMQMessage(task);
            //IShiftScheduleService sss = new ShiftSheduleService(this.DbString);
            //List<ShiftScheduleView> shifts = sss.GetDetailViewByDateTime(calculateAt, shiftCodes);
            //if (shifts.Count > 0)
            //{
            //    List<DateTime> datetimes = shifts.Select(s => s.scheduleAt).Distinct().ToList();
            //    foreach (var dt in datetimes)
            //    {
            //        AttWarnParameter attWarnParam = new AttWarnParameter()
            //        {
            //            AttWarnDate = dt,
            //            ShiftCodes = shiftCodes
            //        };


            //        TaskSetting task = new TaskSetting()
            //        {
            //            TaskCreateAt = DateTime.Now,
            //            TaskType = TaskType.AttExceptionWarn,
            //            JsonParameter = JSONHelper.stringify(attWarnParam),
            //            LogTaskRound = false
            //        };

            //        SendMQMessage(task);
            //    }
            //}
        }



        /// <summary>
        /// 发送转正提醒消息
        /// </summary>
        public void SendToBeFullMemberMsgRecordMessage()
        {
            TaskSetting task = new TaskSetting()
            {
                TaskType = TaskType.ToFullMemeberWarn
            };

            SendMQMessage(task);
        }

        /// <summary>
        /// 发送后台服务重启消息
        /// </summary>
        public void SendRestartSvcMessage()
        {
            TaskSetting task = new TaskSetting()
            {
                TaskType = TaskType.ReStartSvc,
                LogTaskRound = false
            };

            SendMQMessage(task);
        }

    }
}
