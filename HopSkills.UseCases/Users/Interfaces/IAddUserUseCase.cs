using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Users.Interfaces
{
    public interface IAddUserUseCase
    {
        Task ExecuteAsync(User user);
        Task<List<Customer>> GetCustomersAsync(int userId);
    }
}