using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;


namespace BlueHrLib.Data
{
    public partial class LeaveRecord
    {
        public TimeSpan duration
        {
            get { return (this.leaveEnd-this.leaveStart).Duration(); }
        }
    }
}
