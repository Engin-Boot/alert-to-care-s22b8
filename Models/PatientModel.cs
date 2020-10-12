using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class PatientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }
        
        public string BloodGroup { get; set; }
        
        public string Address { get; set; }

        public int IcuId { get; set; }

        public int BedNumber { get; set; }
    }
}
