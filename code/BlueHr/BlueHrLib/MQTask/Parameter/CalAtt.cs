﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.MQTask.Parameter
{
    [Serializable]
    public class CalAtt
    {
        public DateTime AttDateTime { get; set; }
        public List<string> ShiftCodes { get; set; }
    }
}
