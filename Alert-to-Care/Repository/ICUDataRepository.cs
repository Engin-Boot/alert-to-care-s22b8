using Models;
using System.Collections.Generic;

namespace Alert_to_Care.Repository
{
    public class ICUDataRepository:IICUData
    {

        List<ICUModel> listOfICU=new List<ICUModel>();
        static int ICUId=0;

        public ICUDataRepository()
        {


        }

        public List<ICUModel> GetAllICU()
        {
            return listOfICU;
        }


        public void RegisterNewICU(UserInput userInput)
        {
            ICUModel newICU = new ICUModel();
            newICU.id = ICUId++;
            newICU.Beds = new Bed[newICU.NumberOfBeds];
            newICU.NumberOfBeds = userInput.NumberOfBeds;
            newICU.Layout = userInput.Layout;
            for(int i = 0; i < newICU.NumberOfBeds; i++)
            {
                newICU.Beds[i].id = "ICU:" + newICU.id + "|Bed:" + i;
            }
            listOfICU.Add(newICU);


        }
        public Models.ICUModel ViewICU(int id)
        {
            ICUModel iCUModel = new ICUModel();
            for (int i = 0; i < listOfICU.Count; i++) {
                if (listOfICU[i].id == id) {
                    iCUModel = listOfICU[i];
                }
            }
            return iCUModel;
        }
    }
}
