using HopSkills.CoreBusiness;

namespace HopSkills.Plugins.InMemory
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