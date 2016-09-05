using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.MQTask.Parameter
{
    [Serializable]
    public class CalAttParameter
    {
        /// <summary>
        /// 计算时间
        /// </summary>
        public DateTime AttDateTime { get; set; }

        /// <summary>
        /// 班次列表
        /// </summary>
        public List<string> ShiftCodes { get; set; }
    }
}
