using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.CusException
{
  public  class TaskRoundNotFoundException : Exception
    {
        public TaskRoundNotFoundException() : base("任务记录未找到，请联系系统管理员！") { }
    
    
    }
}
