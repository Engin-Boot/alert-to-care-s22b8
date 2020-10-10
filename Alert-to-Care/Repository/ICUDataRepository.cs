using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.SQLite;


namespace Alert_to_Care.Repository
{
    public class ICUDataRepository :IICUData
    {
        string cs = @"URI=file:C:\Users\320105541\OneDrive - Philips\Desktop\boot\alert-to-care-s22b8\Alert-to-Care\ICU.db";
        SQLiteConnection con;


         //List<ICUModel> listOfICU = new List<ICUModel>();
       
        //static int ICUId = 0;

        public ICUDataRepository()
        {
            con = new SQLiteConnection(cs,true);
            con.Open();
            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS ICU
             (Id      INTEGER NOT NULL PRIMARY KEY,
              NumberOfBeds INTEGER NOT NULL,
              Layout CHAR(2) NOT NULL)";
            cmd.ExecuteNonQuery();

            //cmd.CommandText = @"CREATE TABLE IF NOT EXISTS RLATIONS(
            //    IcuId   INTEGER NOT NULL,
            //    BedId   INTEGER NOT NULL,
            //    EmpId   INTEGER NOT NULL,
            //    PRIMARY KEY (IcuId, BedId)
            //)";
            //cmd.ExecuteNonQuery();
            //
            //RegisterNewICU(new UserInput
            //{
            //    NumberOfBeds = 4,
            //    Layout = 'H'
            //});

            //RegisterNewICU(new UserInput
            //{
            //    NumberOfBeds = 6, 
            //    Layout = 'L'
            //});


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
            //{
            //    ICUModel newICU = new ICUModel();


            //    newICU.id = ICUId++;
            //    newICU.Beds = new Bed[userInput.NumberOfBeds];
            //    newICU.NumberOfBeds = userInput.NumberOfBeds;
            //    newICU.Layout = userInput.Layout;


            //    for (int i = 0; i < newICU.Beds.Length; i++)
            //    {
            //        newICU.Beds[i] = new Bed();
            //        newICU.Beds[i].id = "ICU:" + newICU.id + "|Bed:" + i;
            //        newICU.Beds[i].isOccupied = false;
            //    }
            //    listOfICU.Add(newICU);

            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = @"INSERT INTO ICU(NumberOfBeds, Layout) VALUES('" + userInput.NumberOfBeds + "','" + userInput.Layout + "')";
            cmd.ExecuteNonQuery();
  } 
        public Models.ICUModel ViewICU(int id)
        {
    //ICUModel iCUModel = new ICUModel();
    //for (int i = 0; i < listOfICU.Count; i++)
    //{
    //    if (listOfICU[i].id == id)
    //    {
    //        iCUModel = listOfICU[i];
    //    }
    //}
    //return iCUModel;
        string stm = @"SELECT * FROM ICU WHERE id=" + id;

        using var cmd = new SQLiteCommand(stm, con);
        using SQLiteDataReader rdr = cmd.ExecuteReader();

            if (rdr == null)
                return null;

        ICUModel iCUModel = new ICUModel();
            //Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetInt32(1)} {rdr.GetChar(2)}");
            while (rdr.Read())
        {
                iCUModel.id = (int)Convert.ToInt64(rdr["Id"]);
                iCUModel.NumberOfBeds = (int)Convert.ToInt64(rdr["NumberOfBeds"]);
                iCUModel.Layout =Convert.ToChar(rdr["Layout"]);

                Console.WriteLine($"{iCUModel.id} {iCUModel.NumberOfBeds} {iCUModel.Layout}");
        }
            return iCUModel;
    }

        public void DeleteICU(int id)
        {
            string stm = @"DELETE FROM ICU WHERE id=" + id;

            using var cmd = new SQLiteCommand(stm, con);
            cmd.ExecuteNonQuery();
            
        }

        public void EmptyDB()
        {
            string stm = @"DELETE FROM ICU ";

            using var cmd = new SQLiteCommand(stm, con);
            cmd.ExecuteNonQuery();

        }


    }
}