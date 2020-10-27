using Alert_to_Care.Controller;
using Alert_to_Care.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Xunit;
using Xunit.Abstractions;

namespace Alert_To_Care_Unit_Tests
{
    public class OccupancyManagementControllerUnitTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly OccupancyManagementController _controller;

        public OccupancyManagementControllerUnitTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            IPatientData service = new PatientDataRepository();
            _controller = new OccupancyManagementController(service);
        }

        [Fact]
        private void WhenGetisCalledWithIcuIdReturnPatientsList()
        {
            //make sure this ICU id should be in database
            var icuId = 27;
            // Act
            var result = _controller.Get(icuId);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void WhenIcuHasZeroPatient()
        {
            //make sure this ICU id should not be in database
            var icuId = 999999;
            // Act
            var result = _controller.Get(icuId);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void WhenGetPatientByIdCalledWithAvailablePatientIdReturnPatient()
        {
            //make sure this patient id is in database
            var patientId = 10003;
            var result = _controller.GetPatientById(patientId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void WhenGetPatientByIdCalledWithNotAvailableId()
        {
            //this patient id shold not be in db
            var patientId = 99999;
            var result = _controller.GetPatientById(patientId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void WhenPostCalledWithAvailbleIcuIdRegisterPatient()
        {
            //make sure this icu id is in db
            var icuId = 27;
            var body = new PatientDetailsInput
            {
                Name = "Tom",
                Age = 22,
                BloodGroup = "AB+",
                Address = "Jaipur"
            };
            var result = _controller.Post(icuId, body);
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
                Name = "Jerry",
                Age = 20,
                BloodGroup = "A+",
                Address = "UP"
            };
            var result = _controller.Post(icuId, body);
            Assert.IsType<OkObjectResult>(result);
            //Assert.NotNull(okObjectResult);
            if (result is OkObjectResult okObjectResult)
            {
                //Assert.NotNull(model);
                if (okObjectResult.Value is Message model)
                {
                    var actual = model.Messages;
                    Assert.Equal("Registration UnSucessfull - Bed not Available!", actual);
                }
            }
        }

        [Fact]
        private void WhenDeleteCalledDisChargeThePatientIfPatientIdAvailable()
        {
            //make sure this patient id is in db
            var patientId = 102;
            var result = _controller.Delete(patientId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void WhenDeleteCalledDisChargeThePatientIfPatientIdNotAvailable()
        {
            //make sure this patient id is not i db
            var patientId = 99999;
            var result = _controller.Delete(patientId);
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            //Assert.NotNull(model);
            if (okObjectResult.Value is Message model)
            {
                var actual = model.Messages;
                Assert.Equal("Patient ID : " + patientId + " not registered!", actual);
            }
        }

        [Fact]
        private void WhenUpdateIsCalledWithAvailablePatientId()
        {
            //make sure this patient id is there in db
            var patientId = 10003;
            var body = new PatientModel
            {
                Id = 10003,
                Name = "Salini",
                Age = 22,
                BloodGroup = "AB+",
                Address = "UP",
                IcuId = 27,
                BedNumber = 3
            };
            //Assert
            var result = _controller.Update(patientId, body);
            
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void WhenUpdateIsCalledWithNotAvailablePatientId()
        {
            //make sure this patient id is not there in db
            var patientId = 999999;
            var body = new PatientModel
            {
                Id = 999999,
                Name = "Shivani",
                Age = 23,
                BloodGroup = "A+",
                Address = "Bihar",
                IcuId = 27,
                BedNumber = 2
            };
            //Assert
            var result = _controller.Update(patientId, body);
            
            //Assert
            Assert.IsType<OkObjectResult>(result);
            // Assert.NotNull(okObjectResult);
            if (result is OkObjectResult okObjectResult)
            {
                // Assert.NotNull(model);
                if (okObjectResult.Value is Message model)
                {
                    var actual = model.Messages;
                    Assert.Equal("Id Not Present - Update Unsuccessfull!", actual);
                }
            }
        }


        [Fact]
        private void ModelTest()
        {
            var patient = new PatientDetailsInput {Name = "Twinkal", Age = 22, BloodGroup = "AB+", Address = "Jaipur"};
            Assert.Equal("Twinkal", patient.Name);
            var patientModel = new PatientModel();
            _testOutputHelper.WriteLine(patient.Name);
            _testOutputHelper.WriteLine(patient.Age.ToString());
            _testOutputHelper.WriteLine(patient.BloodGroup);
            _testOutputHelper.WriteLine(patient.Address);
            patientModel.Id = 1;
            patientModel.Name = "Shivani";
            patientModel.Age = 22;
            patientModel.BloodGroup = "AB+";
            patientModel.Address = "Jaipur";
            patientModel.IcuId = 27;
            patientModel.BedNumber = 3;
            Assert.Equal(1, patientModel.Id);
            _testOutputHelper.WriteLine(patientModel.Name);
            _testOutputHelper.WriteLine(patientModel.Age.ToString());
            _testOutputHelper.WriteLine(patientModel.BloodGroup);
            _testOutputHelper.WriteLine(patientModel.Address);
            _testOutputHelper.WriteLine(patientModel.IcuId.ToString());
            _testOutputHelper.WriteLine(patientModel.BedNumber.ToString());
        }
    }
}