using System.Collections.Generic;
using Models;

namespace Alert_to_Care.Repository
{
    public class VitalsCheckerRepository : IVitalsCheckerRepository
    {
        private readonly IAlerter _alerter = new EmailAlert();
        private  readonly List<int> _lowerLimit = new List<int>();
        private readonly List<int> _upperLimit = new List<int>();

        public VitalsCheckerRepository()
        {
            //upper and lower limit of beats per minute
            _upperLimit.Add(150);
            _lowerLimit.Add(70);
            //upper and lower limit of spo2
            _upperLimit.Add(int.MaxValue);
            _lowerLimit.Add(90);
            //upper and lower linit of resp rate
            _upperLimit.Add(95);
            _lowerLimit.Add(30);
        }

        public void CheckVitals(List<PatientVitals> patientVitals)
        {
            for (var i = 0; i < patientVitals.Count; i++)
            {
                AlertForBpm(_upperLimit[0], _lowerLimit[0], patientVitals[i].Vitals[0], patientVitals, i);

                AlertForSpo2(_upperLimit[1], _lowerLimit[1], patientVitals[i].Vitals[1], patientVitals, i);

                AlertForRr(_upperLimit[2], _lowerLimit[2], patientVitals[i].Vitals[2], patientVitals, i);
            }

            
        }

        private void AlertForBpm(int upperLimit, int lowerLimit, int value, List<PatientVitals> patientVitals, int i)
        {
            if (value < lowerLimit || value > upperLimit)
                _alerter.Alert($"{patientVitals[i].Id} crossed threshold of BPM ");
        }

        private void AlertForSpo2(int upperLimit, int lowerLimit, int value, List<PatientVitals> patientVitals, int i)
        {
            if (value < lowerLimit || value > upperLimit)
                _alerter.Alert($"{patientVitals[i].Id} crossed threshold of Spo2 ");
        }

        private void AlertForRr(int upperLimit, int lowerLimit, int value, List<PatientVitals> patientVitals, int i)
        {
            if (value < lowerLimit || value > upperLimit)
                _alerter.Alert($"{patientVitals[i].Id} crossed threshold of resp rate ");
        }
    }
}