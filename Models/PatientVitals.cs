using System.Collections.Generic;

namespace Models
{
    public class PatientVitals
    {
        public int Id { get; set; }
        public List<int> Vitals { get; set; }
    }
}