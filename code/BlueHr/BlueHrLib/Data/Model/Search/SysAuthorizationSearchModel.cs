using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Search
{
    public class SysAuthorizationSearchModel
    {
        //模块
        public string funCode { get; set; }

        //名称
        public string Name { get; set; }

        //控制器
        public string controlName { get; set; }

        //动作
        public string actionName { get; set; }
    }
}
