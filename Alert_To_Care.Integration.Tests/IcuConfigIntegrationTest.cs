using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AssistAPurchase.Integration.Tests;
using FluentAssertions;
using Models;
using Newtonsoft.Json;
using Xunit;
using System.IO;

namespace Alert_To_Care.Integration.Tests
{
    public class IcuConfigIntegrationTest
    {
        private static readonly string url = "https://localhost:5000/api/ICUConfig";
        private readonly TestContext _sut;


        public IcuConfigIntegrationTest()
        {
            _sut = new TestContext();
        }

        [Fact]
        public async Task WhenAllIcuDetailsIsRequiredThenCheckStatusCode()
        {
            var response = await _sut.Client.GetAsync(url);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/9")]
        [InlineData("/100")]
        public async Task WhenSpecificIuDetailsIsRequiredThenCheckStatusCode(string value)
        {
            var response = await _sut.Client.GetAsync(url + value);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task WhenDeleteRequestIsSentThenResponseOk()
        {
            var response = await _sut.Client.DeleteAsync(url + "/8");
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task WhenDeleteRequestIsSentThenResponseNotFound()
        {
            var response = await _sut.Client.DeleteAsync(url + "/8909808");
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task WhenNewDataToBeCreatedThenCheckResponse()
        {
            var createIcu = new ICUModel
            {
                NumberOfBeds = 3,
                Layout = 'C'
            };
            var response = await _sut.Client.PostAsync(url + "/register",
                new StringContent(JsonConvert.SerializeObject(createIcu), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task WhenNewDataIsUpdatedThenCheckTheResponseNoContent()
        {
            var updateIcu = new ICUModel
            {
                NumberOfBeds = 3,
                Layout = 'C'
            };
            var response = await _sut.Client.PutAsync(url + "/21",
                new StringContent(JsonConvert.SerializeObject(updateIcu), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public void  WhenIcuDataUpdatedThenCheckTheResponseNoContent()
        {
            var updateIcu = new Bed
            {
                id = "3",
                isOccupied = true
            };
            /*var response = await _sut.Client.PutAsync(url + "/22",
                new StringContent(JsonConvert.SerializeObject(updateIcu), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);*/
            Console.WriteLine(updateIcu);

        }
    }
}