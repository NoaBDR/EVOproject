using Microsoft.AspNetCore.Mvc;
using EVO_ServerSide.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EVO_ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        // GET: api/<ProducersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProducersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProducersController>
        [HttpPost]
        public int Post([FromBody] Producer newProducer)
        {
            int producerID = newProducer.InsertNewUser();
            return producerID;
        }

        // PUT api/<ProducersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProducersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
