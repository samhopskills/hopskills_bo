using HopSkills.CoreBusiness;

namespace HopSkills.Plugins.InMemory
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task DeleteUserAsync(User user);
        User? GetUserById(int userId);
        Task<IEnumerable<User>> GetUserByNameAsync(string name);
        Task UpdateUser(User user);
        Task<List<User>> GetUsers();
    }
}