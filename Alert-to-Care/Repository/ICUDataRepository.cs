using Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Alert_to_Care.Repository
{
    public class ICUDataRepository :IICUData
    {

        //List<ICUModel> listOfICU = new List<ICUModel>();
        ICUContext db = new ICUContext();
        static int ICUId = 0;

        public ICUDataRepository()
        {
            //RegisterNewICU(new UserInput
            //{
            //    NumberOfBeds = 4,
            //    Layout = 'H'
            //});

            //RegisterNewICU(new UserInput
            //{
            //    NumberOfBeds = 6,
            //    Layout = 'L'
            //});

            db.Add(new ICUContext
            {
                NumberOfBeds = 4,
                Layout = 'H'
            });


            db.Add(new ICUContext
            {
                NumberOfBeds = 6,
                Layout = 'L'
            });

            db.SaveChanges();

        }

        //public List<ICUModel> GetAllICU()
        //{
        //    return listOfICU;
        //}

        public ICUContext GetAllICU()
        {
            return db;
        }

        public void RegisterNewICU(UserInput userInput)
        {
            //ICUModel newICU = new ICUModel();


            //newICU.id = ICUId++;
            //newICU.Beds = new Bed[userInput.NumberOfBeds];
            //newICU.NumberOfBeds = userInput.NumberOfBeds;
            //newICU.Layout = userInput.Layout;


            //for (int i = 0; i < newICU.Beds.Length; i++)
            //{
            //    newICU.Beds[i] = new Bed();
            //    newICU.Beds[i].id = "ICU:" + newICU.id + "|Bed:" + i;
            //    newICU.Beds[i].isOccupied = false;
            //}
            //listOfICU.Add(newICU);

            ICUContext newICU = new ICUContext();


            newICU.id = ICUId++;
            newICU.Beds = new Bed[userInput.NumberOfBeds];
            newICU.NumberOfBeds = userInput.NumberOfBeds;
            newICU.Layout = userInput.Layout;


            for (int i = 0; i < newICU.Beds.Length; i++)
            {
                newICU.Beds[i] = new Bed();
                newICU.Beds[i].id = "ICU:" + newICU.id + "|Bed:" + i;
                newICU.Beds[i].isOccupied = false;
            }
            db.Add(newICU);
            db.SaveChanges();


        }
        //public Models.ICUModel ViewICU(int id)
        //{
        //    ICUModel iCUModel = new ICUModel();
        //    for (int i = 0; i < listOfICU.Count; i++)
        //    {
        //        if (listOfICU[i].id == id)
        //        {
        //            iCUModel = listOfICU[i];
        //        }
        //    }
        //    return iCUModel;
        //}

        //public ICUContext ViewICU(int ICUid)
        //{
        //    ICUContext iCUModel = db
        //            .Where(b => b.id==ICUid);


        //    return iCUModel;
        //}
    }
}