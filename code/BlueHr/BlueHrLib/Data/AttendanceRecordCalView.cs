﻿using BlueHrLib.Data.Enum;
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
                return this.exceptionCodes.Split(',').ToList();
            }
        }

        public string exceptionStr
        {
            get
            {
                string es = string.Empty;
                List<string> strs = this.exceptionStrs;
                foreach(var s in strs)
                {
                    es += EnumHelper.GetDescriptionByFiledName(s,typeof(AttendanceExceptionType))+";";
                }
                return es;
            }
        }
    }
}
