using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.CusException
{
  public  class WorkAndRestNotFoundException : Exception
    {
        public WorkAndRestNotFoundException() : base("作息表未设置，请联系系统管理员！") { }

        public WorkAndRestNotFoundException(DateTime datetime) : base(string.Format("{0}的作息表未设置，请联系系统管理员！",datetime.ToString("yyyy-MM-dd"))) { }


    }
}
