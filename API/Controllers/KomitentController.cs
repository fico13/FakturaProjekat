using Application.CQRS.Requests.Commands.Komitent;
using Application.CQRS.Requests.Queries.Komitent;
using Application.DTOs;
using Application.Mappers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KomitentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KomitentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Komitent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KomitentDTO>>> GetKomitenti()
        {
            var komitenti = await _mediator.Send(new GetKomitentsListQuery());
            if (komitenti == null || !komitenti.Any()) return NotFound();

            return Ok(komitenti.Select(k => k.ToKomitentDTO()).ToList());
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<KomitentDTO>>> GetKomitentBySifra([FromQuery] string sifraKomitenta)
        {
            var komitenti = await _mediator.Send(new GetKomitentBySifraQuery(sifraKomitenta));
            if (komitenti == null || !komitenti.Any())
            {
                return NotFound();
            }
            return Ok(komitenti.Select(k => k.ToKomitentDTO()).ToList());
        }


        [HttpPut]
        public async Task<IActionResult> UpdateKomitentEntity([FromBody] KomitentDTO komitentDTO)
        {
            var komitentEntity = await _mediator.Send(new UpdateKomitentCommand(komitentDTO.ToKomitentEntity()));

            if (komitentEntity == null) return NotFound();

            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<KomitentEntity>> AddKomitentEntity([FromBody] KomitentDTO komitentDTO)
        {
            var komitentEntity = await _mediator.Send(new AddKomitentCommand(komitentDTO.ToKomitentEntity()));
            if (komitentEntity == null) return NotFound();
            return Ok();
        }


        [HttpDelete("{sifraKomitenta}")]
        public async Task<IActionResult> DeleteKomitentEntity([FromRoute] string sifraKomitenta)
        {
            var successful = await _mediator.Send(new DeleteKomitentCommand(sifraKomitenta));

            if (!successful) return NotFound();

            return NoContent();
        }
    }
}
