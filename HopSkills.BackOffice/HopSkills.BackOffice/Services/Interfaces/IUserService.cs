
using HopSkills.BackOffice.Client.ViewModels;
using HopSkills.BackOffice.Model;
using Microsoft.AspNetCore.Identity;

namespace HopSkills.BackOffice.Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync(UserModel user);
        Task<List<UserModel>> GetUsersAsync();
    }
}