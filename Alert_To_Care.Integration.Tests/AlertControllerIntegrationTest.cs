using System.Collections.Generic;
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
    public class AlertControllerIntegrationTest
    {
        private static readonly string url = "http://localhost:5000/api/VitalsAlert";

        private readonly TestContext _sut;

        public AlertControllerIntegrationTest()
        {
            _sut = new TestContext();
        }

        [Fact]
        public async Task WhenNeedSendAnAlert()
        {
            var vitalsList = new List<PatientVitals>();
            var vitals = new List<int> {200, 1000, 30};
            var patientVital = new PatientVitals
            {
                Id = 24,
                Vitals = vitals
            };
            vitalsList.Add(patientVital);
            var response = await _sut.Client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(vitalsList), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            //  var responseString = await response.Content.ReadAsStringAsync();
            //Assert.Contains("Registration UnSucessfull - Bed not Available!", responseString);
        }
    }
}