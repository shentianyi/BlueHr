using System;
using BlueHrLib.Data;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrLibTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueHrLibTests.Service.Implement
{
    [TestClass]
    public class JobTitleServiceTest
    {
        [TestMethod]
        public void Test_CreateJobTitle()
        {
            IJobTitleService service = new JobTitleService(Settings.Default.connString);
            var job1 = new JobTitle()
            {
                name = "高级工程师",
                remark = "高级工程师"
            };
            Assert.IsTrue(service.Create(job1));

            var job2 = new JobTitle()
            {
                name = "中级工程师",
                remark = "中级工程师"
            };
            Assert.IsTrue(service.Create(job2));

        }

        [TestMethod]
        public void Test_UpdateJobTitle()
        {
            IJobTitleService service = new JobTitleService(Settings.Default.connString);

            var job = service.FindById(1);

            job.name = "高级工程师@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            job.remark = "高级工程师@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Assert.IsTrue(service.Update(job,""));
        }
    }
}
