using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.CusException
{
    public class SystemSettingNotSetException : Exception
    {
        public SystemSettingNotSetException() : base("系统设置未初始化，请联系系统管理员！") { }
    }
}
