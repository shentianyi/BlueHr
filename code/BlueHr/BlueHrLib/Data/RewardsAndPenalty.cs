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
        IUserService ss = new UserService(Settings.Default.BlueHrConnectionString10);

        
        public string userName
        {
            get
            {
                return ss.FindById(Convert.ToInt32(this.createdUserId)).name;
            }
            set
            {
                this.userName = ss.FindById(Convert.ToInt32(this.createdUserId)).name;
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
                return ss.FindById(Convert.ToInt32(this.approvalUserId)).name;
            }
        }

    }
}
