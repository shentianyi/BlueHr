using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class JobTitle
    {
        public string CertificateTypeStr
        {
            get { return string.Join<string>(";",this.JobCertificate.Select(s=>s.CertificateType.name).ToList()); }
        }
    }
}
