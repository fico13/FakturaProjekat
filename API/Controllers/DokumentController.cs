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

            if (dokumenti.Count == 0) return NotFound();

            return Ok(dokumenti.Select(d => d.ToDokumentDTO()).ToList());
        }

        [HttpGet("search")]
        public async Task<ActionResult<IReadOnlyList<DokumentDTO>>> GetDokumentByName([FromQuery] string name)
        {
            var dokumenti = await _dokumentRepository.GetByNameAsync(name);
            if (dokumenti == null || !dokumenti.Any())
            {
                return NotFound();
            }
            return Ok(dokumenti.Select(d => d.ToDokumentDTO()).ToList());
        }

        // PUT: api/DokumentEntities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutDokumentEntity([FromRoute] int id, [FromBody] DokumentDTO dokumentDTO)
        {
            var dokument = await _dokumentRepository.UpdateAsync(id, dokumentDTO.ToDokumentEntity());

            if (dokument == null) return NotFound();

            return Ok(dokument.ToDokumentDTO());
        }

        // POST: api/DokumentEntities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DokumentEntity>> PostDokumentEntity([FromBody] DokumentDTO dokumentDTO)
        {
            var dokumentEntity = await _dokumentRepository.AddAsync(dokumentDTO.ToDokumentEntity());

            return Ok(dokumentEntity.ToDokumentDTO());
        }

        // DELETE: api/DokumentEntities/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDokumentEntity([FromRoute] int id)
        {
            var successful = await _dokumentRepository.DeleteAsync(id);

            if (!successful) return BadRequest();

            return NoContent();
        }


    }
}
