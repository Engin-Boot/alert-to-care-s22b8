using Alert_to_Care.Controller;
using Alert_to_Care.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Xunit;
using Xunit.Abstractions;

namespace Alert_To_Care_Unit_Tests
{
    public class IcuConfigControllerUnitTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IcuConfigController _controller;

        public IcuConfigControllerUnitTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            IIcuData service = new IcuDataRepository();
            _controller = new IcuConfigController(service);
        }

        [Fact]
        public void WhenGetCalledReturnOkResult()
        {
            // Act
            var result = _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void WhenGetWithParameterCalledReturnIcuDetails()
        {
            //add test n delete
            var body = new UserInput
            {
                Layout = 'L',
                NumberOfBeds = 3
            };
            _controller.Post(body);
            //Act-make sure data base has this id
            var result = _controller.Get(288);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void WhenGetWithParameterCalledWithNotAvailableId()
        {
            _controller.Delete(56);
            var result = _controller.Get(56);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void WhenRegisterIsCalledRegisterIcu()
        {
            var body = new UserInput
            {
                Layout = 'L',
                NumberOfBeds = 3
            };
            var result = _controller.Post(body);
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
            var actionResult = _controller.Post(body);
            Assert.IsType<OkObjectResult>(actionResult);
            //Assert
            if (actionResult is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is Message model)
                {
                    var actual = model.Messages;
                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void WhenDeleteCalledDelteIcuReturnDelted()
        {
            //make sure bedore running test cases add here some id-check with database is it there or not
            var id = 207;
            var actionResult = _controller.Delete(id);
            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public void WhenDeleteIsCalledReturnNotRegister()
        {
            //make sure bedore running test cases add here some id-check with database is it there or not
            var id = 207;
            var actionResult = _controller.Delete(id);
            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            if (actionResult is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is Message model)
                {
                    var actual = model.Messages;
                    Assert.Equal("ICU ID : " + id + " not registered!", actual);
                }
            }
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
            var result = _controller.Update(id, body);
            
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void WhenUpdateIsCalledWithNoIucIdReturnNotRegistered()
        {
            var id = 20;
            _controller.Delete(id);
            var body1 = new UserInput
            {
                Layout = 'I',
                NumberOfBeds = 2
            };
            var result1 = _controller.Update(id, body1);
            
            //Assert
            Assert.IsType<OkObjectResult>(result1);
            if (result1 is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is Message model)
                {
                    var actual = model.Messages;
                    Assert.Equal("ICU ID : " + id + " not registered!", actual);
                }
            }
        }

        [Fact]
        private void ModelBedTest()
        {
            var bed = new Bed {Id = "1", IsOccupied = true};
            Assert.Equal("1", bed.Id);
            Assert.True(bed.IsOccupied);
        }

        [Fact]
        private void ModelIcuTest()
        {
            var icuModel = new IcuModel {Id = 1, NumberOfBeds = 2, Layout = 'L'};
            _testOutputHelper.WriteLine(icuModel.Id.ToString());
            _testOutputHelper.WriteLine(icuModel.NumberOfBeds.ToString());
            _testOutputHelper.WriteLine(icuModel.Layout.ToString());
        }
    }
}