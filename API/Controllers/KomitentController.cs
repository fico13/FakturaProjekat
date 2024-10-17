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
        public async Task<ActionResult<IEnumerable<KomitentDTO>>> GetKomitentBySifra([FromQuery] string sifraKomitenta)
        {
            var komitenti = await _komitentRepository.GetBySifraAsync(sifraKomitenta);
            if (komitenti == null || !komitenti.Any())
            {
                return NotFound();
            }
            return Ok(komitenti.Select(k => k.ToKomitentDTO()).ToList());
        }


        [HttpPut]
        public async Task<IActionResult> UpdateKomitentEntity([FromBody] KomitentDTO komitentDTO)
        {

            var komitentEntity = await _komitentRepository.UpdateAsync(komitentDTO.ToKomitentEntity());

            if (komitentEntity == null) return NotFound();

            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<KomitentEntity>> AddKomitentEntity([FromBody] KomitentDTO komitentDTO)
        {
            var komitentEntity = await _komitentRepository.AddAsync(komitentDTO.ToKomitentEntity());
            if (komitentEntity == null) return NotFound();
            return Ok();
        }


        [HttpDelete("{sifraKomitenta}")]
        public async Task<IActionResult> DeleteKomitentEntity([FromRoute] string sifraKomitenta)
        {
            var successful = await _komitentRepository.DeleteAsync(sifraKomitenta);

            if (!successful) return NotFound();

            return NoContent();
        }
    }
}
