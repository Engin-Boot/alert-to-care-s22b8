﻿using System.Collections.Generic;
using Models;

namespace Alert_to_Care.Repository
{
    public interface IPatientData
    {
        public List<PatientModel> GetAllPatientsInTheIcu(int id);

        public bool AddNewPatient(int icuId, PatientDetailsInput patient);


        public PatientModel GetPatient(int id);


        public bool DischargePatient(int patientId);

        public void RegisterNewPatinetWithGivenId(int id, PatientModel value);
    }
}