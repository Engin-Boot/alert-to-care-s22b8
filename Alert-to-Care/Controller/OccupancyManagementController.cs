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
            if (allPatients.Count != 0)
                return Ok(allPatients);
            else
            {
                return Ok(allPatients);
            }
        }

        // GET api/<OccupancyManagementController>/<GetPatientById>/<patientId>
        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult GetPatientById(int id)
        {
            PatientModel patient=patientRepo.GetPatient(id);
            if (patient != null)
                return Ok(patient);
            else
                patient = new PatientModel();
            return Ok(patient);

        }

        // POST api/<OccupancyManagementController>/{icuId}
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody] PatientDetailsInput patient)
        {
            bool result = patientRepo.AddNewPatient(id, patient);
            if (result)
            {
                Message message = new Message();
                message.Messages = "Registered Sucessfully!";
                return Ok(message);
            }
            else
            {
                Message message = new Message();
                message.Messages = "Registration UnSucessfull - Bed not Available!";
                return Ok(message);

            }
        }

        
        // DELETE api/<OccupancyManagementController>/<patient>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
             bool present= patientRepo.DischargePatient(id);
                if(present)
                {
                    Message message = new Message();
                    message.Messages = "Patient ID : " + id + " deleted!";
                    return Ok(message);
                }
                else
                {
                    Message message = new Message();
                    message.Messages = "Patient ID : " + id + " not registered!";
                    return Ok(message);

                }
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
               bool present= patientRepo.DischargePatient(id);
                if (present)
                {
                    patientRepo.RegisterNewPatinetWithGivenId(id, value);

                    Message message = new Message();
                    message.Messages = "Updated Successfully!";
                    return Ok(message);
                }
                else
                {
                    Message message = new Message();
                    message.Messages = "Id Not Present - Update Unsuccessfull!";
                    return Ok(message);
                }

            }
            catch (Exception)
            {
                Message message = new Message();
                message.Messages = "Something went wrong!";
                return NotFound(message);
            }
        }
    }
}
