using Application.CQRS.Requests.Commands.Roba;
using Application.CQRS.Requests.Queries.Roba;
using Application.DTOs;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RobaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Roba
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RobaEntity>>> GetRoba()
        {
            var roba = await _mediator.Send(new GetRobaListQuery());

            if (roba == null || !roba.Any()) return NotFound();

            return Ok(roba);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IReadOnlyList<KomitentDTO>>> GetRobaBySifraRobe([FromQuery] string sifraRobe)
        {
            var roba = await _mediator.Send(new GetRobaBySifraQuery(sifraRobe));
            if (roba == null || !roba.Any())
            {
                return NotFound();
            }
            return Ok(roba);
        }

        // PUT: api/Roba/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateRobaEntity([FromBody] RobaDTO robaDTO)
        {
            var robaEntity = await _mediator.Send(new UpdateRobaCommand(robaDTO));

            if (robaEntity == null) return NotFound();

            return Ok();
        }

        // POST: api/Roba
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RobaEntity>> AddRoba([FromBody] RobaDTO robaDTO)
        {
            var robaEntity = await _mediator.Send(new AddRobaCommand(robaDTO));

            if (robaEntity == null) return BadRequest();

            return Ok();
        }

        // DELETE: api/Roba/5
        [HttpDelete("{sifraRobe}")]
        public async Task<IActionResult> DeleteRobaEntity([FromRoute] string sifraRobe)
        {
            var successful = await _mediator.Send(new DeleteRobaCommand(sifraRobe));

            if (!successful) return NotFound();

            return NoContent();
        }


    }
}
