using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTask;
using SiteDLL;

namespace TestForAvailability
{
    [TestClass]
    public class TestOfAccessProgram
    {
        [TestMethod]
        public void There_Is_No_Site_Name()
        {
            string SiteName = "www.ya.ru"; //87.250.250.242
            string Expected = "87.250.250.242";
            Sites site = new Sites(SiteName);

            site.CheckAvailability();

            string actual = site.SitePingData;
            Assert.AreEqual(Expected, actual, "The programm does not work correctly");
        }
    }
}
