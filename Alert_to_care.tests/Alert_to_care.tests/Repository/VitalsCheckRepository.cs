using System;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Text;

namespace Alert_to_care.tests.Repository
{
    public class VitalsCheckRepository
    {
        
        public bool CheckVitals(bool inRange)
        {
            var restClient = new RestClient("http://localhost:54384/api/");
            var restRequest = new RestRequest($"VitalsAlert/", Method.POST);

            List<Models.PatientVitals> list = new List<Models.PatientVitals>();
            Models.PatientVitals patient = new Models.PatientVitals();
           
            patient.Id = 1;
            patient.Vitals = new List<int>();

            if (inRange)
                patient.Vitals.Add(80);
            else
                patient.Vitals.Add(65);

            patient.Vitals.Add(100);
            patient.Vitals.Add(40);
            list.Add(patient);

            restRequest.AddJsonBody(JsonConvert.SerializeObject(list));

            IRestResponse restResponse = restClient.Execute(restRequest);
            return restResponse.IsSuccessful;
        }
    }
}
