using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSqlPr;
using SitePr;

namespace TestForAvailability
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void There_Is_No_Site_Name()
        {
            string SiteName = "";
            string Expected = "www.ya.ru";
            Sites site = new Sites();

            site.CheckAvailability();

            Assert.AreEqual(Expected,, 0.001,"The programm does not work correctly");
        }
    }
}
