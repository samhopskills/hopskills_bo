using HopSkills.BackOffice.Model;
using Microsoft.AspNetCore.Identity;

namespace HopSkills.BackOffice.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleModel>> GetRolesAsync();

        Task<List<string>> GetUserRolesAsync(string emailId);

        Task<IdentityResult> AddRoleAsync(string roleName);

        Task<bool> AddUserRoleAsync(string userEmail, string[] roles);
    }
}
