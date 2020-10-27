using Alert_to_Care.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Alert_to_Care.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IcuConfigController : ControllerBase
    {
        private readonly IIcuData _icuDataRep;

        public IcuConfigController(IIcuData iCuData)
        {
            _icuDataRep = iCuData;
        }

        // GET: api/<ICUConfigController>
        [HttpGet]
        public IActionResult Get()
        {
            var allIcu = _icuDataRep.GetAllIcu();
            return Ok(allIcu);
        }

        //GET api/<ICUConfigController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var icu = _icuDataRep.ViewIcu(id);
            if (icu != null)
            {
                return Ok(icu);
            }

            icu = new IcuModel();
            return Ok(icu);
        }

        //POST api/<ICUConfigController>
        [HttpPost("register")]
        public IActionResult Post([FromBody] UserInput value)
        {
            _icuDataRep.RegisterNewIcu(value);
            var message = new Message {Messages = "Registered Sucessfully!"};
            return Ok(message);
        }

        // DELETE api/<ICUConfigController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isPresent = _icuDataRep.DeleteIcu(id);
            if (isPresent)
            {
                var message = new Message {Messages = "ICU ID : " + id + " deleted!"};
                return Ok(message);
            }
            else
            {
                var message = new Message {Messages = "ICU ID : " + id + " not registered!"};
                return Ok(message);
            }
        }

        //PUT api/<ICUConfigController>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserInput value)
        {
            var isPresent = _icuDataRep.DeleteIcu(id);
            if (isPresent)
            {
                _icuDataRep.RegisterNewIcuWithGivenId(id, value);
                var message = new Message {Messages = "ICU ID : " + id + " Updated!"};
                return Ok(message);
            }
            else
            {
                var message = new Message {Messages = "ICU ID : " + id + " not registered!"};
                return Ok(message);
            }
        }
    }
}