using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;
using BlueHrLib.MQTask;

namespace BlueHrLib.Data
{
  public partial  class TaskRound
    {
        public string statusDisplay
        {
            get
            {
                return EnumHelper.GetDescription((TaskRoundStatus)this.taskStatus);
            }
        }

        public string typeDisplay
        {
            get{
                return EnumHelper.GetDescription((TaskType)this.taskType); 
            }
        }
    }
}
