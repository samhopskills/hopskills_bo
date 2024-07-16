using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Users.Interfaces
{
    public interface IViewUserListUseCase
    {
        Task<List<User>> ExecuteAsync();
        Task DeleteAsync(List<User> users);
        Task UpdateAsync(User user);
    }
}