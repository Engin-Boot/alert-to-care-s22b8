using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Alert_to_care.tests.Repository
{
    public class OccupancyMgmtRepository
    {
        public HttpStatusCode AddPatient(int icuId) {
            var restClient = new RestClient("http://localhost:5000/api/");
            var restRequest = new RestRequest($"OccupancyManagement/{icuId}", Method.POST);
            Models.OccupancyMgmtModel userInput = new Models.OccupancyMgmtModel();
            userInput.address = "rohini";
            userInput.age = 24;
            userInput.bloodGroup = "ab-";
            userInput.name = "luta";
            restRequest.AddJsonBody(JsonConvert.SerializeObject(userInput));

            IRestResponse restResponse = restClient.Execute(restRequest);
            return restResponse.StatusCode;
        }

        public IRestResponse<List<Models.PatientModel>> GetAllPatient(int icuId)
        {
            var restClient = new RestClient("http://localhost:5000/api/");

            string s = "OccupancyManagement/" + icuId.ToString();
            //Console.WriteLine(s);
            var restRequest = new RestRequest(s, Method.GET);


            IRestResponse<List<Models.PatientModel>> response = restClient.Execute<List<Models.PatientModel>>(restRequest);
            return response;
        }

        public IRestResponse<Models.PatientModel> GetPatientDetails(int patientId)
        {
            var restClient = new RestClient("http://localhost:5000/api/");

            string s = "OccupancyManagement/GetPatientById/" + patientId.ToString();
            //Console.WriteLine(s);
            var restRequest = new RestRequest(s, Method.GET);


            IRestResponse<Models.PatientModel> response = restClient.Execute<Models.PatientModel>(restRequest);
            return response;
        }
        public HttpStatusCode DeletePatient(int patientId)
        {
            var restClient = new RestClient("http://localhost:5000/api/");

            string s = "OccupancyManagement/" + patientId.ToString();
            //Console.WriteLine(s);
            var restRequest = new RestRequest(s, Method.DELETE);
            IRestResponse response = restClient.Execute(restRequest);
            return response.StatusCode;
        }

    }
}
