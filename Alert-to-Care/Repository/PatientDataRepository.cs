using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Models;

namespace Alert_to_Care.Repository
{
    public class PatientDataRepository : CommonFunctionality, IPatientData
    {
        private readonly SQLiteConnection con;

        //string cs = @"URI=file:C:\Users\320104085\OneDrive - Philips\Bootcamp\Alert-To-Care\alert-to-care-s22b8\Alert-to-Care\Patient.db";
        private readonly string cs = @"URI=file:" + Directory.GetCurrentDirectory() + @"\" + "Patient.db";

        public PatientDataRepository()
        {
            con = OpenFile(cs);

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Patient
             (Id      INTEGER NOT NULL PRIMARY KEY,
              Name VARCHAR(20) NOT NULL,
              Age INTEGER NOT NULL,
              BloodGroup VARCHAR(3) NOT NULL,
              Address VARCHAR(40) NOT NULL,
              IcuId INTEGER NOT NULL,
              BedNumber INTEGER NOT NULL,
              FOREIGN KEY(IcuId) REFERENCES ICU(IcuId))";
            var x = cmd.ExecuteNonQuery();
        }


        public bool AddNewPatient(int icuID, PatientDetailsInput patient)
        {
            var occupancy = ReturnBedNumber(icuID);

            if (occupancy == -1)
                return false;

            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = @"INSERT INTO Patient(Name,Age,BloodGroup,Address,BedNumber,IcuId) VALUES('" +
                              patient.name + "','" + patient.age + "','" + patient.bloodGroup + "','" +
                              patient.address + "','" + occupancy + "','" + icuID + "')";
            cmd.ExecuteNonQuery();

            return true;
        }

        public PatientModel GetPatient(int patientID)
        {
            var stm = "SELECT * FROM Patient Where Id=" + patientID;

            using var cmd = new SQLiteCommand(stm, con);
            using var rdr = cmd.ExecuteReader();
            Console.WriteLine("inside get");

            var list = RetrievePatient(rdr);
            if (list.Count != 0)
                return list[0];


            return null;
        }

        public List<PatientModel> GetAllPatientsInTheICU(int id)
        {
            var stm = "SELECT * FROM Patient Where IcuId=" + id;

            using var cmd = new SQLiteCommand(stm, con);
            using var rdr = cmd.ExecuteReader();
            Console.WriteLine("inside get");

            var listOfPatients = RetrievePatient(rdr);

            return listOfPatients;
        }

        public bool DischargePatient(int patientID)
        {
            try
            {
                var com = @"SELECT COUNT(*) AS Count FROM Patient WHERE id=" + patientID;

                var countOfIcu = CheckIfICUExists(com, con);

                var stm = @"DELETE FROM patient WHERE Id=" + patientID;

                using var cmd = new SQLiteCommand(stm, con);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RegisterNewPatinetWithGivenId(int id, PatientModel patient)
        {
            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = @"INSERT INTO Patient(Id,Name,Age,BloodGroup,Address,IcuId,BedNumber) VALUES('" + id +
                              "','" + patient.Name + "','" + patient.Age + "','" + patient.BloodGroup + "','" +
                              patient.Address + "','" + patient.IcuId + "','" + patient.BedNumber + "')";
            cmd.ExecuteNonQuery();
            return true;
        }

        public int ReturnBedNumber(int icuID)
        {
            //Connecting to ICU table to check the capacity
            var capacityOfICU = CapacityOfICU(icuID);

            var stm = @"SELECT  COUNT(*) AS NumOfOccupants FROM patient Where IcuId=" + icuID;
            using var cmi = new SQLiteCommand(stm, con);
            using var rdri = cmi.ExecuteReader();
            if (!rdri.Read())
                return -1;
            var occupancy = (int) Convert.ToInt64(rdri["NumOfOccupants"]);

            if (occupancy >= capacityOfICU)
                return -1;

            return ProcessBedNumber(icuID, capacityOfICU);
        }

        public int CapacityOfICU(int icuID)
        {
            //string cs1 = @"URI=file:C:\Users\320104085\OneDrive - Philips\Bootcamp\Alert-To-Care\alert-to-care-s22b8\Alert-to-Care\ICU.db";
            var cs1 = @"URI=file:" + Directory.GetCurrentDirectory() + @"\" + "ICU.db";
            var con1 = OpenFile(cs1);


            using var cmdICU = new SQLiteCommand(con1);
            var stm = "SELECT * FROM ICU where Id=" + icuID;
            using var cmd1 = new SQLiteCommand(stm, con1);
            using var rdr1 = cmd1.ExecuteReader();

            var capacityOfICU = 0;
            if (!rdr1.Read())
                return -1;

            //Checking capacity is full or not
            capacityOfICU = (int) Convert.ToInt64(rdr1["NumberOfBeds"]);
            return capacityOfICU;
        }

        public int ProcessBedNumber(int icuID, int capacityOfICU)
        {
            var stm = @"SELECT  * FROM patient Where IcuId=" + icuID;
            using var cm = new SQLiteCommand(stm, con);
            using var rdr = cm.ExecuteReader();
            var beds = new bool[capacityOfICU + 1];


            while (rdr.Read())
            {
                var pos = (int) Convert.ToInt64(rdr["BedNumber"]);
                beds[pos] = true;
            }

            return ProcessNumberHelper(capacityOfICU, beds);
        }

        public int ProcessNumberHelper(int capacityOfICU, bool[] beds)
        {
            var occupancy = 0;
            var i = 1;

            while (i <= capacityOfICU)
            {
                if (!beds[i])
                {
                    occupancy = i;
                    break;
                }

                i++;
            }

            return occupancy;
        }

        public List<PatientModel> RetrievePatient(SQLiteDataReader rdr)
        {
            var list = new List<PatientModel>();
            while (rdr.Read())
            {
                var patientObject = new PatientModel();
                patientObject.Id = (int) Convert.ToInt64(rdr["Id"]);
                patientObject.Name = Convert.ToString(rdr["Name"]);
                patientObject.Age = (int) Convert.ToInt64(rdr["Age"]);
                patientObject.BloodGroup = Convert.ToString(rdr["BloodGroup"]);
                patientObject.Address = Convert.ToString(rdr["Address"]);
                patientObject.BedNumber = (int) Convert.ToInt64(rdr["BedNumber"]);
                patientObject.IcuId = (int) Convert.ToInt64(rdr["IcuId"]);
                list.Add(patientObject);
            }

            return list;
        }
    }
}