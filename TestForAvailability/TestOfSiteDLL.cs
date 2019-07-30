using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiteDLL;
using Unity;
using RemoveWriteLines;

namespace TestForAvailability
{
    [TestClass]
    public class TestOfSiteDLL
    {
        [TestMethod]
        public void PingReply()
        {
            unityData Container = new unityData();
            Container.SetContainer();

            string SiteName = "www.ya.ru"; //87.250.250.242
            string Expected = "87.250.250.242";
            Sites site = new Sites(SiteName, Container.container);

            site.CheckAvailability();

            string actual = site.SitePingData;
            Assert.AreEqual(Expected, actual, "The programm does not work correctly");
        }
    }
}
