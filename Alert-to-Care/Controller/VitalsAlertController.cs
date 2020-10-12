using System.Collections.Generic;
using Alert_to_Care.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Alert_to_Care.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VitalsAlertController : ControllerBase
    {
        readonly IVitalsCheckerRepository vitalsChecker;

        public VitalsAlertController(IVitalsCheckerRepository _vitalsChecker)
        {
            vitalsChecker = _vitalsChecker;
        }
        

       

        // POST api/<VitalsAlertController>
        [HttpPost]
        public void Post([FromBody] List<PatientVitals> allPatientVitals)
        {
            vitalsChecker.CheckVitals(allPatientVitals);
        }

       

       
    }
}
