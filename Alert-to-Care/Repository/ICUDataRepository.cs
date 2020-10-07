using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alert_to_Care.Repository
{
    public class ICUDataRepository:IICUData
    {

        List<Models.ICUModel> listOfICU=new List<Models.ICUModel>();
        static int ICUId=0;

        public ICUDataRepository()
        {


        }

        public List<Models.ICUModel> GetAllICU()
        {
            return listOfICU;
        }


        public void RegisterNewICU(Models.ICUModel newICU)
        {
            newICU.id = ICUId++;
            newICU.Beds = new Models.Bed[newICU.NumberOfBeds];

            for(int i = 0; i < newICU.NumberOfBeds; i++)
            {
                newICU.Beds[i].id = "ICU:" + newICU.id + "|Bed:" + i;
            }
            listOfICU.Add(newICU);


        }
        public Models.ICUModel ViewICU(int id)
        {
            return 
        }
    }
}
