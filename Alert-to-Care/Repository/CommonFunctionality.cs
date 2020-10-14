using System;
using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alert_to_Care.Repository
{
    public class CommonFunctionality
    {
        public void OpenFile(String cs1, SQLiteConnection con1)
        {
            con1 = new SQLiteConnection(cs1, true);

            try
            {
                if (con1.OpenAndReturn() == null)
                    throw new FileNotFoundException();

            }
            catch (FileNotFoundException e)
            {
                con1.Close();
            }
        }

        public int CheckIfICUExists(string com,SQLiteConnection con)
        {
            using var check = new SQLiteCommand(com, con);
            using SQLiteDataReader sQLiteDataReader = check.ExecuteReader();
            var countOfIcu = 0;
            if (sQLiteDataReader.Read())
                countOfIcu = (int)Convert.ToInt64(sQLiteDataReader["Count"]);
            if (countOfIcu == 0)
            {
                throw new Exception();
            }
            return countOfIcu;
        }
    }
}
