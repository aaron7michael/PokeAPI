using PokeAPI.Models;
using PokeAPI.Services;
using Microsoft.AspNetCore.Mvc;
using PokeAPI.DTOs;
using System.ComponentModel.DataAnnotations;
namespace PokeAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MoveController : ControllerBase
    {
        private readonly PokeAPIService _service;

        public MoveController(PokeAPIService service) =>
            _service = service;

        [HttpGet]
        public async Task<ActionResult<List<Move>>> Get()
        {
            List<Move> moves = await _service.GetMoveAsync();
            return Ok(moves);
        }

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
            Move newMove = ModelDTOConverter.MoveFromMoveDTO(newMoveDTO);
            bool exists = await _service.GetMoveByNameAsync(newMove.Name) != null;
            if (exists)
            {
                return BadRequest("Move with that name already exists.");
            }
            
            List<ValidationResult>? validationResults = ValidateMove(newMove);
            if (validationResults != null)
            {
                return BadRequest(validationResults);
            }

            await _service.CreateMoveAsync(newMove);
            return CreatedAtAction(nameof(Get), new { name = newMove.Name }, newMove);
        }

        [HttpPatch("{name}")]
        public async Task<ActionResult<Move>> UpdateMove(string name, MoveDTO moveDTO)
        {
            Move? existingMove = await _service.GetMoveByNameAsync(name);
            if (existingMove == null)
            {
                return NotFound();
            }

            Move updatedMove = ModelDTOConverter.MoveFromMoveDTO(moveDTO);
            updatedMove.Id = existingMove.Id;

            List<ValidationResult>? validationResults = ValidateMove(updatedMove);
            if(validationResults != null)
            {
                return BadRequest(validationResults);
            }

            return Ok(updatedMove);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteMove(string name)
        {
            Move? move = await _service.GetMoveByNameAsync(name);
            if (move == null)
            {
                return NotFound();
            }

            await _service.RemoveMoveAsync(move.Id);
            return Ok();
        }

        private List<ValidationResult>? ValidateMove(Move move)
        {
            var ctx = new ValidationContext(move);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(move, ctx, results, validateAllProperties: true))
                return results;
            return null;
        }
    }
}
