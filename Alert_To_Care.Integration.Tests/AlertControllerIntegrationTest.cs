using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Models;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using AssistAPurchase.Integration.Tests;
using System.Collections.Generic;

namespace Alert_To_Care.Integration.Tests
{
   public class AlertControllerIntegrationTest
    {

        private readonly TestContext _sut;
        static string url = "http://localhost:5000/api/VitalsAlert";
        public AlertControllerIntegrationTest()
        {
            _sut = new TestContext();
        }
        
        [Fact]
        public async Task WhenNeedSendAnAlert()
        {
            List<PatientVitals> vitalsListWithId = new List<PatientVitals>();
            List<int> vitalList = new List<int>();
            vitalList.Add(22);
            vitalList.Add(22);
            vitalList.Add(22);
            PatientVitals patientOneVital = new PatientVitals()
            {
                Id = 24,
                Vitals = vitalList
            };
            vitalsListWithId.Add(patientOneVital);
            var response = await _sut.Client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(vitalsListWithId), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
          //  var responseString = await response.Content.ReadAsStringAsync();
            //Assert.Contains("Registration UnSucessfull - Bed not Available!", responseString);
        }
    }
}
