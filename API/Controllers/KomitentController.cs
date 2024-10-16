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
        public async Task<ActionResult<IEnumerable<KomitentDTO>>> GetKomitenti()
        {
            var komitenti = await _komitentRepository.GetAllAsync();
            if (komitenti == null || !komitenti.Any()) return NotFound();

            return Ok(komitenti.Select(k => k.ToKomitentDTO()).ToList());
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<KomitentDTO>>> GetKomitentByName([FromQuery] string name)
        {
            var komitenti = await _komitentRepository.GetByNameAsync(name);
            if (komitenti == null || !komitenti.Any())
            {
                return NotFound();
            }
            return Ok(komitenti.Select(k => k.ToKomitentDTO()).ToList());
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
            return Ok(komitentEntity.ToKomitentDTO());
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
