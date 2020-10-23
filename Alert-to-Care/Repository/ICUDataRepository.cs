using Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace Alert_to_Care.Repository
{
    public class ICUDataRepository :CommonFunctionality,IICUData
    {
       
   
       
        string cs = @"URI=file:"+ Directory.GetCurrentDirectory() + @"\ICU.db";
        //string cs = @"URI=file:C:\Users\320104085\OneDrive - Philips\Bootcamp\Alert-To-Care\alert-to-care-s22b8\Alert-to-Care\ICU.db";
        
        SQLiteConnection con=null;

        public ICUDataRepository()
        {
            con=OpenFile(cs);

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS ICU
             (Id      INTEGER NOT NULL PRIMARY KEY,
              NumberOfBeds INTEGER NOT NULL,
              Layout CHAR(2) NOT NULL)";
            cmd.ExecuteNonQuery();

            Console.WriteLine("path:"+cs);

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

        public bool RegisterNewICU(UserInput userInput) 
        {
                using var cmd = new SQLiteCommand(con);
                cmd.CommandText = @"INSERT INTO ICU(NumberOfBeds, Layout) VALUES('" + userInput.NumberOfBeds + "','" + userInput.Layout + "')";
                cmd.ExecuteNonQuery();
                return true;
 
        } 
       
        public ICUModel ViewICU(int id)
        {
   
            string stm = @"SELECT * FROM ICU WHERE Id=" + id;

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            if (!rdr.Read())
                return null;

            ICUModel iCUModel = new ICUModel();
            
            iCUModel.id = (int)Convert.ToInt64(rdr["Id"]);
            iCUModel.NumberOfBeds = (int)Convert.ToInt64(rdr["NumberOfBeds"]);
            iCUModel.Layout =Convert.ToChar(rdr["Layout"]);

                    
            
            return iCUModel;
        }

        public bool DeleteICU(int id)
        {
            try
            {
                string com = @"SELECT COUNT(*) AS Count FROM ICU WHERE id=" + id;

                var countOfICU = CheckIfICUExists(com, con);


                string stm = @"DELETE FROM ICU WHERE id=" + id;

                using var cmd = new SQLiteCommand(stm, con);
                cmd.ExecuteNonQuery();

                //string cs2 = @"URI=file:C:\Users\320104085\OneDrive - Philips\Bootcamp\Alert-To-Care\alert-to-care-s22b8\Alert-to-Care\Patient.db";
                string cs2 = @"URI=file:" + Directory.GetCurrentDirectory() + @"\" + "Patient.db";
                SQLiteConnection con2 = OpenFile(cs2);

                string stm2 = @"DELETE FROM Patient where IcuId=" + id;
                using var cmd2 = new SQLiteCommand(stm2, con2);
                cmd2.ExecuteNonQuery();
                return true;
            }
            catch 
            {
                return false;
            }

        }
        public bool RegisterNewICUWithGivenId(int id, UserInput value) {
           
            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = @"INSERT INTO ICU(Id, NumberOfBeds, Layout) VALUES('" + id + "','" + value.NumberOfBeds + "','" + value.Layout+ "')";
            //cmd.CommandText = @"INSERT INTO ICU(Id, NumberOfBeds, Layout) VALUES("+id+ "," +value.NumberOfBeds+"," +value.Layout+")";
            cmd.ExecuteNonQuery();
            return true; 
        }





    }
}
