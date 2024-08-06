using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HopSkills.BackOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IGroupService _groupService;

        public GroupsController(ILogger<GroupsController> logger, IGroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }
        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _groupService.GetGroupsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("GetGroupsByComppany/{id}")]
        public async Task<IActionResult> GetGroupsByComppany(string companyId)
        {
            try
            {
                return Ok(await _groupService.GetGroupsbyCompanyAsync(companyId));   
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost("CreateGroup")]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupModel group)
        {
            try
            {
                await _groupService.CreateGroupAsync(group);
                return Ok(Task.CompletedTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost("AddUserGroup")]
        public async Task<IActionResult> AddUserGroup([FromBody] AddUserGroupModel userGroup)
        {
            try
            {
                await _groupService.AddUserGroupAsync(userGroup);
                return Ok(Task.CompletedTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
