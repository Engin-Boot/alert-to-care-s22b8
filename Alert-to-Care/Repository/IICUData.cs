using Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Alert_to_Care.Repository
{
    public interface IICUData
    {
        //public List<ICUModel> GetAllICU();
        //public void RegisterNewICU(UserInput newICU);
        //public ICUModel ViewICU(int id);

        public List<ICUModel> GetAllICU();
        public void RegisterNewICU(UserInput newICU);

        public ICUModel ViewICU(int id);

        public void DeleteICU(int id);

    }
}
