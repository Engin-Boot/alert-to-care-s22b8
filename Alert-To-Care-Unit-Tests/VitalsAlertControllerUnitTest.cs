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
        private readonly VitalsAlertController _controller;

        public VitalsAlertControllerUnitTest()
        {
            IVitalsCheckerRepository service = new VitalsCheckerRepository();
            _controller = new VitalsAlertController(service);
        }

        [Fact]
        private void ReturnOkWhenPostIsCalled()
        {
            var vitalsListWithId = new List<PatientVitals>();
            var vitalList = new List<int> {22, 22, 22};
            var patientOneVital = new PatientVitals
            {
                Id = 102,
                Vitals = vitalList
            };
            vitalsListWithId.Add(patientOneVital);
            // Act
            var result = _controller.Post(vitalsListWithId);
            // Assert
            Assert.IsType<OkObjectResult>(result);
            //Assert.NotNull(okObjectResult);
            if (result is OkObjectResult okObjectResult)
            {
                //Assert.NotNull(model);
                if (okObjectResult.Value is Message model)
                {
                    var actual = model.Messages;
                    Assert.Equal("Alert Sent!!", actual);
                }
            }
        }
    }
}