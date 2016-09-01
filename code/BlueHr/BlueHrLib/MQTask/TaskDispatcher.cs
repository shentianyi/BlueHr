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

namespace BlueHrLib.MQTask
{
    public class TaskDispatcher
    {
        public string DbString { get; set; }

        public TaskDispatcher() { }
        public TaskDispatcher(string dbString) { this.DbString = dbString; }

        public void Dispatch(TaskSetting ts)
        {
            ITaskRoundService trs = new TaskRoundService(this.DbString);
            TaskRound taskRound = trs.Create(ts.TaskType);

            try
            {
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
                try
                {
                    trs.FinishTaskByUniqId(taskRound.uuid, string.Format("{0}: {1}", ex.Message, ex.StackTrace));
                }
                catch
                {
                }
                throw ex;
            }
        }
    }
}
