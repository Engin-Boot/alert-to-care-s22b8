using System.Collections.Generic;
using Models;

namespace Alert_to_Care.Repository
{
    public interface IVitalsCheckerRepository
    {
        public void CheckVitals(List<PatientVitals> patientVitals);
    }
}