using System;
using Alert_to_Care.Controller;
using Alert_to_Care.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Xunit;

namespace Alert_To_Care_Unit_Tests
{
    public class ICUConfigControllerUnitTest
    {
        private readonly ICUConfigController controller;

        public ICUConfigControllerUnitTest()
        {
            IICUData service = new ICUDataRepository();
            controller = new ICUConfigController(service);
        }

        [Fact]
        public void WhenGetCalledReturnOkResult()
        {
            // Act
            var result = controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void WhenGetWithParameterCalledReturnICUDetails()
        {
            //add test n delete
            var body = new UserInput
            {
                Layout = 'L',
                NumberOfBeds = 3
            };
            controller.Post(body);
            //Act-make sure data base has this id
            var result = controller.Get(288);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void WhenGetWithParameterCalledWithNotAvailableId()
        {
            controller.Delete(56);
            var result = controller.Get(56);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void WhenRegisterIsCalledRegisterICU()
        {
            var body = new UserInput
            {
                Layout = 'L',
                NumberOfBeds = 3
            };
            var result = controller.Post(body);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void WhenRegisterCompareContent()
        {
            //Arrange
            var expected = "Registered Sucessfully!";
            //Act
            var body = new UserInput
            {
                Layout = 'L',
                NumberOfBeds = 3
            };
            var actionResult = controller.Post(body);
            Assert.IsType<OkObjectResult>(actionResult);
            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            var model = okObjectResult.Value as Message;
            var actual = model.Messages;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhenDeleteCalledDelteICUReturnDelted()
        {
            //make sure bedore running test cases add here some id-check with database is it there or not
            var id = 207;
            var actionResult = controller.Delete(id);
            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public void WhenDeleteIsCalledReturnNotRegister()
        {
            //make sure bedore running test cases add here some id-check with database is it there or not
            var id = 207;
            var actionResult = controller.Delete(id);
            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            var okObjectResult = actionResult as OkObjectResult;
            var model = okObjectResult.Value as Message;
            var actual = model.Messages;
            Assert.Equal("ICU ID : " + id + " not registered!", actual);
        }


        [Fact]
        public void WhenUpdateIsCalledWithAvailableIdReturnUpdated()
        {
            //make sure this id is available in db
            //write the icu id which is available
            var id = 100;
            var body = new UserInput
            {
                Layout = 'L',
                NumberOfBeds = 3
            };
            var result = controller.Update(id, body);
            ;
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void WhenUpdateIsCalledWithNoIucIdReturnNotRegistered()
        {
            var id = 20;
            controller.Delete(id);
            var body1 = new UserInput
            {
                Layout = 'I',
                NumberOfBeds = 2
            };
            var result1 = controller.Update(id, body1);
            ;
            //Assert
            Assert.IsType<OkObjectResult>(result1);
            var okObjectResult = result1 as OkObjectResult;
            var model = okObjectResult.Value as Message;
            var actual = model.Messages;
            Assert.Equal("ICU ID : " + id + " not registered!", actual);
        }

        [Fact]
        private void ModelBedTest()
        {
            var bed = new Bed();
            bed.id = "1";
            bed.isOccupied = true;
            Assert.Equal("1", bed.id);
            Assert.True(bed.isOccupied);
        }

        [Fact]
        private void ModelIcuTest()
        {
            var icuModel = new ICUModel();
            icuModel.id = 1;
            icuModel.NumberOfBeds = 2;
            icuModel.Layout = 'L';
            Console.WriteLine(icuModel.id);
            Console.WriteLine(icuModel.NumberOfBeds);
            Console.WriteLine(icuModel.Layout);
        }
    }
}