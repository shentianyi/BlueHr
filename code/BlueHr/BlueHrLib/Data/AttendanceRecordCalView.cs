using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data
{
    public partial class AttendanceRecordCalView
    {
      
        public string attendanceDateStr
        {
            get { return this.attendanceDate.ToString("yyyy-MM-dd"); }
        }

        public string isExceptionStr
        {
            get { return this.isException ? "是" : "否"; }
        }
        
        public string isExceptionHandledStr
        {
            get { return this.isExceptionHandled.HasValue ? (this.isExceptionHandled.Value ? "是" : "否") : string.Empty; }
        }

        public double oriWorkingHourRound
        {
            get { return Math.Round(this.oriWorkingHour, 1); }
        }

        public double actWorkingHourRound
        {
            get { return Math.Round(this.actWorkingHour, 1); }
        }

        /// <summary>
        /// 有可能为空， 如果为空，默认填写0
        /// </summary>
        public double oriExtraWorkingHourRound
        {
            get { return Math.Round(this.oriExtraWorkingHour.Value, 1); }
        }

        public double actExtraWorkingHourRound
        {
            get { return Math.Round(this.actExtraWorkingHour.Value, 1); }
        }

        public List<string> exceptionStrs
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.exceptionCodes)? null: this.exceptionCodes.Split(',').ToList();
            }
        }

        public string exceptionStr
        {
            get
            {
                string es = string.Empty;
                List<string> strs = this.exceptionStrs;

                if (strs != null)
                {
                    foreach (var s in strs)
                    {
                        es += EnumHelper.GetDescriptionByFiledName(s, typeof(AttendanceExceptionType)) + ";";
                    }
                }
                
                return es;
            }
        }

        public string extraWorkTypeDisplay
        {
            get
            {
                return this.extraworkType.HasValue ? EnumHelper.GetDescription((SystemExtraType)this.extraworkType) :  string.Empty;
            }
        }
    }
}
