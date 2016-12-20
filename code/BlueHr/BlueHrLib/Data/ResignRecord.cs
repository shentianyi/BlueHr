﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;
using BlueHrLib.Data.Repository.Interface;

namespace BlueHrLib.Data
{
    public partial class ResignRecord
    {
        public string userName
        {
            get
            {
                return this.User.name;
            }
        }
        public string resignTypeDisplay
        {
            get
            {
                return this.ResignType.name;
            }
        }

    }
}
