using BlueHrLib.Helper;
using BlueHrLib.MQTask.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.MQTask
{
    public class TaskDispatcher
    {
        public void Dispatch(TaskSetting ts)
        {
            switch (ts.TaskType)
            {
                case "CalAtt":
                    CalAtt calAtt = JSONHelper.parse<CalAtt>(ts.JsonParameter);

                    break;
                case "SendMail":
                default:
                    break;
            }
        }
    }
}
