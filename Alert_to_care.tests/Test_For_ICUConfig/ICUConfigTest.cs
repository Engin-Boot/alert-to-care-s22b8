using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp.Extensions;
using System.Net;

namespace Test_For_ICUConfig
{
    [TestClass]
    public class ICUConfigTest
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
        public void WhenICUIsFullStatusNotFound()
        {
            Alert_to_care.tests.Repository.OccupancyMgmtRepository occupancyMgmt = new Alert_to_care.tests.Repository.OccupancyMgmtRepository();
            var response = occupancyMgmt.AddPatient(1);
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response);
        }

        [TestMethod]
        public void GetAllPatientWhenPatientsArePresent()
        {
            Alert_to_care.tests.Repository.OccupancyMgmtRepository occupancyMgmt = new Alert_to_care.tests.Repository.OccupancyMgmtRepository();
            var response = occupancyMgmt.GetAllPatient(1);
            Assert.Equals(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("ana", response.Data[0].name);
            Assert.AreEqual(7, response.Data[1].id);
        }

        [TestMethod]
        public void GetAllPatientWhenICUDoesntExist()
        {
            Alert_to_care.tests.Repository.OccupancyMgmtRepository occupancyMgmt = new Alert_to_care.tests.Repository.OccupancyMgmtRepository();
            var response = occupancyMgmt.GetAllPatient(9);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetPatientDetailsWhenPatientExists()
        {
            Alert_to_care.tests.Repository.OccupancyMgmtRepository occupancyMgmt = new Alert_to_care.tests.Repository.OccupancyMgmtRepository();
            var response = occupancyMgmt.GetPatientDetails(6);
            Assert.Equals(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("ana", response.Data.name);
            Assert.AreEqual("b+", response.Data.bloodGroup);
            
        }

        [TestMethod]
        public void GetPatientDetailsWhenPatientDoesNotExist()
        {
            Alert_to_care.tests.Repository.OccupancyMgmtRepository occupancyMgmt = new Alert_to_care.tests.Repository.OccupancyMgmtRepository();
            var response = occupancyMgmt.GetPatientDetails(26);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }


        [TestMethod]
        public void CheckIfPatientIsDeletedWhenExists()
        {
            Alert_to_care.tests.Repository.OccupancyMgmtRepository occupancyMgmt = new Alert_to_care.tests.Repository.OccupancyMgmtRepository();
            var response = occupancyMgmt.DeletePatient(15);
            Assert.AreEqual("Ok", response);
        }

        [TestMethod]
        public void CheckIfPatientIsDeletedWhenDoesNotExists()
        {
            Alert_to_care.tests.Repository.OccupancyMgmtRepository occupancyMgmt = new Alert_to_care.tests.Repository.OccupancyMgmtRepository();
            var response = occupancyMgmt.DeletePatient(115);
            Assert.AreEqual("NotFound", response);
        }

        [TestMethod]
        public void PostVitalsToCheckWhenInRange()
        {
            Alert_to_care.tests.Repository.VitalsCheckRepository vitalsCheck = new Alert_to_care.tests.Repository.VitalsCheckRepository();
            var response = vitalsCheck.CheckVitals(true);
            Assert.AreEqual(true, response);
        }

        [TestMethod]
        public void PostVitalsToCheckWhenNotInRange()
        {
            Alert_to_care.tests.Repository.VitalsCheckRepository vitalsCheck = new Alert_to_care.tests.Repository.VitalsCheckRepository();
            var response = vitalsCheck.CheckVitals(false);
            Assert.AreEqual(true, response);
        }

    }
}
