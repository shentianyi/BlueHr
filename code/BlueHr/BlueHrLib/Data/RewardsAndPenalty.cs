using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class RewardsAndPenalty
    {
        public string typeStr
        {
            get
            {
                return EnumHelper.GetDescription((RewardsAndPenaltyType)this.type);
            }
            set { }
        }

    }
}
