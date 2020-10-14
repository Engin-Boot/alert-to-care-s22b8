using Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DataProducer
{
    public class DataReader
    {
        public void ReadDataAndPost() 
        {
            using (var reader = new StreamReader(@"C:\Users\320105541\OneDrive - Philips\Desktop\boot\alert-to-care-s22b8\DataProducer\vitalData.csv"))
            {
                reader.ReadLine();

                
                while (!reader.EndOfStream)
                {
                    List<PatientVitals> li = new List<PatientVitals>();
                    for (int j = 0; j < 3; j++) {
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
                    var restClient = new RestClient("http://localhost:54384/api/");
                    var restRequest = new RestRequest("VitalsAlert", Method.POST);
                    
                    restRequest.AddJsonBody(JsonConvert.SerializeObject(li));

                    restClient.Execute(restRequest);
                    Thread.Sleep(10000);

                }
            }
        }
    }
}
