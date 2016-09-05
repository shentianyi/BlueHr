using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.MQTask.Parameter
{

    [Serializable]
    public class AttWarnEmailParameter
    {
        /// <summary>
        /// 存在异常的日期
        /// </summary>
        public DateTime AttWarnDate { get; set; }
        
    }
}
