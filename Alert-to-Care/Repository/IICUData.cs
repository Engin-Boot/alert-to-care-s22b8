using System.Collections.Generic;
using Models;

namespace Alert_to_Care.Repository
{
    public interface IIcuData
    {
        //public List<ICUModel> GetAllICU();
        //public void RegisterNewICU(UserInput newICU);
        //public ICUModel ViewICU(int id);

        public List<IcuModel> GetAllIcu();
        public void RegisterNewIcu(UserInput newIcu);

        public IcuModel ViewIcu(int id);

        public bool DeleteIcu(int id);

        public void RegisterNewIcuWithGivenId(int id, UserInput value);
    }
}