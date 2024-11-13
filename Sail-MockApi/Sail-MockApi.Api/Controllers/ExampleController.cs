using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sail_MockApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        // GET: api/<ExampleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ExampleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ExampleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ExampleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ExampleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}