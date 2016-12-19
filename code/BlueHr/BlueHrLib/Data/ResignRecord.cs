using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Enum;
using BlueHrLib.Helper;
using BlueHrLib.Data.Repository.Interface;

namespace BlueHrLib.Data
{
    public partial class ResignRecord
    {
        private IUserRepository userRep;
        private IResignTypeRepository resignTypeRep;
        public string userName
        {
            get
            {
                return userRep.FindById(Convert.ToInt32(this.userId)).name;
            }
        }
        public string resignTypeDisplay
        {
            get
            {
                return resignTypeRep.FindById(Convert.ToInt32(this.resignTypeId)).name;
            }
        }

    }
}
