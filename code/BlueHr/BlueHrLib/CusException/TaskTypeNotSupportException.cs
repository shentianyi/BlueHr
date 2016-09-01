using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.CusException
{
  public  class TaskTypeNotSupportException : Exception
    {
        public TaskTypeNotSupportException() : base("任务类型不支持，请联系系统管理员！") { }
    
    
    }
}
