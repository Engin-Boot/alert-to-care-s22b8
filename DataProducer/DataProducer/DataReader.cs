using Models;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;

namespace DataProducer
{
    public class DataReader
    {
        public PatientVitals ReadData() 
        {
            using (var reader = new StreamReader(@"C:\Users\320105541\OneDrive - Philips\Desktop\boot\DataProducer\vitalData.csv"))
            {

                int i = 0;
                while (!reader.EndOfStream)
                {
                    if (i == 0) {
                        continue;
                    }
                    i++;
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    if (i == 3) { 
                        
                    }
                }
            }
        }
    }
}
