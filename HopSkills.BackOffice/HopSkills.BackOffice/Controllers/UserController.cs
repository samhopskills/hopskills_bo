using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services;
using HopSkills.BackOffice.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HopSkills.BackOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _userService.GetUsersAsync();
            return Ok(list);
        }

        // GET api/<UserController>/5
        [HttpGet("userdetails/{userName}")]
        public async Task<IActionResult> Get(string userName)
        {
            try
            {
                return Ok(await _userService.GetUserAsync(userName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<UserController>
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] CreateUserModel user)
        {
            var result = await _userService.CreateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        // GET: api/<CustomerController>
        [HttpGet("getusersbycustomer/{customerId}")]
        public async Task<IActionResult> GetUsersByCustomer(string customerId)
        {
            var list = await _userService.GetUsersByCustomerAsync(customerId);
            return Ok(list);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
