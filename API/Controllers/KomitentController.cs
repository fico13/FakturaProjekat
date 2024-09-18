using Application.Contracts.Interfaces;
using Application.DTOs;
using Application.Mappers;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KomitentController : ControllerBase
    {
        private readonly IKomitentRepository _komitentRepository;

        public KomitentController(IKomitentRepository komitentRepository)
        {
            _komitentRepository = komitentRepository;
        }

        // GET: api/Komitent
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<KomitentDTO>>> GetKomitenti()
        {
            var komitenti = await _komitentRepository.GetAllAsync();
            if (komitenti.Count == 0) return NotFound();

            return Ok(komitenti.Select(k => k.ToKomitentDTO()).ToList());
        }

        // GET: api/Komitent/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<KomitentDTO>> GetKomitent([FromRoute] int id)
        {
            var komitent = await _komitentRepository.GetAsync(id);

            if (komitent == null) return NotFound();

            return Ok(komitent.ToKomitentDTO());
        }

        // PUT: api/Komitent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutKomitentEntity([FromRoute] int id, [FromBody] KomitentDTO komitentDTO)
        {

            var komitentEntity = await _komitentRepository.UpdateAsync(id, komitentDTO.ToKomitentEntity());

            if (komitentEntity == null) return NotFound();

            return Ok(komitentEntity.ToKomitentDTO());
        }

        // POST: api/Komitent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KomitentEntity>> PostKomitentEntity([FromBody] KomitentDTO komitentDTO)
        {
            var komitentEntity = await _komitentRepository.AddAsync(komitentDTO.ToKomitentEntity());

            return CreatedAtAction("GetKomitent", new { id = komitentEntity.Id }, komitentEntity.ToKomitentDTO());
        }

        // DELETE: api/Komitent/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteKomitentEntity([FromRoute] int id)
        {
            var successful = await _komitentRepository.DeleteAsync(id);

            if (!successful) return NotFound();

            return NoContent();
        }
    }
}
