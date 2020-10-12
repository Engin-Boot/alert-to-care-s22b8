using System;
using System.Collections.Generic;
using System.Text;

namespace Alert_to_care.tests.Models
{
    public class OccupancyMgmtModel
    {
        public string name { get; set; }
        public int age { get; set; }
        public string bloodGroup { get; set; }
        public string address { get; set; }
    }

    public class PatientModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string bloodGroup { get; set; }
        public string address { get; set; }
        public int icuId { get; set; }
        public int bedNumber { get; set; }
    }

    public class Root
    {
        public List<PatientModel> MyArray { get; set; }
    }

}
