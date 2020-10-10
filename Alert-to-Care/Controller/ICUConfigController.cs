using System.Collections.Generic;
using Models;
using Microsoft.AspNetCore.Mvc;
using Alert_to_Care.Repository;
using System.Data.SQLite;

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
            this.icuDataRep = iCUData;    
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

        // PUT api/<ICUConfigController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ICUConfigController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            icuDataRep.DeleteICU(id);
        }

        [HttpDelete]
        public void Delete()
        {
            icuDataRep.EmptyDB();
        }


    }
}
