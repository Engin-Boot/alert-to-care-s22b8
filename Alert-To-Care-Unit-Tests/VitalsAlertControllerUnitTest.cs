using System;
using Xunit;
using Alert_to_Care.Controller;
using Models;
using Alert_to_Care.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query.Internal;
namespace Alert_To_Care_Unit_Tests
{
    public class VitalsAlertControllerUnitTest
    {
        readonly VitalsAlertController  controller;

        public VitalsAlertControllerUnitTest()
        {

            IVitalsCheckerRepository service = new VitalsCheckerRepository();
            controller = new VitalsAlertController(service);
        }

        [Fact]

        void ReturnOkWhenPostIsCalled() {

            List<PatientVitals> vitalsListWithId = new List<PatientVitals>();
            List<int> vitalList =new List<int>();
            vitalList.Add(22);
            vitalList.Add(22);
            vitalList.Add(22);
            PatientVitals patientOneVital = new PatientVitals()
            {
                Id = 102,
                Vitals =vitalList
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

