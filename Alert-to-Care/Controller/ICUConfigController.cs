using System.Collections.Generic;
using Models;
using Microsoft.AspNetCore.Mvc;
using Alert_to_Care.Repository;
using System.Data.SQLite;
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
        public List<ICUModel> Get()
        {
            return icuDataRep.GetAllICU();
        }

        //GET api/<ICUConfigController>/5
        [HttpGet("{id}")]
        public ICUModel Get(int id)
        {
            return icuDataRep.ViewICU(id);
        }

        //POST api/<ICUConfigController>
        [HttpPost("register")]
        public void Post([FromBody] UserInput value)
        {
            icuDataRep.RegisterNewICU(value);
        }

        // DELETE api/<ICUConfigController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                icuDataRep.DeleteICU(id);
                
            }
            catch (Exception)
            {
                Console.WriteLine("this icu does not exist");
            }
        }

        


    }
}
