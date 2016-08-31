using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.CusException
{
    public class SystemCertificateTypeNotFoundException : Exception
    {
        public SystemCertificateTypeNotFoundException() : base("系统默认证照类别未初始化，请联系系统管理员！") { }
    }
}
