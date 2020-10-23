using System.Collections.Generic;
using Alert_to_Care.Controller;
using Alert_to_Care.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Xunit;

namespace Alert_To_Care_Unit_Tests
{
    public class VitalsAlertControllerUnitTest
    {
        private readonly VitalsAlertController controller;

        public VitalsAlertControllerUnitTest()
        {
            IVitalsCheckerRepository service = new VitalsCheckerRepository();
            controller = new VitalsAlertController(service);
        }

        [Fact]
        private void ReturnOkWhenPostIsCalled()
        {
            var vitalsListWithId = new List<PatientVitals>();
            var vitalList = new List<int>();
            vitalList.Add(22);
            vitalList.Add(22);
            vitalList.Add(22);
            var patientOneVital = new PatientVitals
            {
                Id = 102,
                Vitals = vitalList
            };
            vitalsListWithId.Add(patientOneVital);
            // Act
            var result = controller.Post(vitalsListWithId);
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = result as OkObjectResult;
            //Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as Message;
            //Assert.NotNull(model);
            var actual = model.Messages;
            Assert.Equal("Alert Sent!!", actual);
        }
    }
}