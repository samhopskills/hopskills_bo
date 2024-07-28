using HopSkills.BackOffice.Data;
using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HopSkills.BackOffice.Services
{
    public class RoleService : IRoleService
    {
        private IEnumerable<IdentityError>? _identityErrors;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RoleService> _logger;

        public RoleService(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IEnumerable<IdentityError>? identityErrors,
            ILogger<RoleService> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _identityErrors = identityErrors;
            _logger = logger;
        }

        public async Task<IdentityResult> AddRoleAsync(string roleName)
        {
            var newRole = CreateRole();
            var result = new IdentityResult();
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.SetRoleNameAsync(newRole, roleName);
                result = await _roleManager.CreateAsync(newRole);
                if (!result.Succeeded)
                {
                    _identityErrors = result.Errors;
                    _logger.LogError($"[RoleService] : {string.Join(",", result.Errors)}");
                }
                _logger.LogInformation($"[RoleService] : Role {roleName} created");
            }
            return result;
        }

        private IdentityRole CreateRole()
        {
            try
            {
                return Activator.CreateInstance<IdentityRole>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityRole)}'. " +
                    $"Ensure that '{nameof(IdentityRole)}' is not an abstract class and has a parameterless constructor.");
            }
        }

        public async Task<bool> AddUserRoleAsync(string userEmail, string[] roles)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RoleModel>> GetRolesAsync()
        {
            var roleList = _roleManager.Roles.Select(x =>
            new RoleModel { Id = Guid.Parse(x.Id), Name = x.Name }).ToList();
            return roleList;
        }

        public async Task<List<string>> GetUserRolesAsync(string emailId)
        {
            throw new NotImplementedException();
        }
    }
}
