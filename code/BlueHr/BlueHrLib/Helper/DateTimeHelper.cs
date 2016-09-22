using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Helper
{
    public class DateTimeHelper
    {
        public static string GetShortDayOfWeek(DateTime dateTime)
        {
            switch (dateTime.DayOfWeek.ToString("D"))
            {
                case "0":
                    return "日";
                case "1":
                    return "一";
                case "2":
                    return "二";
                case "3":
                    return "三";
                case "4":
                    return "四";
                case "5":
                    return "五";
                case "6":
                    return "六";
            }
            return null;
        }


        // <summary>
        /// 返回日期几
        /// </summary>
        /// <returns></returns>
        public static string GetDayOfWeek(DateTime dateTime)
        {

            switch (dateTime.DayOfWeek.ToString("D"))
            {
                case "0":
                    return "星期日";
                case "1":
                    return "星期一";
                case "2":
                    return "星期二";
                case "3":
                    return "星期三";
                case "4":
                    return "星期四";
                case "5":
                    return "星期五";
                case "6":
                    return "星期六";
            }
            return null;
        }
    }
}
