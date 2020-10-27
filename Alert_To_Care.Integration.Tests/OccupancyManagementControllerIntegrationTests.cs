using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Models;
using Newtonsoft.Json;
using Xunit;

namespace Alert_To_Care.Integration.Tests
{
    public class OccupancyManagementControllerIntegrationTests
    {
        private static readonly string url = "https://localhost:5000/api/OccupancyManagement";
        private readonly TestContext _sut;

        public OccupancyManagementControllerIntegrationTests()
        {
            _sut = new TestContext();
        }

        [Theory]
        [InlineData("/27")]
        [InlineData("/GetPatientById/1")]
        public async Task WhenAllPatientDetailsIsRequiredThenCheckStatusCode(string value)
        {
            var response = await _sut.Client.GetAsync(url + value);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task WhenDischargePatientRequestIsSentThenResponseNotFound()
        {
            var response = await _sut.Client.DeleteAsync(url + "/8909808");
            var responseString = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Contains("Patient ID : 8909808 not registered!", responseString);
        }

        [Fact]
        public async Task WhenDischargePatientRequestIsSentThenResponsePresent()
        {
            var response = await _sut.Client.DeleteAsync(url + "/1");
            await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            //   Assert.Contains("Patient ID : 1 deleted!", responseString);
        }

        [Fact]
        public async Task WhenNewDataToBeCreatedThenCheckResponse()
        {
            var createIcu = new PatientModel
            {
                Name = "Amulya",
                Address = "UP",
                Age = 21,
                BedNumber = 9,
                BloodGroup = "B+"
            };
            var response = await _sut.Client.PostAsync(url + "/100",
                new StringContent(JsonConvert.SerializeObject(createIcu), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await response.Content.ReadAsStringAsync();
            //  Assert.Contains("Registered Sucessfully!", responseString);
        }

        [Fact]
        public async Task WhenNewDataToBeCreatedThenCheckResponseNotRegistered()
        {
            var createIcu = new PatientModel
            {
                Name = "Amulya",
                Address = "UP",
                Age = 21,
                BedNumber = 5,
                BloodGroup = "B+"
            };
            var response = await _sut.Client.PostAsync(url + "/27685",
                new StringContent(JsonConvert.SerializeObject(createIcu), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Registration UnSucessfull - Bed not Available!", responseString);
        }

        [Fact]
        public async Task WhenPatientIsNotUpdatedThenCheckTheResponse()
        {
            var updatePatient = new PatientModel
            {
                Name = "Amulya Alur",
                Address = "UP",
                Age = 21,
                BedNumber = 5,
                BloodGroup = "B+"
            };
            var response = await _sut.Client.PutAsync(url + "/1",
                new StringContent(JsonConvert.SerializeObject(updatePatient), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            await response.Content.ReadAsStringAsync();
            //  Assert.Contains("Updated Successfully!", responseString);
            //  Assert.Contains("Id Not Present - Update Unsuccessfull!", responseString);
        }

        [Fact]
        public async Task WhenPatientIsUpdatedSuccessfullyThenCheckTheResponse()
        {
            var updatePatient = new PatientModel
            {
                Name = "Amulya Alur",
                Address = "UP",
                Age = 21,
                BedNumber = 5,
                BloodGroup = "B+"
            };
            var response = await _sut.Client.PutAsync(url + "/2",
                new StringContent(JsonConvert.SerializeObject(updatePatient), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            await response.Content.ReadAsStringAsync();
            //  Assert.Contains("Updated Successfully!", responseString);
            //     Assert.Contains("Updated Successfully!", responseString);
        }

        [Fact]
        public async Task WhenPatientsAreUpdatedSuccessfullyThenCheckTheResponse()
        {
            var updatePatient = new PatientDetailsInput
            {
                Name = "Amulya Alur",
                Address = "UP",
                Age = 21,
                BloodGroup = "B+"
            };
            var response = await _sut.Client.PutAsync(url + "/22",
                new StringContent(JsonConvert.SerializeObject(updatePatient), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            await response.Content.ReadAsStringAsync();
            //  Assert.Contains("Updated Successfully!", responseString);
            //     Assert.Contains("Updated Successfully!", responseString);
        }
    }
}