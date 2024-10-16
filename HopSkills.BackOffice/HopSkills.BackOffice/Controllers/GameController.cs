using HopSkills.BackOffice.Client.ViewModels;
using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HopSkills.BackOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GameController> _logger;

        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        //[Authorize("Admin")]
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

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            try
            {
                return Ok(await _gameService.GetGameById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        //[Authorize("Admin, Manager")]
        [HttpGet("GetGamesByUser/{userMail}")]
        public async Task<IActionResult> GetGamesByUser(string userMail)
        {
            try
            {
                return Ok(await _gameService.GetGamesByUser(userMail));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        //[Authorize("Admin, Manager")]
        [HttpGet("GetGamesByCustomer/{userMail}")]
        public async Task<IActionResult> GetGamesByCustomer(string companyId)
        {
            try
            {
                return Ok(await _gameService.GetGamesByCustomer(companyId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        //[Authorize("Admin, Manager")]
        [HttpPost("AddGame")]
        public async Task<IActionResult> AddGame([FromBody]CreateGameModel createGameModel)
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

        [HttpPost("EditGame")]
        public async Task<IActionResult> EditGame([FromBody] EditGameModel editGameModel)
        {
            try
            {
                await _gameService.EditGame(editGameModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPost("UpdatePartial")]
        public async Task<IActionResult> UpdatePartial([FromBody] GameChangesModel changes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _gameService.UpdateGamePartial(changes);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdatePartial");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody]List<GameViewModel> games)
        {
            try
            {
                return Ok(await _gameService.DeleteGame(games));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(string id)
        {
            try
            {
                return Ok(await _gameService.UpdateGameStatus(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("DeleteImageFromGame/{id}")]
        public async Task<IActionResult> DeleteImageFromGame(string id)
        {
            try
            {
                return Ok(await _gameService.DeleteImageFromGame(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UploadImageForGame/{id}")]
        public async Task<IActionResult> UploadImageForGame([FromBody] EditGameImage Image, string Id)
        {
            try
            {
                return Ok(await _gameService.UploadImageForGame(Image, Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
