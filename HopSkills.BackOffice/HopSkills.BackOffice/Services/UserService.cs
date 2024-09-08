using HopSkills.BackOffice.Data;
using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services.Interfaces;
using HopSkills.BO.CoreBusiness;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System.Text;

namespace HopSkills.BackOffice.Services
{
    public class UserService : IUserService
    {
        private IEnumerable<IdentityError>? _identityErrors;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly UserRoleManager 
        private readonly ILogger<RoleService> _logger;
        private readonly IUserStore<ApplicationUser> _UserStore;
        private readonly ICustomerService _customerService;

        public UserService(UserManager<ApplicationUser> userManager, 
            ILogger<RoleService> logger,
            IUserStore<ApplicationUser> userStore,
            ICustomerService customerService)
        {
            _userManager = userManager;
            _logger = logger;
            _UserStore = userStore;
            _customerService = customerService;
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            var userList = _userManager.Users.Select(x =>
            new UserModel { FirstName = x.FirstName, 
                LastName = x.LastName,  
                Email = x.Email,
            Role = x.Role,
            Company = x.CustomerId.ToString()}).ToList();
            return userList;
        }

        public async Task<List<UserModel>> GetUsersByCustomerAsync(string customerId)
        {
            var userList = _userManager.Users.Where(x => x.CustomerId.ToString().Equals(customerId)).Select(x =>
            new UserModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Role = x.Role,
                Company = x.CustomerId.ToString()
            }).ToList();
            return userList;
        }

        public async Task<IdentityResult> CreateAsync(CreateUserModel user)
        {
            var newuser = CreateUser();
            var findUser = await _userManager.FindByEmailAsync(user.Email);
            var result = new IdentityResult();
            if (findUser is null)
            {
                await _userManager.SetEmailAsync(newuser, user.Email);
                await _userManager.SetUserNameAsync(newuser, user.FirstName + '_' + user.LastName);
                await _UserStore.SetUserNameAsync(newuser, user.Email, CancellationToken.None);
                newuser.EmailConfirmed = true;
                newuser.FirstName = user.LastName;
                newuser.LastName = user.LastName;
                newuser.Role = user.Role;
                newuser.CustomerId = new Guid(user.Company);
                result = await _userManager.CreateAsync(newuser, user.Password);
                if (!result.Succeeded)
                {
                    _logger.LogError(($"[UserService] : {string.Join(",", result.Errors)}"));
                    return result;
                }
                await _userManager.AddToRoleAsync(newuser, user.Role);
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

        public async Task<UserModel> GetUserAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => 
            x.Email.ToString().Equals(userName));
            if(user is not null)
            {
                var foundUser = new UserModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role,
                    Company = user.CustomerId.ToString(),
                    Email = user.Email
                };
                return foundUser;
            }
            return null;
        }
    }
}
