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

            if (roba.Count == 0) return NotFound();

            return Ok(roba.Select(r => r.ToRobaDTO()).ToList());
        }

        // GET: api/Roba/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RobaEntity>> GetRobaEntity([FromRoute] int id)
        {
            var roba = await _robaRepository.GetAsync(id);

            if (roba == null) return NotFound();

            return Ok(roba.ToRobaDTO());
        }

        // PUT: api/Roba/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutRobaEntity([FromRoute] int id, [FromBody] RobaDTO robaDTO)
        {
            var robaEntity = await _robaRepository.UpdateAsync(id, robaDTO.ToRobaEntity());

            if (robaEntity == null) return NotFound();

            return Ok(robaEntity.ToRobaDTO());
        }

        // POST: api/Roba
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RobaEntity>> PostRobaEntity([FromBody] RobaDTO robaDTO)
        {
            var robaEntity = await _robaRepository.AddAsync(robaDTO.ToRobaEntity());

            return CreatedAtAction("GetRobaEntity", new { id = robaEntity.Id }, robaEntity.ToRobaDTO());
        }

        // DELETE: api/Roba/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRobaEntity([FromRoute] int id)
        {
            var successful = await _robaRepository.DeleteAsync(id);

            if (!successful) return NotFound();

            return NoContent();
        }


    }
}
