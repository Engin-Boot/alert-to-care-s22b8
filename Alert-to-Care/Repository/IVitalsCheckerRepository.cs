using System.Collections.Generic;
using Models;

namespace Alert_to_Care.Repository
{
    public interface IVitalsCheckerRepository
    {
        public bool CheckVitals(List<PatientVitals> patientVitals);
    }
}
