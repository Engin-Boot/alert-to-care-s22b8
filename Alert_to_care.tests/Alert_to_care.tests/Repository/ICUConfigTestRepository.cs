using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Alert_to_care.tests.Repository
{
    public class ICUConfigTestRepository
    {
        public List<Models.ICUModel> GetAllICU()
        {
            var restClient = new RestClient("http://localhost:54384/api/");

            var restRequest = new RestRequest("ICUConfig/", Method.GET);


            IRestResponse<List<Models.ICUModel>> response = restClient.Execute<List<Models.ICUModel>>(restRequest);
            return response.Data;
        }

        public Models.ICUModel GetICU(int id)
        {
            var restClient = new RestClient("http://localhost:54384/api/");

            string s = "ICUConfig/" + id.ToString();
            //Console.WriteLine(s);
            var restRequest = new RestRequest(s, Method.GET);


            IRestResponse<Models.ICUModel> response = restClient.Execute<Models.ICUModel>(restRequest);
            return response.Data;
        }

        public bool RegisterIcu() {
            var restClient = new RestClient("http://localhost:54384/api/");
            var restRequest = new RestRequest("ICUConfig/register", Method.POST);
            Models.UserInput userInput = new Models.UserInput();
            userInput.numberOfBeds = 3;
            userInput.layout = 'H';
            restRequest.AddJsonBody(JsonConvert.SerializeObject(userInput));

            IRestResponse restResponse = restClient.Execute(restRequest);
            return restResponse.IsSuccessful;
        }
        public HttpStatusCode DeleteIcu(int id) {
            var restClient = new RestClient("http://localhost:54384/api/");

            string s = "ICUConfig/" + id.ToString();
            //Console.WriteLine(s);
            var restRequest = new RestRequest(s, Method.DELETE);
            IRestResponse response = restClient.Execute(restRequest);
            return response.StatusCode;
        }
    }
}
