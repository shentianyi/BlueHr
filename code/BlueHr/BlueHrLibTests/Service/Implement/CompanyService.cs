using BlueHrLib.Data;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrLibTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueHrLibTests.Service.Implement
{
    [TestClass()]
    public class CompanyServiceTest
    {
        [TestMethod]
        public void Test_CreateCompany()
        {
            ICompanyService service = new CompanyService(Settings.Default.connString);
            Company cpy1 = new Company()
            {
                name = "中国电信",
                remark = "电信公司",
                address = "上海浦东"
            };
            Assert.IsTrue(service.Create(cpy1));
            Company cpy2 = new Company()
            {
                name = "中国移动",
                remark = "移动公司",
                address = "上海浦东"
            };
            Assert.IsTrue(service.Create(cpy2));
        }
    }
}
