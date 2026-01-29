using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyApp.Namespace.Models;
using MyApp.Namespace.Services;

namespace MyApp.Namespace.Controllers
{
    [Route("api/fox")]
    [ApiController]
    public class FoxController : ControllerBase
    {
        private readonly IFoxService _service;

        public FoxController(IFoxService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Fox>> GetAll()
        {
            var foxes = _service.GetAll();
            if (!foxes.Any())
            {
                return NotFound();
            }
            
            return Ok(foxes);
        }


        [HttpGet("{id}")]
        public ActionResult<Fox> Get(int id)
        {
            var fox = _service.Get(id);
            if (fox == null)
            {
                return NotFound();
            }
            return Ok(fox);
        }

        [HttpPost, Authorize]
        public ActionResult Add([FromBody] Fox f)
        {
            var created = _service.Add(f);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Fox f)
        {
            var ok = _service.Update(id, f);
            if (!ok)
                return NotFound();

            return NoContent();
        }

        [HttpPut("love/{id}")]
        public IActionResult Love(int id)
        {
            var fox = _service.Love(id);
            if (fox == null)
                return NotFound();

            return Ok(fox);
        }

        [HttpPut("hate/{id}")]
        public IActionResult Hate(int id)
        {
            var fox = _service.Hate(id);
            if (fox == null)
                return NotFound();

            return Ok(fox);
        }

        [HttpGet("throw")]
        public IActionResult Throw()
        {
            throw new Exception("Testowy wyjątek");
        }

    }
}
