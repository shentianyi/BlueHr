using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrClient.Escape
{
    public class Escaper
    {
        public string NationEscape(string nation)
        {
            string nationCN = "";
            switch (nation)
            {
                case "01":
                    nationCN = "汉族";
                    break;
            }
            return nationCN;
        }
        public string SexEscape(string sex)
        {
            string sexCN = "";
            switch (sex) {
                case "2":
                    sexCN = "男";
                    break;
                default:
                    sexCN = "女";
                    break;
            }
            return sexCN;
        }
    }
}
