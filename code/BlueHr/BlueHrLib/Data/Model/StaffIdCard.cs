using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Model
{
    public class StaffIdCard
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        public string id { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string ethnic { get; set; }
        public DateTime birthday { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public string residenceAddress { get; set; }
        /// <summary>
        /// 开始有效期
        /// </summary>
        public DateTime effectiveFrom { get; set; }
        /// <summary>
        /// 截止有效期
        /// </summary>
        public DateTime effectiveEnd { get; set; }
        /// <summary>
        /// 签发机构
        /// </summary>
        public string institution { get; set; }
        public string photo { get; set; }
    }
}
