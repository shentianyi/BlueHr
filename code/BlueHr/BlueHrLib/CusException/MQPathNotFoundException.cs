using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.CusException
{
  public  class MQPathNotFoundException : Exception
    {
        public MQPathNotFoundException() : base("系统任务队列未找到，请联系系统管理员！") { }
    
    
    }
}
