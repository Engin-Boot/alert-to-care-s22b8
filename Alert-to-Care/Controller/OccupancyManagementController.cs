using Alert_to_Care.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Alert_to_Care.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupancyManagementController : ControllerBase
    {
        public IPatientData patientRepo;

        public OccupancyManagementController(IPatientData patientData)
        {
            patientRepo = patientData;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var allPatients = patientRepo.GetAllPatientsInTheICU(id);
            if (allPatients.Count != 0)
                return Ok(allPatients);
            return Ok(allPatients);
        }

        // GET api/<OccupancyManagementController>/<GetPatientById>/<patientId>
        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult GetPatientById(int id)
        {
            var patient = patientRepo.GetPatient(id);
            if (patient != null)
                return Ok(patient);
            patient = new PatientModel();
            return Ok(patient);
        }

        // POST api/<OccupancyManagementController>/{icuId}
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody] PatientDetailsInput patient)
        {
            var result = patientRepo.AddNewPatient(id, patient);
            if (result)
            {
                var message = new Message();
                message.Messages = "Registered Sucessfully!";
                return Ok(message);
            }
            else
            {
                var message = new Message();
                message.Messages = "Registration UnSucessfull - Bed not Available!";
                return Ok(message);
            }
        }


        // DELETE api/<OccupancyManagementController>/<patient>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var present = patientRepo.DischargePatient(id);
            if (present)
            {
                var message = new Message();
                message.Messages = "Patient ID : " + id + " deleted!";
                return Ok(message);
            }
            else
            {
                var message = new Message();
                message.Messages = "Patient ID : " + id + " not registered!";
                return Ok(message);
            }
        }

        //PUT api/<ICUConfigController>/<patient id>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PatientModel value)
        {
            var present = patientRepo.DischargePatient(id);
            if (present)
            {
                patientRepo.RegisterNewPatinetWithGivenId(id, value);

                var message = new Message();
                message.Messages = "Updated Successfully!";
                return Ok(message);
            }
            else
            {
                var message = new Message();
                message.Messages = "Id Not Present - Update Unsuccessfull!";
                return Ok(message);
            }
        }
    }
}