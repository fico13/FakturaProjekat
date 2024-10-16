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
            var komitenti = await _komitentRepository.GetBySifraAsync(name);
            if (komitenti == null || !komitenti.Any())
            {
                return NotFound();
            }
            return Ok(komitenti.Select(k => k.ToKomitentDTO()).ToList());
        }

        // PUT: api/Komitent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutKomitentEntity([FromQuery] string sifraKomitenta, [FromBody] KomitentDTO komitentDTO)
        {

            var komitentEntity = await _komitentRepository.UpdateAsync(sifraKomitenta, komitentDTO.ToKomitentEntity());

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
        [HttpDelete]
        public async Task<IActionResult> DeleteKomitentEntity([FromQuery] string sifraKomitenta)
        {
            var successful = await _komitentRepository.DeleteAsync(sifraKomitenta);

            if (!successful) return NotFound();

            return NoContent();
        }
    }
}
