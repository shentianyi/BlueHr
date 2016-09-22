using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data
{
    public partial class WorkAndRest
    {
        public string dateTypeDisplay
        {
            get
            {
                return EnumHelper.GetDescription((WorkAndRestType)this.dateType);
            }
        }

        public bool IsWorkDay
        {
            get
            { return this.dateType == (int)WorkAndRestType.WorkDay; }
        }
    }
}
