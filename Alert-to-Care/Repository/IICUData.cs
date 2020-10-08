using Models;
using System.Collections.Generic;

namespace Alert_to_Care.Repository
{
    public interface IICUData
    {
        //public List<ICUModel> GetAllICU();
        //public void RegisterNewICU(UserInput newICU);

        //public ICUModel ViewICU(int id);

        public ICUContext GetAllICU();
        public void RegisterNewICU(UserInput newICU);

        //public ICUModel ViewICU(int id);
    }
}
