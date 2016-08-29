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
<<<<<<< HEAD
        public void CalculateAttendRecordTest()
=======
        public void CalculateAttendeRcordTest()
>>>>>>> 6ee768bbd614d9fa4785334d74082a0ee6f947bf
        {
            AttendanceRecordService ars = new AttendanceRecordService(Settings.Default.db);
            ars.CalculateAttendRecord(DateTime.Now.Date);
          
        }
    }
}