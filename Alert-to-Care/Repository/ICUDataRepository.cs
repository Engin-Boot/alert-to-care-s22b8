using Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SQLite;


namespace Alert_to_Care.Repository
{
    public class ICUDataRepository :IICUData
    {
        string cs = @"URI=file:C:\Users\320105541\OneDrive - Philips\Desktop\newBoot\alert-to-care-s22b8\Alert-to-Care\ICU.db";
        SQLiteConnection con;

        public ICUDataRepository()
        {
            con = new SQLiteConnection(cs,true);

            try
            {
                if (con.OpenAndReturn() == null)
                    throw new FileNotFoundException();

            }
            catch (FileNotFoundException e)
            {
                con.Close();
            }

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS ICU
             (Id      INTEGER NOT NULL PRIMARY KEY,
              NumberOfBeds INTEGER NOT NULL,
              Layout CHAR(2) NOT NULL)";
            cmd.ExecuteNonQuery();

        }

        public List<ICUModel> GetAllICU()
        {
            string stm = "SELECT * FROM ICU";

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine("inside get");

            List<ICUModel> listOfIcu = new List<ICUModel>();
            while (rdr.Read())
            {
                ICUModel iCUObject = new ICUModel();
                iCUObject.id = (int)Convert.ToInt64(rdr["Id"]);
                iCUObject.NumberOfBeds = (int)Convert.ToInt64(rdr["NumberOfBeds"]);
                iCUObject.Layout = Convert.ToChar(rdr["Layout"]);

                
                listOfIcu.Add(iCUObject);
            }

            return listOfIcu;
        }

        public void RegisterNewICU(UserInput userInput) {
           
            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = @"INSERT INTO ICU(NumberOfBeds, Layout) VALUES('" + userInput.NumberOfBeds + "','" + userInput.Layout + "')";
            cmd.ExecuteNonQuery();
  } 
       
        public ICUModel ViewICU(int id)
        {
   
            string stm = @"SELECT * FROM ICU WHERE Id=" + id;

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            if (!rdr.Read())
                throw new Exception();

            ICUModel iCUModel = new ICUModel();
            
            iCUModel.id = (int)Convert.ToInt64(rdr["Id"]);
            iCUModel.NumberOfBeds = (int)Convert.ToInt64(rdr["NumberOfBeds"]);
            iCUModel.Layout =Convert.ToChar(rdr["Layout"]);

                    
            
            return iCUModel;
        }

        public void DeleteICU(int id)
        {
            string com = @"SELECT COUNT(*) AS Count FROM ICU WHERE id=" + id;
            using var check = new SQLiteCommand(com, con);
            using SQLiteDataReader sQLiteDataReader = check.ExecuteReader();
            var countOfIcu = 0;
            if (sQLiteDataReader.Read())
                countOfIcu = (int)Convert.ToInt64(sQLiteDataReader["Count"]);
            if (countOfIcu == 0) {
                throw new Exception();
            }


            string stm = @"DELETE FROM ICU WHERE id=" + id;

            using var cmd = new SQLiteCommand(stm, con);
            cmd.ExecuteNonQuery();

            string cs2 = @"URI=file:C:\Users\320105541\OneDrive - Philips\Desktop\newBoot\alert-to-care-s22b8\Alert-to-Care\Patient.db";
            SQLiteConnection con2 = new SQLiteConnection(cs2, true);

            try
            {
                if (con2.OpenAndReturn() == null)
                    throw new FileNotFoundException();

            }
            catch (FileNotFoundException e)
            {
                con2.Close();
            }

            string stm2 = @"DELETE FROM Patient where IcuId=" + id;
            using var cmd2 = new SQLiteCommand(stm2, con2);
            cmd2.ExecuteNonQuery();

        }

       


    }
}