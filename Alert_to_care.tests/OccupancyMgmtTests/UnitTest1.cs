using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OccupancyMgmtTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Alert_to_care.tests.Repository.OccupancyMgmtRepository occupancyMgmt = new Alert_to_care.tests.Repository.OccupancyMgmtRepository();
            var response = occupancyMgmt.AddPatient(1);
            Assert.AreEqual(true, response);
        }
    }
}
