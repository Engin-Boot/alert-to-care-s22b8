using System;
using System.Collections.Generic;

namespace Models
{
    public class Bed
    {
        public string id { get; set; }
        public bool isOccupied { get; set; }
    }
    public class ICUModel
    {
        public int id { get; set; }
        public int numberOfBeds { get; set; }
       
        public List<Bed> beds {get; set; }

        public char layout { get; set; }

    }

    public class UserInput
    {
        public int numberOfBeds { get; set; }
        public char layout { get; set; }
    }
}
