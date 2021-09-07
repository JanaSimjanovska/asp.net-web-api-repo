using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public ActionResult<IEnumerable<string>> LongNameForMethodThatGetsAllUsers()
        {
            return Ok(StaticDB.Users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<string> AnotherNonimportantMethodNameReturnsOneUser(int id)
        {
            try
            {

                if (id < 0)
                {
                    return BadRequest("Id cannot be a negative number.");
                }
                if (id >= StaticDB.Users.Count)
                {
                    return NotFound($"There is no user with id {id} in the database.");
                }
                return Ok(StaticDB.Users[id]);
            }
            catch
            {
                // Znam deka ne e dobra praksa da se vaka neuniformno napisani status kodovive, i pogore sakav da ja probam drugata sintaksa od dvete od cas, ama za 500 ne mozev da najdam ime za da go napisam kako pogornite sto se napisani.
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
        }

        [HttpGet("getThisRequestPath")]
        public ActionResult<string> GetPath()
        {
            var request = Request;
            return Ok(request.Path.ToString());
        }
    }
}
