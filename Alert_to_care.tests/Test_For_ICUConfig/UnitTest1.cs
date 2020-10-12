using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp.Extensions;

namespace Test_For_ICUConfig
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod0()
        {
            Alert_to_care.tests.Repository.ICUConfigTestRepository iCUConfigTestRepository = new Alert_to_care.tests.Repository.ICUConfigTestRepository();
            var icu = iCUConfigTestRepository.GetAllICU();
            Assert.AreEqual(1, icu[0].id);
            Assert.AreEqual(10, icu[0].numberOfBeds);
            Assert.AreEqual('H', icu[0].layout);
        }


        [TestMethod]
        public void TestMethod1()
        {
            Alert_to_care.tests.Repository.ICUConfigTestRepository iCUConfigTestRepository = new Alert_to_care.tests.Repository.ICUConfigTestRepository();
            var icu = iCUConfigTestRepository.GetICU(1);
            Assert.AreEqual(1, icu.id);
            Assert.AreEqual(10, icu.numberOfBeds);
            Assert.AreEqual('H', icu.layout);
            
        }

        [TestMethod]
        public void TestMethod2()
        {
            Alert_to_care.tests.Repository.ICUConfigTestRepository iCUConfigTestRepository = new Alert_to_care.tests.Repository.ICUConfigTestRepository();
            var response = iCUConfigTestRepository.RegisterIcu();
            Assert.AreEqual(true, response);
            
        }
        [TestMethod]
        public void TestMethod3()
        {
            Alert_to_care.tests.Repository.ICUConfigTestRepository iCUConfigTestRepository = new Alert_to_care.tests.Repository.ICUConfigTestRepository();
            var response = iCUConfigTestRepository.DeleteIcu(11);
            Assert.AreEqual(true, response);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Alert_to_care.tests.Repository.OccupancyMgmtRepository occupancyMgmt = new Alert_to_care.tests.Repository.OccupancyMgmtRepository();
            var response = occupancyMgmt.AddPatient(1);
            Assert.AreEqual(true, response);
        }

        [TestMethod]
        public void TestMethod5()
        {
            Alert_to_care.tests.Repository.OccupancyMgmtRepository occupancyMgmt = new Alert_to_care.tests.Repository.OccupancyMgmtRepository();
            var response = occupancyMgmt.GetAllPatient(1);
            Assert.AreEqual("ana", response[0].name);
            Assert.AreEqual(7, response[1].id);
        }

        [TestMethod]
        public void TestMethod6()
        {
            Alert_to_care.tests.Repository.OccupancyMgmtRepository occupancyMgmt = new Alert_to_care.tests.Repository.OccupancyMgmtRepository();
            var response = occupancyMgmt.GetPatientDetails(6);
            Assert.AreEqual("ana", response.name);
            Assert.AreEqual("b+", response.bloodGroup);
        }

        [TestMethod]
        public void TestMethod7()
        {
            Alert_to_care.tests.Repository.OccupancyMgmtRepository occupancyMgmt = new Alert_to_care.tests.Repository.OccupancyMgmtRepository();
            var response = occupancyMgmt.DeletePatient(15);
            Assert.AreEqual(true, response);
        }
    }
}
