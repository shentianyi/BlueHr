using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Enum
{
    public enum SearchConditions
    {
        [Description("大于")]
        GT = 1,

        [Description("小于")]
        LT = 2,

        [Description("等于")]
        Equals = 3,

        [Description("大于等于")]
        GTE = 4,

        [Description("小于等于")]
        LTE = 5,

        [Description("包含")]
        Contains = 6,
       
        [Description("不包含")]
        NotContains = 7,

        [Description("中间匹配")]
        CenterLike = 8,

        [Description("左匹配")]
        LeftLike = 9,

        [Description("右匹配")]
        RightLike = 9,

    }
}
