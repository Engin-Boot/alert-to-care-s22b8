using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Models;

namespace Alert_to_Care.Repository
{
    public class PatientDataRepository : CommonFunctionality, IPatientData
    {
        private readonly SQLiteConnection _con;

        //string cs = @"URI=file:C:\Users\320104085\OneDrive - Philips\Bootcamp\Alert-To-Care\alert-to-care-s22b8\Alert-to-Care\Patient.db";
        private readonly string _cs = @"URI=file:" + Directory.GetCurrentDirectory() + @"\" + "Patient.db";

        public PatientDataRepository()
        {
            _con = OpenFile(_cs);

            using var cmd = new SQLiteCommand(_con)
            {
                CommandText = @"CREATE TABLE IF NOT EXISTS Patient
             (Id      INTEGER NOT NULL PRIMARY KEY,
              Name VARCHAR(20) NOT NULL,
              Age INTEGER NOT NULL,
              BloodGroup VARCHAR(3) NOT NULL,
              Address VARCHAR(40) NOT NULL,
              IcuId INTEGER NOT NULL,
              BedNumber INTEGER NOT NULL,
              FOREIGN KEY(IcuId) REFERENCES ICU(IcuId))"
            };

            cmd.ExecuteNonQuery();
        }


        public bool AddNewPatient(int icuId, PatientDetailsInput patient)
        {
            var occupancy = ReturnBedNumber(icuId);

            if (occupancy == -1)
                return false;

            using var cmd = new SQLiteCommand(_con)
            {
                CommandText = @"INSERT INTO Patient(Name,Age,BloodGroup,Address,BedNumber,IcuId) VALUES('" +
                              patient.Name + "','" + patient.Age + "','" + patient.BloodGroup + "','" +
                              patient.Address + "','" + occupancy + "','" + icuId + "')"
            };
            cmd.ExecuteNonQuery();

            return true;
        }

        public PatientModel GetPatient(int patientId)
        {
            var stm = "SELECT * FROM Patient Where Id=" + patientId;

            using var cmd = new SQLiteCommand(stm, _con);
            using var rdr = cmd.ExecuteReader();
            Console.WriteLine("inside get");

            var list = RetrievePatient(rdr);
            if (list.Count != 0)
                return list[0];


            return null;
        }

        public List<PatientModel> GetAllPatientsInTheIcu(int id)
        {
            var stm = "SELECT * FROM Patient Where IcuId=" + id;

            using var cmd = new SQLiteCommand(stm, _con);
            using var rdr = cmd.ExecuteReader();
            Console.WriteLine("inside get");

            var listOfPatients = RetrievePatient(rdr);

            return listOfPatients;
        }

        public bool DischargePatient(int patientId)
        {
            try
            {
                var com = @"SELECT COUNT(*) AS Count FROM Patient WHERE id=" + patientId;

                CheckIfIcuExists(com, _con);

                var stm = @"DELETE FROM patient WHERE Id=" + patientId;

                using var cmd = new SQLiteCommand(stm, _con);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RegisterNewPatinetWithGivenId(int id, PatientModel patient)
        {
            using var cmd = new SQLiteCommand(_con)
            {
                CommandText = @"INSERT INTO Patient(Id,Name,Age,BloodGroup,Address,IcuId,BedNumber) VALUES('" + id +
                              "','" + patient.Name + "','" + patient.Age + "','" + patient.BloodGroup + "','" +
                              patient.Address + "','" + patient.IcuId + "','" + patient.BedNumber + "')"
            };
            cmd.ExecuteNonQuery();
            
        }

        private int ReturnBedNumber(int icuId)
        {
            //Connecting to ICU table to check the capacity
            var capacityOfIcu = CapacityOfIcu(icuId);

            var stm = @"SELECT  COUNT(*) AS NumOfOccupants FROM patient Where IcuId=" + icuId;
            using var cmi = new SQLiteCommand(stm, _con);
            using var rdri = cmi.ExecuteReader();
            if (!rdri.Read())
                return -1;
            var occupancy = (int) Convert.ToInt64(rdri["NumOfOccupants"]);

            if (occupancy >= capacityOfIcu)
                return -1;

            return ProcessBedNumber(icuId, capacityOfIcu);
        }

        private int CapacityOfIcu(int icuId)
        {
            //string cs1 = @"URI=file:C:\Users\320104085\OneDrive - Philips\Bootcamp\Alert-To-Care\alert-to-care-s22b8\Alert-to-Care\ICU.db";
            var cs1 = @"URI=file:" + Directory.GetCurrentDirectory() + @"\" + "ICU.db";
            var con1 = OpenFile(cs1);


            using var cmdIcu = new SQLiteCommand(con1);
            var stm = "SELECT * FROM ICU where Id=" + icuId;
            using var cmd1 = new SQLiteCommand(stm, con1);
            using var rdr1 = cmd1.ExecuteReader();

            
            if (!rdr1.Read())
                return -1;

            //Checking capacity is full or not
            int capacityOfIcu = (int) Convert.ToInt64(rdr1["NumberOfBeds"]);
            return capacityOfIcu;
        }

        private int ProcessBedNumber(int icuId, int capacityOfIcu)
        {
            var stm = @"SELECT  * FROM patient Where IcuId=" + icuId;
            using var cm = new SQLiteCommand(stm, _con);
            using var rdr = cm.ExecuteReader();
            var beds = new bool[capacityOfIcu + 1];


            while (rdr.Read())
            {
                var pos = (int) Convert.ToInt64(rdr["BedNumber"]);
                beds[pos] = true;
            }

            return ProcessNumberHelper(capacityOfIcu, beds);
        }

        private int ProcessNumberHelper(int capacityOfIcu, bool[] beds)
        {
            var occupancy = 0;
            var i = 1;

            while (i <= capacityOfIcu)
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

        private List<PatientModel> RetrievePatient(SQLiteDataReader rdr)
        {
            var list = new List<PatientModel>();
            while (rdr.Read())
            {
                var patientObject = new PatientModel
                {
                    Id = (int) Convert.ToInt64(rdr["Id"]),
                    Name = Convert.ToString(rdr["Name"]),
                    Age = (int) Convert.ToInt64(rdr["Age"]),
                    BloodGroup = Convert.ToString(rdr["BloodGroup"]),
                    Address = Convert.ToString(rdr["Address"]),
                    BedNumber = (int) Convert.ToInt64(rdr["BedNumber"]),
                    IcuId = (int) Convert.ToInt64(rdr["IcuId"])
                };
                list.Add(patientObject);
            }

            return list;
        }
    }
}