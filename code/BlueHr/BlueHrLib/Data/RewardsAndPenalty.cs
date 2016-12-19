﻿using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class RewardsAndPenalty
    {
        private IUserRepository userRep;

        public string typeStr
        {
            get
            {
                return EnumHelper.GetDescription((RewardsAndPenaltyType)this.type);
            }
            set { }
        }
        public string userName
        {
            get
            {
                return userRep.FindById(Convert.ToInt32(this.createdUserId)).name;
            }
        }

        public string approvalUserName
        {
            get
            {
                return userRep.FindById(Convert.ToInt32(this.approvalUserId)).name;
            }
        }

    }
}
