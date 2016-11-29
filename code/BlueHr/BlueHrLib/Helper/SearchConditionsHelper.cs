using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Helper
{
    public class SearchConditionsHelper
    {
        public static string GetStrWhere(string Table, string AllTableName, string SearchConditions, string SearchValue)
        {
            string strWhere = string.Empty;
            switch (SearchConditions)
            {
                case "1":
                    strWhere = "Select s from "+ Table + " as s where s." + AllTableName + ">" + SearchValue;
                    break;
                case "2":
                    strWhere = "Select s from " + Table + " as s where s." + AllTableName + "<" + SearchValue;
                    break;
                case "3":
                    strWhere = "Select s from " + Table + " as s where s." + AllTableName + "=" + SearchValue;
                    break;
                case "4":
                    strWhere = "Select s from " + Table + " as s where s." + AllTableName + ">=" + SearchValue;
                    break;
                case "5":
                    strWhere = "Select s from " + Table + " as s where s." + AllTableName + "<=" + SearchValue;
                    break;
                case "6":
                    strWhere = "Select s from " + Table + " as s where Contains(s." + AllTableName + ", '" + SearchValue + "')";
                    break;
                case "7":
                    strWhere = "Select s from " + Table + " as s where Contains(s." + AllTableName + ", '" + SearchValue + "')";
                    break;
                case "8":
                    strWhere = "Select s from " + Table + " as s where s." + AllTableName + " like '%" + SearchValue + "%'";
                    break;
                case "9":
                    strWhere = "Select s from " + Table + " as s where s." + AllTableName + " like '%" + SearchValue + "'";
                    break;
                case "10":
                    strWhere = "Select s from " + Table + " as s where s." + AllTableName + " like '" + SearchValue + "%'";
                    break;
                default:
                    break;
            }
            return strWhere;
        }

    }
}
