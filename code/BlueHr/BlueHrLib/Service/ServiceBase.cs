using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;

namespace BlueHrLib.Service
{
    public class ServiceBase
    {
        private string dbString;

        private DataContext _context;

        private static object syncObj = new object();

        public string DbString { get { return dbString; } }

        public DataContext Context
        {
            get
            {
                if (null == _context)
                {
                    lock (syncObj)
                    {
                        if (null == _context)
                        {
                            _context = new DataContext(this.DbString);
                        }
                    }
                }
                return _context;
            }
        }

        public ServiceBase(string dbString)
        {
            this.dbString = dbString;
        }
    }
}
