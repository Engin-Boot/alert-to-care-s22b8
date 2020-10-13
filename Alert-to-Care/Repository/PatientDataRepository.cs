using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Models;

namespace Alert_to_Care.Repository
{
    public class PatientDataRepository : IPatientData
    {

        string cs = @"URI=file:C:\Users\320107420\source\repos\Alert-to-Care\Alert-to-Care\Patient.db";
        SQLiteConnection con;

        public PatientDataRepository()
        {
            con = new SQLiteConnection(cs, true);
            con.Open();
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
            cmd.ExecuteNonQuery();
        }

        public List<PatientModel> GetAllPatientsInTheICU(int id)
        {
            string stm = "SELECT * FROM Patient Where IcuId=" + id;

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine("inside get");

            List<PatientModel> listOfPatients = new List<PatientModel>();
            while (rdr.Read())
            {
                PatientModel patientObject = new PatientModel();
                patientObject.Id = (int)Convert.ToInt64(rdr["Id"]);
                patientObject.Name = Convert.ToString(rdr["Name"]);
                patientObject.Age = (int)Convert.ToInt64(rdr["Age"]);
                patientObject.BloodGroup = Convert.ToString(rdr["BloodGroup"]);
                patientObject.Address = Convert.ToString(rdr["Address"]);
                patientObject.BedNumber = (int)Convert.ToInt64(rdr["BedNumber"]);
                patientObject.IcuId = (int)Convert.ToInt64(rdr["IcuId"]);


                listOfPatients.Add(patientObject);
            }

            return listOfPatients;
        }


        public bool AddNewPatient(int icuID, PatientDetailsInput patient)
        {
            //Connecting to ICU table to check the capacity
            string cs1 = @"URI=file:C:\Users\320107420\source\repos\Alert-to-Care\Alert-to-Care\ICU.db";
            SQLiteConnection con1;
            con1 = new SQLiteConnection(cs1, true);
            con1.Open();
            using var cmdICU = new SQLiteCommand(con1);
            string stm = "SELECT * FROM ICU where Id=" + icuID;
            using var cmd1 = new SQLiteCommand(stm, con1);
            using SQLiteDataReader rdr1 = cmd1.ExecuteReader();
            int occupancy = 0;
            int capacityOfICU = 0;
            if (rdr1.Read())
            {
                //Checking capacity is full or not
                capacityOfICU = (int)Convert.ToInt64(rdr1["NumberOfBeds"]);

                stm = @"SELECT  COUNT(*) AS NumOfOccupants FROM patient Where IcuId=" + icuID;
                using var cmi = new SQLiteCommand(stm, con);
                using SQLiteDataReader rdri = cmi.ExecuteReader();
                if(rdri.Read())
                    occupancy = (int)Convert.ToInt64(rdri["NumOfOccupants"]);

                if (occupancy >= capacityOfICU)
                    return false;
            }
            else
            {
                return false;
            }
            stm = @"SELECT  * FROM patient Where IcuId=" + icuID;
            using var cm = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cm.ExecuteReader();
            Boolean[] beds = new Boolean[capacityOfICU+1];
            int i = 1;
            while (rdr.Read())
            {
                int pos = (int)Convert.ToInt64(rdr["BedNumber"]);
                beds[pos] = true;
            }
            while (i <= capacityOfICU)
            {
                if(!beds[i])
                {
                    occupancy = i;
                    break;
                }
                i++;
            }

            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = @"INSERT INTO Patient(Name,Age,BloodGroup,Address,BedNumber,IcuId) VALUES('" + patient.name + "','" + patient.age + "','" + patient.bloodGroup + "','" + patient.address + "','" + occupancy + "','" + icuID + "')";
            cmd.ExecuteNonQuery();

            return true;
        }

        public PatientModel GetPatient(int patientID)
        {
            string stm = "SELECT * FROM Patient Where Id=" + patientID;

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine("inside get");
            PatientModel patientObject=null;

            while (rdr.Read())
            {
                patientObject = new PatientModel();
                Console.WriteLine("Hi");
                patientObject.Id = (int)Convert.ToInt64(rdr["Id"]);
                patientObject.Name = Convert.ToString(rdr["Name"]);
                patientObject.Age = (int)Convert.ToInt64(rdr["Age"]);
                patientObject.BloodGroup = Convert.ToString(rdr["BloodGroup"]);
                patientObject.Address = Convert.ToString(rdr["Address"]);
                patientObject.BedNumber = (int)Convert.ToInt64(rdr["BedNumber"]);
                patientObject.IcuId = (int)Convert.ToInt64(rdr["IcuId"]);

            }

            return patientObject;
        }


        public void DischargePatient(int patientID)
        {
            string stm;
          
            stm = @"SELECT * FROM patient Where Id=" + patientID;
            using var cmi = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdri = cmi.ExecuteReader();
            int occupancy = (int)Convert.ToInt64(rdri["Id"]);
            Console.WriteLine(occupancy);

            if (occupancy!=patientID)
                throw new Exception();

                
            stm = @"DELETE FROM patient WHERE Id=" + patientID;

            using var cmd = new SQLiteCommand(stm, con);
            cmd.ExecuteNonQuery();


        }
    }
}
