using ASP_NET_CORE_CPP_ENTRY.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_CORE_CPP_ENTRY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TetrisController : ControllerBase
    {
        private readonly TetrisService _tetrisService;

        public TetrisController(TetrisService tetrisService)
        {
            _tetrisService = tetrisService;
        }

        [HttpGet("state")]
        public ActionResult<TetrisService.TetrisState> GetState()
        {
            return Ok(_tetrisService.GetState());
        }

        [HttpPost("step")]
        public IActionResult Step()
        {
            _tetrisService.Step();
            return Ok();
        }

        [HttpPost("reset")]
        public IActionResult Reset()
        {
            _tetrisService.Reset();
            return Ok();
        }

        [HttpPost("load-model")]
        public ActionResult<bool> LoadModel([FromBody] string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return BadRequest("Filename required");

            bool success = _tetrisService.LoadModel(filename);
            return Ok(success);
        }
    }
}
