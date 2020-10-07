using Models;

namespace Alert_to_Care.Repository
{
    public interface IICUData
    {

        public void RegisterNewICU(UserInput newICU);

        public ICUModel ViewICU(int id);
    }
}
