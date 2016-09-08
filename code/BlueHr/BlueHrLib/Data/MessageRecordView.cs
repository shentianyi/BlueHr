using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class MessageRecordView
    {
        public string createdAtStr
        {
            get { return this.createdAt.Value.ToString("yyyy-MM-dd HH:mm"); }
        }
    }
}
