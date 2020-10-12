using Models;
using System;
using System.Collections.Generic;

namespace Alert_to_Care.Repository
{
    public class VitalsCheckerRepository: IVitalsCheckerRepository
    {
        public List<int> upperLimit = new List<int>();
        public List<int> lowerLimit = new List<int>();
        public Alerter alerter = new EmailAlert();
        public VitalsCheckerRepository()
        {
            //upper and lower limit of beats per minute
            upperLimit.Add(150);
            lowerLimit.Add(70);
            //upper and lower limit of spo2
            upperLimit.Add(int.MaxValue);
            lowerLimit.Add(90);
            //upper and lower linit of resp rate
            upperLimit.Add(95);
            lowerLimit.Add(30);
        }
        public bool CheckVitals(List<PatientVitals> patientVitals)
        {
            int fl = 0;
            for (int i = 0; i < patientVitals.Count; i++) {
                if (patientVitals[i].Vitals[0] < lowerLimit[0] || patientVitals[i].Vitals[0] > upperLimit[0]) {
                    alerter.Alert($"{patientVitals[i].Id} crossed threshold of BPM ");
                    fl--;
                }
                if (patientVitals[i].Vitals[1] < lowerLimit[1] || patientVitals[i].Vitals[1] > upperLimit[1])
                {
                    alerter.Alert($"{patientVitals[i].Id} crossed threshold of sop2 ");
                    fl--;
                }
                if (patientVitals[i].Vitals[2] < lowerLimit[2] || patientVitals[i].Vitals[2] > upperLimit[2])
                {
                    alerter.Alert($"{patientVitals[i].Id} crossed threshold of resp rate");
                    fl--;
                }

                
            }
            if (fl == 0)
                return true;
            return false;
        }

    }
}
