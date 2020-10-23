﻿using Alert_to_Care.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Alert_to_Care.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ICUConfigController : ControllerBase
    {
        public IICUData icuDataRep;

        public ICUConfigController(IICUData iCUData)
        {
            icuDataRep = iCUData;
        }

        // GET: api/<ICUConfigController>
        [HttpGet]
        public IActionResult Get()
        {
            var allIcu = icuDataRep.GetAllICU();
            return Ok(allIcu);
        }

        //GET api/<ICUConfigController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ICUModel icu;
            icu = icuDataRep.ViewICU(id);
            if (icu != null)
            {
                return Ok(icu);
            }

            icu = new ICUModel();
            return Ok(icu);
        }

        //POST api/<ICUConfigController>
        [HttpPost("register")]
        public IActionResult Post([FromBody] UserInput value)
        {
            icuDataRep.RegisterNewICU(value);
            var message = new Message();
            message.Messages = "Registered Sucessfully!";
            return Ok(message);
        }

        // DELETE api/<ICUConfigController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isPresent = icuDataRep.DeleteICU(id);
            if (isPresent)
            {
                var message = new Message();
                message.Messages = "ICU ID : " + id + " deleted!";
                return Ok(message);
            }
            else
            {
                var message = new Message();
                message.Messages = "ICU ID : " + id + " not registered!";
                return Ok(message);
            }
        }

        //PUT api/<ICUConfigController>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserInput value)
        {
            var isPresent = icuDataRep.DeleteICU(id);
            if (isPresent)
            {
                icuDataRep.RegisterNewICUWithGivenId(id, value);
                var message = new Message();
                message.Messages = "ICU ID : " + id + " Updated!";
                return Ok(message);
            }
            else
            {
                var message = new Message();
                message.Messages = "ICU ID : " + id + " not registered!";
                return Ok(message);
            }
        }
    }
}