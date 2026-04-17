using PokeAPI.Models;
using PokeAPI.Services;
using Microsoft.AspNetCore.Mvc;
namespace PokeAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class BattleController
    {
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Battle>> Get(string id)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<ActionResult<Battle>> CreateBattle()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<ActionResult<Battle>> PostMove(Move move)
        {
            throw new NotImplementedException();
        }
    }
}
