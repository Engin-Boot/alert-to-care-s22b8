using Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DataProducer
{
    public class DataReader
    {
        public List<IRestResponse> ReadDataAndPost()
        {
            string cs2 = Directory.GetCurrentDirectory() + @"\" + "vitalData.csv";
            Console.WriteLine(cs2);


            using (var reader = new StreamReader(@"C:\Users\320104085\OneDrive - Philips\Bootcamp\Alert-To-Care\alert-to-care-s22b8\DataProducer\vitalData.csv"))
                {
                    reader.ReadLine();

                    List<IRestResponse> lr = new List<IRestResponse>();
                    while (!reader.EndOfStream)
                    {
                        List<PatientVitals> li = new List<PatientVitals>();
                        for (int j = 0; j < 3; j++)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            PatientVitals patientVitals = new PatientVitals();
                            patientVitals.Id = int.Parse(values[0]);
                            patientVitals.Vitals = new List<int>
                        {
                            int.Parse(values[1]),
                            int.Parse(values[2]),
                            int.Parse(values[3])
                        };
                            li.Add(patientVitals);
                        }
                        var restClient = new RestClient("http://localhost:5000/api/");
                        var restRequest = new RestRequest("VitalsAlert", Method.POST);

                        restRequest.AddJsonBody(JsonConvert.SerializeObject(li));

                        restClient.Execute(restRequest);
                        IRestResponse restResponse = restClient.Execute(restRequest);
                        lr.Add(restResponse);
                        //Thread.Sleep(10000);

                    }
                    return lr;
                }

            
            
        }
    }
}
