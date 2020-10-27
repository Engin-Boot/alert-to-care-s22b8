using System;
using System.Data.SQLite;
using System.IO;

namespace Alert_to_Care.Repository
{
    public class CommonFunctionality
    {
        protected SQLiteConnection OpenFile(string cs1)
        {
            var con1 = new SQLiteConnection(cs1, true);

            try
            {
                if (con1.OpenAndReturn() == null)
                {
                    con1.Close();
                    throw new FileNotFoundException();
                }

                //con1.Open()
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }

            return con1;
        }

        protected void CheckIfIcuExists(string com, SQLiteConnection con)
        {
            using var check = new SQLiteCommand(com, con);
            using var sQLiteDataReader = check.ExecuteReader();
            var countOfIcu = 0;
            if (sQLiteDataReader.Read())
                countOfIcu = (int) Convert.ToInt64(sQLiteDataReader["Count"]);
            if (countOfIcu == 0) throw new Exception();
            // return countOfIcu;
        }
    }
}