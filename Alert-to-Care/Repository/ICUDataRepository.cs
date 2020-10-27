using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Models;

namespace Alert_to_Care.Repository
{
    public class IcuDataRepository : CommonFunctionality, IIcuData
    {
        //string cs = @"URI=file:C:\Users\320104085\OneDrive - Philips\Bootcamp\Alert-To-Care\alert-to-care-s22b8\Alert-to-Care\ICU.db";

        private readonly SQLiteConnection _con;


        private readonly string _cs = @"URI=file:" + Directory.GetCurrentDirectory() + @"\ICU.db";

        public IcuDataRepository()
        {
            _con = OpenFile(_cs);

            using var cmd = new SQLiteCommand(_con)
            {
                CommandText = @"CREATE TABLE IF NOT EXISTS ICU
             (Id      INTEGER NOT NULL PRIMARY KEY,
              NumberOfBeds INTEGER NOT NULL,
              Layout CHAR(2) NOT NULL)"
            };

            cmd.ExecuteNonQuery();

            Console.WriteLine("path:" + _cs);
        }

        public List<IcuModel> GetAllIcu()
        {
            var stm = "SELECT * FROM ICU";

            using var cmd = new SQLiteCommand(stm, _con);
            using var rdr = cmd.ExecuteReader();
            Console.WriteLine("inside get");

            var listOfIcu = new List<IcuModel>();
            while (rdr.Read())
            {
                /* var iCuObject = new IcuModel
                 {
                     Id = (int) Convert.ToInt64(rdr["Id"]),
                     NumberOfBeds = (int) Convert.ToInt64(rdr["NumberOfBeds"]),
                     Layout = Convert.ToChar(rdr["Layout"])
                 };*/
                var iCuObject = createIcuModel(rdr);
                listOfIcu.Add(iCuObject);
            }

            return listOfIcu;
        }

        public void RegisterNewIcu(UserInput userInput)
        {
            using var cmd = new SQLiteCommand(_con)
            {
                CommandText = @"INSERT INTO ICU(NumberOfBeds, Layout) VALUES('" + userInput.NumberOfBeds + "','" +
                              userInput.Layout + "')"
            };
            cmd.ExecuteNonQuery();
        }

        public IcuModel ViewIcu(int id)
        {
            var stm = @"SELECT * FROM ICU WHERE Id=" + id;

            using var cmd = new SQLiteCommand(stm, _con);
            using var rdr = cmd.ExecuteReader();

            if (!rdr.Read())
                return null;

            /*var iCuModel1 = new IcuModel
            {
                Id = (int) Convert.ToInt64(rdr["Id"]),
                NumberOfBeds = (int) Convert.ToInt64(rdr["NumberOfBeds"]),
                Layout = Convert.ToChar(rdr["Layout"])
            };*/
            var iCuModel1 = createIcuModel(rdr);
            return iCuModel1;
        }

        public bool DeleteIcu(int id)
        {
            try
            {
                var com = @"SELECT COUNT(*) AS Count FROM ICU WHERE id=" + id;

                CheckIfIcuExists(com, _con);


                var stm = @"DELETE FROM ICU WHERE id=" + id;

                using var cmd = new SQLiteCommand(stm, _con);
                cmd.ExecuteNonQuery();

                //string cs2 = @"URI=file:C:\Users\320104085\OneDrive - Philips\Bootcamp\Alert-To-Care\alert-to-care-s22b8\Alert-to-Care\Patient.db";
                var cs2 = @"URI=file:" + Directory.GetCurrentDirectory() + @"\" + "Patient.db";
                var con2 = OpenFile(cs2);

                var stm2 = @"DELETE FROM Patient where IcuId=" + id;
                using var cmd2 = new SQLiteCommand(stm2, con2);
                cmd2.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RegisterNewIcuWithGivenId(int id, UserInput value)
        {
            using var cmd = new SQLiteCommand(_con)
            {
                CommandText = @"INSERT INTO ICU(Id, NumberOfBeds, Layout) VALUES('" + id + "','" + value.NumberOfBeds +
                              "','" + value.Layout + "')"
            };
            //cmd.CommandText = @"INSERT INTO ICU(Id, NumberOfBeds, Layout) VALUES("+id+ "," +value.NumberOfBeds+"," +value.Layout+")";
            cmd.ExecuteNonQuery();
        }

        private IcuModel createIcuModel(SQLiteDataReader rdr)
        {
            var iCuObject = new IcuModel
            {
                Id = (int) Convert.ToInt64(rdr["Id"]),
                NumberOfBeds = (int) Convert.ToInt64(rdr["NumberOfBeds"]),
                Layout = Convert.ToChar(rdr["Layout"])
            };
            return iCuObject;
        }
    }
}