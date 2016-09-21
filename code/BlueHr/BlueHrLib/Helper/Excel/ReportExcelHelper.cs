using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Excel;
using BlueHrLib.Data.Enum;

namespace BlueHrLib.Helper.Excel
{
    public class ReportExcelHelper : ExcelHelperBase
    {
        public ReportExcelHelper() { }

        public ReportExcelHelper(string dbString) : base(dbString)
        {
            this.DbString = dbString;
        }



        public ReportMessage ExportOriginExtraSumReport(DateTime startDate, DateTime endDate, StaffSearchModel searchModel)
        {

            ReportMessage msg = new ReportMessage();
            string fileName = "考勤加班原始总表.xlsx";
            try
            {
                IStaffService ss = new StaffService(this.DbString);


                string tmpFile = FileHelper.CreateFullTmpFilePath(fileName, true);
                msg.Content = FileHelper.GetDownloadTmpFilePath(tmpFile);







                msg.Success = true;
            }
            catch (Exception ex)
            {
                msg.Content = ex.Message;
            }

            return msg;
        }
        
    }
}
