using System.Collections.Generic;
using Models;
using Microsoft.AspNetCore.Mvc;
using Alert_to_Care.Repository;
using System;


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
            if (allIcu == null)
            {
                return NotFound();
            }
            else { 
                return Ok(allIcu);
            }
        }

        //GET api/<ICUConfigController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ICUModel icu;
            try
            {
                icu = icuDataRep.ViewICU(id);
                return Ok(icu);
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        //POST api/<ICUConfigController>
        [HttpPost("register")]
        public IActionResult Post([FromBody] UserInput value)
        {
            icuDataRep.RegisterNewICU(value);
            return Ok();
        }

        // DELETE api/<ICUConfigController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                icuDataRep.DeleteICU(id);
                return Ok();
                
            }
            catch (Exception)
            {
                return NotFound();
            }
        }  


    }
}
