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
        private readonly IVitalsCheckerRepository _vitalsChecker;

        public VitalsAlertController(IVitalsCheckerRepository vitalsChecker)
        {
            this._vitalsChecker = vitalsChecker;
        }


        // POST api/<VitalsAlertController>
        [HttpPost]
        public IActionResult Post([FromBody] List<PatientVitals> allPatientVitals)
        {
            _vitalsChecker.CheckVitals(allPatientVitals);
            var message = new Message {Messages = "Alert Sent!!"};
            return Ok(message);
        }
    }
}