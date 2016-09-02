using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.CusException
{
  public  class DataNotFoundException : Exception
    {
        public DataNotFoundException() : base("系统数据未找到，请联系系统管理员！") { }
    
    
    }
}
