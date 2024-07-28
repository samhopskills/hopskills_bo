using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.PluginInterfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task DeleteUserAsync(List<User> users);
        User? GetUserById(int userId);
        Task<IEnumerable<User>> GetUserByNameAsync(string name);
        Task<List<User>> GetUsers();
        Task UpdateUserAsync(User user);
    }
}