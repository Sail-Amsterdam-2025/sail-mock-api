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
        public IActionResult Get()
        {
            string[] t = new string[] { "value1", "value2" };
            return new OkObjectResult(t);
        }

        // GET api/<ExampleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new OkObjectResult("value");
        }

        // POST api/<ExampleController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return new OkObjectResult("value");
        }

        // PUT api/<ExampleController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return new OkObjectResult("value");
        }

        // DELETE api/<ExampleController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new OkObjectResult("value");
        }
    }
}
