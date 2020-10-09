using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Alert_to_Care.Repository
{
    public interface IPatientData
    {
        public List<PatientModel> GetAllPatientsInTheICU(int id);

        public bool AddNewPatient(int icuID,PatientModel patient);


        public PatientModel GetPatient(int icuID, int patientID);


        public void DischargePatient(int patientID);

    }
}
