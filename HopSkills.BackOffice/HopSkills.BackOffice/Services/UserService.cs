using HopSkills.BackOffice.Data;
using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace HopSkills.BackOffice.Services
{
    public class UserService : IUserService
    {
        private IEnumerable<IdentityError>? _identityErrors;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RoleService> _logger;

        public UserService(UserManager<ApplicationUser> userManager, ILogger<RoleService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            var userList = _userManager.Users.Select(x =>
            new UserModel { FirstName = x.FirstName, LastName = x.LastName,  Email = x.Email}).ToList();
            return userList;
        }

        public async Task<IdentityResult> CreateAsync(UserModel user)
        {
            var newuser = CreateUser();
            var findUser = await _userManager.FindByEmailAsync(user.Email);
            var result = new IdentityResult();
            if (findUser is null)
            {
                await _userManager.SetEmailAsync(newuser, user.Email);
                await _userManager.SetUserNameAsync(newuser, user.FirstName+'_'+ user.LastName);
                newuser.FirstName = user.LastName;
                newuser.LastName = user.LastName;
                result = await _userManager.CreateAsync(newuser);
                if(!result.Succeeded)
                {
                    _logger.LogError(($"[UserService] : {string.Join(",", result.Errors)}"));
                }
            }
            return result;
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor.");
            }
        }

       
    }
}
