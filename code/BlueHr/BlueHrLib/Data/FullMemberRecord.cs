using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;
using BlueHrLib.Service.Implement;
using BlueHrLib.Helper.Excel;

namespace BlueHrLib.Data
{
    public partial class FullMemberRecord
    {

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
