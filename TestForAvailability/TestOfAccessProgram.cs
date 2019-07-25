using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSqlPr;
using SitePr;
using System;
using UserStructure;
using TestTask;

namespace TestForAvailability
{
    [TestClass]
    public class TestOfAccessProgram
    {
        [TestMethod]
        public void There_Is_No_Site_Name()
        {
            //string SiteName = "www.ya.ru"; 87.250.250.242
            string Expected = "87.250.250.242";
            Sites site = new Sites();

            site.CheckAvailability();

            string actual = site.SitePingData;
            Assert.AreEqual(Expected, actual, "The programm does not work correctly");
        }
    }
}
