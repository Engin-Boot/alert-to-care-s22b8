using System;

namespace Models
{
    public class ICUModel
    {
        public int id { get; set; }
        public int NumberOfBeds { get; set; }
       
        public Bed[] Beds {get; set; }

        char Layout { get; set; }

    }
}
