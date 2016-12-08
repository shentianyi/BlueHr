using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Enum
{
    /// <summary>
    /// 班次类型
    /// </summary>
    public enum RewardsAndPenaltyType
    {
        [Description("奖励")]
        Reward = 0,

        [Description("惩罚")]
        Penality = 1
    }
}
