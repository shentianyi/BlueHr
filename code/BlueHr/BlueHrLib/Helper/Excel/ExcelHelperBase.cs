using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlueHrLib.Helper.Excel
{
    public class ExcelHelperBase
    {
        public string DbString { get; set; }
        public string FilePath { get; set; }

        public ExcelHelperBase() { }

        public ExcelHelperBase(string dbString)
        {
            this.DbString = dbString;
        }

        public ExcelHelperBase(string dbString, string filePath)
        {
            this.DbString = dbString;
            this.FilePath = filePath;
        }

         

       

       
    }
}
