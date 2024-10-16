using Application.Contracts.Interfaces;
using Application.DTOs;
using Application.Mappers;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobaController : ControllerBase
    {
        private readonly IRobaRepository _robaRepository;

        public RobaController(IRobaRepository robaRepository)
        {
            _robaRepository = robaRepository;
        }

        // GET: api/Roba
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RobaEntity>>> GetRoba()
        {
            var roba = await _robaRepository.GetAllAsync();

            if (roba == null || !roba.Any()) return NotFound();

            return Ok(roba.Select(r => r.ToRobaDTO()).ToList());
        }

        [HttpGet("search")]
        public async Task<ActionResult<IReadOnlyList<KomitentDTO>>> GetRobaByName([FromQuery] string name)
        {
            var roba = await _robaRepository.GetBySifraAsync(name);
            if (roba == null || !roba.Any())
            {
                return NotFound();
            }
            return Ok(roba.Select(k => k.ToRobaDTO()).ToList());
        }

        // PUT: api/Roba/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutRobaEntity([FromQuery] string sifraRobe, [FromBody] RobaDTO robaDTO)
        {
            var robaEntity = await _robaRepository.UpdateAsync(sifraRobe, robaDTO.ToRobaEntity());

            if (robaEntity == null) return NotFound();

            return Ok(robaEntity.ToRobaDTO());
        }

        // POST: api/Roba
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RobaEntity>> PostRobaEntity([FromBody] RobaDTO robaDTO)
        {
            var robaEntity = await _robaRepository.AddAsync(robaDTO.ToRobaEntity());

            return Ok(robaEntity.ToRobaDTO());
        }

        // DELETE: api/Roba/5
        [HttpDelete]
        public async Task<IActionResult> DeleteRobaEntity([FromQuery] string sifraRobe)
        {
            var successful = await _robaRepository.DeleteAsync(sifraRobe);

            if (!successful) return NotFound();

            return NoContent();
        }


    }
}
