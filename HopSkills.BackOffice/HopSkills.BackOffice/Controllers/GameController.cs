using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HopSkills.BackOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GameController> _logger;

        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _gameService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);    
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetGamesByCustomer")]
        public async Task<IActionResult> GetGamesByCustomer()
        {
            try
            {
                return Ok(await _gameService.GetGamesByCustomer());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddGame")]
        public async Task<IActionResult> AddGame(CreateGameModel createGameModel)
        {
            try
            {
                await _gameService.AddGame(createGameModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
