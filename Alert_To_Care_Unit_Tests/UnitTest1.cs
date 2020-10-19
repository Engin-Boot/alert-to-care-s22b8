using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alert_to_Care.Repository;
using Models;
using System.Collections.Generic;
using System.Data.SQLite;
namespace Alert_To_Care_Unit_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAllIcusWhenIcusArePresent()
        {
            ICUDataRepository iCUDataRepository = new ICUDataRepository();
            var icu = iCUDataRepository.GetAllICU();
            Assert.AreEqual(1, icu[0].id);
            Assert.AreEqual(10, icu[0].NumberOfBeds);
            Assert.AreEqual('H', icu[0].Layout);
            
        }


        [TestMethod]
        public void WhenIdIsPresentThenStatusIsOk()
        {
            ICUDataRepository iCUDataRepository = new ICUDataRepository();
            var icu = iCUDataRepository.ViewICU(1);
            Assert.AreEqual(1, icu.id);
            Assert.AreEqual(10, icu.NumberOfBeds);
            Assert.AreEqual('H', icu.Layout);
        }

        [TestMethod]
        public void WhenIcuIdIsNotPresentThenStatusIsNotFound()
        {
            ICUDataRepository iCUDataRepository = new ICUDataRepository();
            var icu = iCUDataRepository.ViewICU(172);
            Assert.AreEqual(null, icu);
        }

        [TestMethod]
        public void RegisterIcuWhenJsonFromBodyIsPosted()
        {
            ICUDataRepository iCUDataRepository = new ICUDataRepository();
            UserInput userInput = new UserInput();
            userInput.Layout = 'L';
            userInput.NumberOfBeds = 100;
            var response = iCUDataRepository.RegisterNewICU(userInput);
            Assert.AreEqual(true, response);

        }
        [TestMethod]
         public void DeleteIcuWhenIdIsPresentThenStatusOk()
        {
            string cs = @"URI=file:C:\Users\320104085\OneDrive - Philips\Bootcamp\Alert-To-Care\alert-to-care-s22b8\Alert-to-Care\ICU.db";
            SQLiteConnection con = new SQLiteConnection(cs, true);
            con.Open();
            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = @"INSERT INTO ICU(Id, NumberOfBeds, Layout) VALUES('200', '1', 'L')";
            cmd.ExecuteNonQuery();
            ICUDataRepository iCUDataRepository = new ICUDataRepository();
            var response = iCUDataRepository.DeleteICU(200);
            Assert.AreEqual(true, response);
        }

        [TestMethod]
        public void DeleteIcuWhenIdIsNotPresentThenStatusNotFound()
        {
            ICUDataRepository iCUDataRepository = new ICUDataRepository();
            var response = iCUDataRepository.DeleteICU(125);
            Assert.AreEqual(false, response);
        }

        [TestMethod]
        public void WhenICUIsFullStatusNotFound()
        {
            PatientDataRepository patientDataRepository = new PatientDataRepository();
            PatientDetailsInput patientDetailsInput = new PatientDetailsInput();
            patientDetailsInput.address = "kolkata";
            patientDetailsInput.age = 45;
            patientDetailsInput.bloodGroup = "o+";
            patientDetailsInput.name = "venu";
            var response = patientDataRepository.AddNewPatient(1, patientDetailsInput);
            Assert.AreEqual(false, response);
        }

        [TestMethod]
        public void GetAllPatientWhenPatientsArePresent()
        {
            PatientDataRepository patientDataRepository = new PatientDataRepository();
            var response = patientDataRepository.GetAllPatientsInTheICU(1);
            Assert.AreEqual("ana", response[0].Name);
            Assert.AreEqual(6, response[0].Id);
        }

        [TestMethod]
        public void GetAllPatientWhenICUDoesntExist()
        {
            PatientDataRepository patientDataRepository = new PatientDataRepository();
            var response = patientDataRepository.GetAllPatientsInTheICU(139);
            Assert.AreEqual(0, response.Count);
        }

        [TestMethod]
        public void GetPatientDetailsWhenPatientExists()
        {
            PatientDataRepository patientDataRepository = new PatientDataRepository();
            var response = patientDataRepository.GetPatient(6);
            Assert.AreEqual("ana", response.Name);
            Assert.AreEqual("b+", response.BloodGroup);

        }

        [TestMethod]
        public void GetPatientDetailsWhenPatientDoesNotExist()
        {
            PatientDataRepository patientDataRepository = new PatientDataRepository();
            var response = patientDataRepository.GetPatient(126);
            Assert.AreEqual(null, response);

        }


        [TestMethod]
         public void CheckIfPatientIsDeletedWhenExists()
        {
            string cs = @"URI=file:C:\Users\320104085\OneDrive - Philips\Bootcamp\Alert-To-Care\alert-to-care-s22b8\Alert-to-Care\Patient.db";
            SQLiteConnection con = new SQLiteConnection(cs, true);
            con.Open();
            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = @"INSERT INTO Patient(Id, Name, Age, BloodGroup, Address, IcuId, BedNumber) VALUES('200', 'ananya', '90', 'B+', 'Bihar', '2', '2')";
            cmd.ExecuteNonQuery();
            PatientDataRepository patientDataRepository = new PatientDataRepository();
            var response = patientDataRepository.DischargePatient(200);
            Assert.AreEqual(true, response);
        }

        [TestMethod]
        public void CheckIfPatientIsDeletedWhenDoesNotExist()
        {
            PatientDataRepository patientDataRepository = new PatientDataRepository();
            var response = patientDataRepository.DischargePatient(8);
            Assert.AreEqual(false, response);
        }

        [TestMethod]
        public void PostVitalsToCheckWhenInRange()
        {
            VitalsCheckerRepository vitalsCheckerRepository = new VitalsCheckerRepository();
            PatientVitals patientVitals = new PatientVitals();
            patientVitals.Id = 1;
            patientVitals.Vitals = new List<int> { 100, 91, 90 };
            List<PatientVitals> li = new List<PatientVitals>();
            var response = vitalsCheckerRepository.CheckVitals(li);
            Assert.AreEqual(true, response);
        }

       
    }
}
