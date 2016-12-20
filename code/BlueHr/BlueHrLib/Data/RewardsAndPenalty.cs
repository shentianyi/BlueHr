using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Repository;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Helper;
using BlueHrLib.Properties;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class RewardsAndPenalty
    {
        public string userName
        {
            get
            {
                return this.User.name;
            }
        }
        public string typeStr
        {
            get
            {
                return EnumHelper.GetDescription((RewardsAndPenaltyType)this.type);
            }
            set { }
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
