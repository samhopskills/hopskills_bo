using HopSkills.BackOffice.Client.ViewModels;
using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services;
using HopSkills.BackOffice.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HopSkills.BackOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;
        private readonly ILogger<TrainingController> _logger;

        public TrainingController(ITrainingService trainingService, ILogger<TrainingController> logger)
        {
            _trainingService = trainingService;
            _logger = logger;
        }
        // GET: api/<TrainingController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _trainingService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        // GET api/<TrainingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("GetTrainingsByUser/{userMail}")]
        public async Task<IActionResult> GetTrainingsByUser(string userMail)
        {
            try
            {
                return Ok(await _trainingService.GetTrainingsByUser(userMail));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTrainingsByCustomer/{companyId}")]
        public async Task<IActionResult> GetTrainingsByCustomer(string companyId)
        {
            try
            {
                return Ok(await _trainingService.GetTrainingsByCustomer(companyId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        // POST api/<TrainingController>
        [HttpPost("AddTraining")]
        public async Task<IActionResult> AddTraining([FromBody] CreateTrainingModel createTrainingModel)
        {
            try
            {
                await _trainingService.AddTraining(createTrainingModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(string id)
        {
            try
            {
                return Ok(await _trainingService.UpdateTrainingStatus(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] List<TrainingModel> trainings)
        {
            try
            {
                return Ok(await _trainingService.DeleteTraining(trainings));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
