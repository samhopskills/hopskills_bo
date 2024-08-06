
using HopSkills.BackOffice.Client.ViewModels;
using HopSkills.BackOffice.Model;
using Microsoft.AspNetCore.Identity;

namespace HopSkills.BackOffice.Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync(CreateUserModel user);
        Task<UserModel> GetUserAsync(string userName);
        Task<List<UserModel>> GetUsersAsync();
        Task<List<UserModel>> GetUsersByCustomerAsync(string customerId);
    }
}