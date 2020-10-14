using Models;
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

        public void AlertForBPM(int upperLimit, int lowerLimit, int value, List<PatientVitals> patientVitals, int i)
        {
            if (value < lowerLimit || value > upperLimit)
            {
                alerter.Alert($"{patientVitals[i].Id} crossed threshold of BPM ");
            }
        }

        public void AlertForSpo2(int upperLimit, int lowerLimit, int value, List<PatientVitals> patientVitals, int i)
        {
            if (value < lowerLimit || value > upperLimit)
            {
                alerter.Alert($"{patientVitals[i].Id} crossed threshold of Spo2 ");
            }
        }

        public void AlertForRR(int upperLimit, int lowerLimit, int value, List<PatientVitals> patientVitals, int i)
        {
            if (value < lowerLimit || value > upperLimit)
            {
                alerter.Alert($"{patientVitals[i].Id} crossed threshold of resp rate ");
            }
        }
        public void CheckVitals(List<PatientVitals> patientVitals)
        {
            for (int i = 0; i < patientVitals.Count; i++) {
                
                AlertForBPM(upperLimit[0], lowerLimit[0], patientVitals[i].Vitals[0], patientVitals, i);
                
                AlertForSpo2(upperLimit[1], lowerLimit[1], patientVitals[i].Vitals[1], patientVitals, i);
                
                AlertForRR(upperLimit[2], lowerLimit[2], patientVitals[i].Vitals[2], patientVitals, i);
                
            }
            
        }

        

    }
}
