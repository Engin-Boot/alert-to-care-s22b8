using System;
using Microsoft.AspNetCore.Mvc;
using Models;
using Alert_to_Care.Repository;


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
            this.patientRepo = patientData;
        }

        // GET: api/<OccupancyManagementController>
        //[HttpGet]
        //public List<PatientModel> Get()
        //{
        //    return ;
        //}

        //public HttpResponseMessage Get(int id)
        //{
        //    Student stud = GetStudentFromDB(id);

        //    if (stud == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound, id);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, stud);
        //}

        // GET api/<OccupancyManagementController>/<icuId>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var allPatients=patientRepo.GetAllPatientsInTheICU(id);
            if(allPatients.Count!=0)
                return Ok(allPatients);
            return NotFound();
        }

        // GET api/<OccupancyManagementController>/<GetPatientById>/<patientId>
        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult GetPatientById(int id)
        {
            var patient=patientRepo.GetPatient(id);
            if (patient != null)
                return Ok(patient);
            return NotFound();

        }

        // POST api/<OccupancyManagementController>/{icuId}
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody] PatientDetailsInput patient)
        {
            bool result = patientRepo.AddNewPatient(id, patient);
            if (result)
                return Ok();
            return NotFound();

        }

        
        // DELETE api/<OccupancyManagementController>/<patient>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                patientRepo.DischargePatient(id);
                return Ok();
            }
            catch(Exception)
            {
                
                return NotFound();
            }
            
        }

        //PUT api/<ICUConfigController>/<patient id>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PatientModel value)
        {
            try
            {
                patientRepo.DischargePatient(id);
                patientRepo.RegisterNewPatinetWithGivenId(id, value);
                return Ok();

            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
