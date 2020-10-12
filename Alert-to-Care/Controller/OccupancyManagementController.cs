﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET api/<OccupancyManagementController>/5
        [HttpGet("{id}")]
        public List<PatientModel> Get(int id)
        {
            return patientRepo.GetAllPatientsInTheICU(id);
        }

        // GET api/<OccupancyManagementController>/<GetPatientById>/5
        [Route("[action]/{id}")]
        [HttpGet]
        public PatientModel GetPatientById(int id)
        {
            return patientRepo.GetPatient(id);
        }

        // POST api/<OccupancyManagementController>
        [HttpPost("{id}")]
        public bool Post(int id, [FromBody] PatientDetailsInput patient)
        {
            bool result = patientRepo.AddNewPatient(id, patient);
            return result;

        }

        // PUT api/<OccupancyManagementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PatientModel patient)
        {
           
        }

        // DELETE api/<OccupancyManagementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            patientRepo.DischargePatient(id);
        }
    }
}