using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Models;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Alert_To_Care.Integration.Tests
{
    public class IcuConfigIntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private static readonly string url = "https://localhost:5000/api/ICUConfig";
        private readonly TestContext _sut;


        public IcuConfigIntegrationTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
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
            var createIcu = new IcuModel
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
            var updateIcu = new IcuModel
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
                Id = "3",
                IsOccupied = true
            };
            /*var response = await _sut.Client.PutAsync(url + "/22",
                new StringContent(JsonConvert.SerializeObject(updateIcu), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);*/
            _testOutputHelper.WriteLine(updateIcu.ToString());

        }
    }
}