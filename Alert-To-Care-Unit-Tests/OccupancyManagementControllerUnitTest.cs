using System;
using Alert_to_Care.Controller;
using Alert_to_Care.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Xunit;

namespace Alert_To_Care_Unit_Tests
{
    public class OccupancyManagementControllerUnitTest
    {
        private readonly OccupancyManagementController controller;

        public OccupancyManagementControllerUnitTest()
        {
            IPatientData service = new PatientDataRepository();
            controller = new OccupancyManagementController(service);
        }

        [Fact]
        private void WhenGetisCalledWithIcuIdReturnPatientsList()
        {
            //make sure this ICU id should be in database
            var icuId = 27;
            // Act
            var result = controller.Get(icuId);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void WhenIcuHasZeroPatient()
        {
            //make sure this ICU id should not be in database
            var icuId = 999999;
            // Act
            var result = controller.Get(icuId);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void WhenGetPatientByIdCalledWithAvailablePatientIdReturnPatient()
        {
            //make sure this patient id is in database
            var patientId = 10003;
            var result = controller.GetPatientById(patientId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void WhenGetPatientByIdCalledWithNotAvailableId()
        {
            //this patient id shold not be in db
            var patientId = 99999;
            var result = controller.GetPatientById(patientId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void WhenPostCalledWithAvailbleIcuIdRegisterPatient()
        {
            //make sure this icu id is in db
            var icuId = 27;
            var body = new PatientDetailsInput
            {
                name = "Tom",
                age = 22,
                bloodGroup = "AB+",
                address = "Jaipur"
            };
            var result = controller.Post(icuId, body);
            Assert.IsType<OkObjectResult>(result);
            //var okObjectResult = result as OkObjectResult;
            //var model = okObjectResult.Value as Message;
            /*var actual = model.Messages;
            Assert.Equal("Registered Sucessfully!", actual);*/
        }

        [Fact]
        private void WhenPostCalledWithNotAvailbleIcuIdRegisterPatient()
        {
            //make sure this icu id is in db
            var icuId = 999999;
            var body = new PatientDetailsInput
            {
                name = "Jerry",
                age = 20,
                bloodGroup = "A+",
                address = "UP"
            };
            var result = controller.Post(icuId, body);
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = result as OkObjectResult;
            //Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as Message;
            //Assert.NotNull(model);
            var actual = model.Messages;
            Assert.Equal("Registration UnSucessfull - Bed not Available!", actual);
        }

        [Fact]
        private void WhenDeleteCalledDisChargeThePatientIfPatientIdAvailable()
        {
            //make sure this patient id is in db
            var patientId = 102;
            var result = controller.Delete(patientId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void WhenDeleteCalledDisChargeThePatientIfPatientIdNotAvailable()
        {
            //make sure this patient id is not i db
            var patientId = 99999;
            var result = controller.Delete(patientId);
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as Message;
            //Assert.NotNull(model);
            var actual = model.Messages;
            Assert.Equal("Patient ID : " + patientId + " not registered!", actual);
        }

        [Fact]
        private void WhenUpdateIsCalledWithAvailablePatientId()
        {
            //make sure this patient id is there in db
            var patientId = 10003;
            var body = new PatientModel();
            body.Id = 10003;
            body.Name = "Salini";
            body.Age = 22;
            body.BloodGroup = "AB+";
            body.Address = "UP";
            body.IcuId = 27;
            body.BedNumber = 3;
            //Assert
            var result = controller.Update(patientId, body);
            ;
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void WhenUpdateIsCalledWithNotAvailablePatientId()
        {
            //make sure this patient id is not there in db
            var patientId = 999999;
            var body = new PatientModel();
            body.Id = 999999;
            body.Name = "Shivani";
            body.Age = 23;
            body.BloodGroup = "A+";
            body.Address = "Bihar";
            body.IcuId = 27;
            body.BedNumber = 2;
            //Assert
            var result = controller.Update(patientId, body);
            ;
            //Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = result as OkObjectResult;
            // Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as Message;
            // Assert.NotNull(model);
            var actual = model.Messages;
            Assert.Equal("Id Not Present - Update Unsuccessfull!", actual);
        }


        [Fact]
        private void ModelTest()
        {
            var patient = new PatientDetailsInput();
            patient.name = "Twinkal";
            patient.age = 22;
            patient.bloodGroup = "AB+";
            patient.address = "Jaipur";
            Assert.Equal("Twinkal", patient.name);
            var patientModel = new PatientModel();
            Console.WriteLine(patient.name);
            Console.WriteLine(patient.age);
            Console.WriteLine(patient.bloodGroup);
            Console.WriteLine(patient.address);
            patientModel.Id = 1;
            patientModel.Name = "Shivani";
            patientModel.Age = 22;
            patientModel.BloodGroup = "AB+";
            patientModel.Address = "Jaipur";
            patientModel.IcuId = 27;
            patientModel.BedNumber = 3;
            Assert.Equal(1, patientModel.Id);
            Console.WriteLine(patientModel.Name);
            Console.WriteLine(patientModel.Age);
            Console.WriteLine(patientModel.BloodGroup);
            Console.WriteLine(patientModel.Address);
            Console.WriteLine(patientModel.IcuId);
            Console.WriteLine(patientModel.BedNumber);
        }
    }
}