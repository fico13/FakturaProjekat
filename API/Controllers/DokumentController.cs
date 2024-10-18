using Application.Contracts.Interfaces;
using Application.DTOs;
using Application.Mappers;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokumentController : ControllerBase
    {
        private readonly IDokumentRepository _dokumentRepository;

        public DokumentController(IDokumentRepository dokumentRepository)
        {
            _dokumentRepository = dokumentRepository;
        }

        // GET: api/DokumentEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DokumentEntity>>> GetDokumenti()
        {
            var dokumenti = await _dokumentRepository.GetAllAsync();

            if (dokumenti == null || !dokumenti.Any()) return NotFound();

            return Ok(dokumenti.Select(d => d.ToDokumentDTO()).ToList());
        }

        [HttpGet("search")]
        public async Task<ActionResult<IReadOnlyList<DokumentDTO>>> GetDokumentByBrojDokumenta([FromQuery] string brojDokumenta)
        {
            var dokumenti = await _dokumentRepository.GetBySifraAsync(brojDokumenta);
            if (dokumenti == null || !dokumenti.Any())
            {
                return NotFound();
            }
            return Ok(dokumenti.Select(d => d.ToDokumentDTO()).ToList());
        }

        // PUT: api/DokumentEntities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutDokumentEntity([FromQuery] string brojDokumenta, [FromBody] DokumentDTO dokumentDTO)
        {
            var dokument = await _dokumentRepository.UpdateAsync(dokumentDTO.ToDokumentEntity());

            if (dokument == null) return NotFound();

            return Ok();
        }

        // POST: api/DokumentEntities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DokumentEntity>> AddDokument([FromBody] DokumentDTO dokumentDTO)
        {
            var dokumentEntity = await _dokumentRepository.AddAsync(dokumentDTO.ToDokumentEntity());

            if (dokumentEntity == null) return NotFound();

            return Ok();
        }

        // DELETE: api/DokumentEntities/5
        [HttpDelete]
        public async Task<IActionResult> DeleteDokumentEntity([FromQuery] string brojDokumenta)
        {
            var successful = await _dokumentRepository.DeleteAsync(brojDokumenta);

            if (!successful) return BadRequest();

            return NoContent();
        }


    }
}
