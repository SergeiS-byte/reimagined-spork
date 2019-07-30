using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSQLDLL;
using RemoveWriteLines;
using Unity;

namespace TestForAvailability
{
    [TestClass]
    class TestOfMSQLDLL
    {
        [TestMethod]
        public void SQLServerReply()
        {
            unityData Container = new unityData();
            Container.SetContainer();

            string ServerName = "DESKTOP-CODCI6J";
            string Expected = "14.00.1000";
            MsSQL ms = new MsSQL(ServerName, Container.container);

            ms.CheckAvailability();

            string actual = ms.MSQlRequest;
            Assert.AreEqual(Expected, actual, "The programm does not work correctly");
        }
    }
}
