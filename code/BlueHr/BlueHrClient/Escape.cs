using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrClient.Escape
{
    public class Escaper
    {
        public string SexEscape(string sex)
        {
            string sexCN = "";
            switch (sex)
            {
                case "1":
                    sexCN = "男";
                    break;
                default:
                    sexCN = "女";
                    break;
            }
            return sexCN;
        }
        public string SexEscapeForUpdate(string sex)
        {
            string sexCN = "";
            switch (sex)
            {
                case "男":
                    sexCN = "0";
                    break;
                default:
                    sexCN = "1";
                    break;
            }
            return sexCN;
        }
        public string DateEscape(string date)
        {
            // return DateTime.ParseExact(date, "yyyyMMdd", new CultureInfo("zh-CN"), DateTimeStyles.AllowWhiteSpaces).ToString();
            date = date.Insert(4, ".");
            date = date.Insert(7, ".");
            return date;
        }
        public string NationEscape(string nation)
        {
            string nationCN = "";
            switch (nation)
            {
                case "01":
                    nationCN = "汉族";
                    break;
                case "02":
                    nationCN = "蒙古族";
                    break;
                case "03":
                    nationCN = "回族";
                    break;
                case "04":
                    nationCN = "藏族";
                    break;
                case "05":
                    nationCN = "维吾尔族";
                    break;
                case "06":
                    nationCN = "苗族";
                    break;
                case "07":
                    nationCN = "彝族";
                    break;
                case "08":
                    nationCN = "壮族";
                    break;
                case "09":
                    nationCN = "布依族";
                    break;
                case "10":
                    nationCN = "朝鲜族";
                    break;
                case "11":
                    nationCN = "满族";
                    break;
                case "12":
                    nationCN = "侗族";
                    break;
                case "13":
                    nationCN = "瑶族";
                    break;
                case "14":
                    nationCN = "白族";
                    break;
                case "15":
                    nationCN = "土家族";
                    break;
                case "16":
                    nationCN = "哈尼族";
                    break;
                case "17":
                    nationCN = "哈萨克族";
                    break;
                case "18":
                    nationCN = "傣族";
                    break;
                case "19":
                    nationCN = "黎族";
                    break;
                case "20":
                    nationCN = "傈僳族";
                    break;
                case "21":
                    nationCN = "佤族";
                    break;
                case "22":
                    nationCN = "畲族";
                    break;
                case "23":
                    nationCN = "高山族";
                    break;
                case "24":
                    nationCN = "拉祜族";
                    break;
                case "25":
                    nationCN = "水族";
                    break;
                case "26":
                    nationCN = "东乡族";
                    break;
                case "27":
                    nationCN = "纳西族";
                    break;
                case "28":
                    nationCN = "景颇族";
                    break;
                case "29":
                    nationCN = "柯尔克孜族";
                    break;
                case "30":
                    nationCN = "土族";
                    break;
                case "31":
                    nationCN = "达斡尔族";
                    break;
                case "32":
                    nationCN = "仫佬族";
                    break;
                case "33":
                    nationCN = "羌族";
                    break;
                case "34":
                    nationCN = "布朗族";
                    break;
                case "35":
                    nationCN = "撒拉族";
                    break;
                case "36":
                    nationCN = "毛难族";
                    break;
                case "37":
                    nationCN = "仡佬族";
                    break;
                case "38":
                    nationCN = "锡伯族";
                    break;
                case "39":
                    nationCN = "阿昌族";
                    break;
                case "40":
                    nationCN = "普米族";
                    break;
                case "41":
                    nationCN = "塔吉克族";
                    break;
                case "42":
                    nationCN = "怒族";
                    break;
                case "43":
                    nationCN = "乌孜别克族";
                    break;
                case "44":
                    nationCN = "俄罗斯族";
                    break;
                case "45":
                    nationCN = "鄂温克族";
                    break;
                case "46":
                    nationCN = "崩龙族";
                    break;
                case "47":
                    nationCN = "保安族";
                    break;
                case "48":
                    nationCN = "裕固族";
                    break;
                case "49":
                    nationCN = "京族";
                    break;
                case "50":
                    nationCN = "塔塔尔族";
                    break;
                case "51":
                    nationCN = "独龙族";
                    break;
                case "52":
                    nationCN = "鄂伦春族";
                    break;
                case "53":
                    nationCN = "赫哲族";
                    break;
                case "54":
                    nationCN = "门巴族";
                    break;
                case "55":
                    nationCN = "珞巴族";
                    break;
                case "56":
                    nationCN = "基诺族";
                    break;
                case "57":
                    nationCN = "其他";
                    break;
                default:
                    nationCN = "外国血统";
                    break;
            }
            return nationCN;
        }
       
    }
}
