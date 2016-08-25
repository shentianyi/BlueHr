using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Service
{
    public class ServiceBase
    {
        private string dbString;
        public string DbString { get { return dbString; } }

        public ServiceBase(string dbString)
        {
            this.dbString = dbString;
        }

    }
}
