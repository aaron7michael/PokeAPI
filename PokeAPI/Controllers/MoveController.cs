using PokeAPI.Models;
using PokeAPI.Services;
using Microsoft.AspNetCore.Mvc;
namespace PokeAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MoveController : ControllerBase
    {
        private readonly PokeAPIService _service;

        public MoveController(PokeAPIService service) =>
            _service = service;

        [HttpGet("{name}")]
        public async Task<ActionResult<Move>> Get(string name)
        {
            Move? move = await _service.GetMoveByNameAsync(name);
            if (move == null)
            {
                return NotFound();
            }
            return Ok(move);
        }

        [HttpPost]
        public async Task<ActionResult<Move>> CreateMove(MoveDTO newMoveDTO)
        {
            Move newMove = new Move(newMoveDTO);
            bool exists = await _service.GetMoveByNameAsync(newMove.Name) != null;
            if (exists)
            {
                return BadRequest("Move with that name already exists.");
            }
            await _service.CreateMoveAsync(newMove);
            return CreatedAtAction(nameof(Get), new { name = newMove.Name }, newMove);
        }
    }
}
