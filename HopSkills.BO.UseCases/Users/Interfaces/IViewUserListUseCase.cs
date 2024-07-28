using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.Users.Interfaces
{
    public interface IViewUserListUseCase
    {
        Task<List<User>> ExecuteAsync();
        Task DeleteAsync(List<User> users);
        Task UpdateAsync(User user);
    }
}