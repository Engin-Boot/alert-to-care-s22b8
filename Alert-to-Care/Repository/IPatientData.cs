
using System.Collections.Generic;
using Models;

namespace Alert_to_Care.Repository
{
    public interface IPatientData
    {
        public List<PatientModel> GetAllPatientsInTheICU(int id);

        public bool AddNewPatient(int icuID,PatientDetailsInput patient);


        public PatientModel GetPatient(int id);


        public void DischargePatient(int patientID);

    }
}
