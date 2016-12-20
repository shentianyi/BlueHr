using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;
using BlueHrLib.Data.Repository.Interface;

namespace BlueHrLib.Data
{
    public partial class LeaveRecord
    {
        private IUserRepository userRep;

        public TimeSpan? duration
        {
            get { return (this.leaveEnd-this.leaveStart); }
        }
        public string userName
        {
            get
            {
                return this.User.name;
            }
        }

        public string approvalUserName
        {
            get
            {
                return this.User1.name;
            }
        }
    }
}
