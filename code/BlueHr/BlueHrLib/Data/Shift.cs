using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;

namespace BlueHrLib.Data
{
   public partial class Shift
    {
        public string shiftTypeStr {
            get { return EnumHelper.GetDescription((ShiftType)this.shiftType); }
        }
    }
}
