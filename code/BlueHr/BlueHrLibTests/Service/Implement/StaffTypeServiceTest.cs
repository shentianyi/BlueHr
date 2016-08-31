using System;
using System.Linq;
using BlueHrLib.Data;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrLibTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueHrLibTests.Service.Implement
{
    [TestClass]
    public class StaffTypeServiceTest
    {
        [TestMethod]
        public void Test_CreateStaffType()
        {
            IStaffTypeService service = new StaffTypeService(Settings.Default.connString);
            StaffType st1 = new StaffType()
            {
                name = "实习员工",
                remark = "实习员工"
            };
            Assert.IsTrue(service.Create(st1));

            StaffType st2 = new StaffType()
            {
                name = "在职员工",
                remark = "在职员工"
            };
            Assert.IsTrue(service.Create(st2));
            StaffType st3 = new StaffType()
            {
                name = "离职员工",
                remark = "离职员工"
            };
            Assert.IsTrue(service.Create(st3));
        }

        [TestMethod]
        public void Test_UpdateStaffType()
        {
            IStaffTypeService service = new StaffTypeService(Settings.Default.connString);
            StaffType st1 = service.FindAll().FirstOrDefault();
            if (st1 != null)
            {
                st1.name = "name@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                st1.remark = "remark@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            Assert.IsTrue(service.Update(st1));
        }
    }
}
