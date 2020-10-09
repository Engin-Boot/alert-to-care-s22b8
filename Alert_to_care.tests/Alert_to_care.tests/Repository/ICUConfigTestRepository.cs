using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alert_to_care.tests.Repository
{
    public class ICUConfigTestRepository
    {
        public Models.ICUModel GetICU(int id)
        {
            var restClient = new RestClient("http://localhost:54384/api/");

            string s = "ICUConfig/" + id.ToString();
            //Console.WriteLine(s);
            var restRequest = new RestRequest(s, Method.GET);


            IRestResponse<Models.ICUModel> response = restClient.Execute<Models.ICUModel>(restRequest);
            return response.Data;
        }
    }
}
