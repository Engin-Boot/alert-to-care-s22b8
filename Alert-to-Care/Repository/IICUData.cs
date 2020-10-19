using Models;
using System.Collections.Generic;


namespace Alert_to_Care.Repository
{
    public interface IICUData
    {
        //public List<ICUModel> GetAllICU();
        //public void RegisterNewICU(UserInput newICU);
        //public ICUModel ViewICU(int id);

        public List<ICUModel> GetAllICU();
        public bool RegisterNewICU(UserInput newICU);

        public ICUModel ViewICU(int id);

        public bool DeleteICU(int id);

        public bool RegisterNewICUWithGivenId(int id, UserInput value);

    }
}
