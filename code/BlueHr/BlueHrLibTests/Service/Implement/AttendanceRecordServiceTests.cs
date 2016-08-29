using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueHrLib.Service.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLibTests.Properties;

namespace BlueHrLib.Service.Implement.Tests
{
    [TestClass()]
    public class AttendanceRecordServiceTests
    {
        [TestMethod()]

        public void CalculateAttendeRcordTest()
        {
            AttendanceRecordService ars = new AttendanceRecordService(Settings.Default.db);
            ars.CalculateAttendRecord(DateTime.Now.Date);
          
        }
    }
}