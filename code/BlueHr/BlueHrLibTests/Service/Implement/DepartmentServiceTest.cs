using System;
using BlueHrLib.Data;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrLibTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueHrLibTests.Service.Implement
{
    [TestClass]
    public class DepartmentServiceTest
    {
        [TestMethod]
        public void Test_CreateDepartment()
        {
            IDepartmentService service = new DepartmentService(Settings.Default.connString);
            Department dep1 = new Department()
            {
                name = "工程部",
                remark = "工程部们",
                companyId = 1
            };
            Assert.IsTrue(service.Create(dep1));

            Department dep2 = new Department()
            {
                name = "人事部",
                remark = "人事部门",
                companyId = 2
            };
            Assert.IsTrue(service.Create(dep2));

        }

        [TestMethod]
        public void Test_UpdateDepartment()
        {
            IDepartmentService service = new DepartmentService(Settings.Default.connString);

            Department dep1 = service.FindById(1);

            dep1.name = "工程部@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dep1.remark = "工程部@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Assert.IsTrue(service.Update(dep1));
        }
    }
}
