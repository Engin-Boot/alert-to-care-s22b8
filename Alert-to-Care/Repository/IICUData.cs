using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alert_to_Care.Repository
{
    public interface IICUData
    {
        public List<Models.ICUModel> GetAllICU();

        public void RegisterNewICU(Models.ICUModel newICU);

        public Models.ICUModel ViewICU(int id);
    }
}
