using Application.Contracts.Interfaces;
using Application.DTOs;
using Application.Mappers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokumentController : ControllerBase
    {
        private readonly IDokumentRepository _dokumentRepository;
        private readonly IKomitentRepository _komitentRepository;
        private readonly IRobaRepository _robaRepository;

        public DokumentController(ProjekatContext context, IDokumentRepository dokumentRepository, IKomitentRepository komitentRepository, IRobaRepository robaRepository)
        {
            _dokumentRepository = dokumentRepository;
            _komitentRepository = komitentRepository;
            _robaRepository = robaRepository;
        }

        // GET: api/DokumentEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DokumentEntity>>> GetDokumenti()
        {
            var dokumenti = await _dokumentRepository.GetAllAsync();

            if (dokumenti.Count == 0) return NotFound();

            return Ok(dokumenti.Select(d => d.ToDokumentDTO()).ToList());
        }

        // GET: api/DokumentEntities/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DokumentEntity>> GetDokumentEntity([FromRoute] int id)
        {
            var dokument = await _dokumentRepository.GetAsync(id);

            if (dokument == null) return NotFound();

            return Ok(dokument.ToDokumentDTO());
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
        public async Task<ActionResult<DokumentEntity>> PostDokumentEntity()
        {
            var roba = await _robaRepository.GetAllAsync();
            var komitent = await _komitentRepository.GetAsync(1);

            var dokument = new DokumentDTO
            {
                DatumIzdavanja = DateTime.UtcNow,
                Komitent = komitent!.ToKomitentDTO(),
                Stavke = new List<StavkaDokumentaDTO>
                {
                    new StavkaDokumentaDTO
                    {
                        Roba = roba[0].ToRobaDTO(),
                        CenaStavkeKom = roba[0].Cena,
                        Kolicina = 3
                    },
                    new StavkaDokumentaDTO
                    {
                        Roba = roba[1].ToRobaDTO(),
                        CenaStavkeKom = roba[1].Cena,
                        Kolicina = 5
                    },
                }
            };
            dokument.Stavke.Select(s => s.UkupnaCenaStavke = s.CenaStavkeKom * s.Kolicina).ToList();
            dokument.UkupnaCena = dokument.Stavke.Sum(s => s.UkupnaCenaStavke);

            var dokumentEntity = await _dokumentRepository.AddAsync(dokument.ToDokumentEntity());

            return CreatedAtAction("GetDokumentEntity", new { id = dokumentEntity.Id }, dokumentEntity.ToDokumentDTO());
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
