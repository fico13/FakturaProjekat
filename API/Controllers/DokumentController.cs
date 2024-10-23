using Application.CQRS.Requests.Commands.Dokument;
using Application.CQRS.Requests.Queries.Dokument;
using Application.DTOs;
using Application.Mappers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DokumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/DokumentEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DokumentEntity>>> GetDokumenti()
        {
            var dokumenti = await _mediator.Send(new GetDokumentListQuery());

            if (dokumenti == null || !dokumenti.Any()) return NotFound();

            return Ok(dokumenti.Select(d => d.ToDokumentDTO()).ToList());
        }

        [HttpGet("search")]
        public async Task<ActionResult<IReadOnlyList<DokumentDTO>>> GetDokumentByBrojDokumenta([FromQuery] string brojDokumenta)
        {
            var dokumenti = await _mediator.Send(new GetDokumentByBrojDokumentaQuery(brojDokumenta));
            if (dokumenti == null || !dokumenti.Any())
            {
                return NotFound();
            }
            return Ok(dokumenti.Select(d => d.ToDokumentDTO()).ToList());
        }

        // PUT: api/DokumentEntities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutDokumentEntity([FromBody] DokumentDTO dokumentDTO)
        {
            var dokument = await _mediator.Send(new UpdateDokumentCommand(dokumentDTO.ToDokumentEntity()));

            if (dokument == null) return NotFound();

            return Ok();
        }

        // POST: api/DokumentEntities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DokumentEntity>> AddDokument([FromBody] DokumentDTO dokumentDTO)
        {
            var dokumentEntity = await _mediator.Send(new AddDokumentCommand(dokumentDTO.ToDokumentEntity()));

            if (dokumentEntity == null) return NotFound();

            return Ok();
        }

        // DELETE: api/DokumentEntities/5
        [HttpDelete("{brojDokumenta}")]
        public async Task<IActionResult> DeleteDokumentEntity([FromRoute] string brojDokumenta)
        {
            var successful = await _mediator.Send(new DeleteDokumentCommand(brojDokumenta));

            if (!successful) return BadRequest();

            return NoContent();
        }


    }
}
