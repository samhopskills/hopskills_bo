using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.Users.Interfaces
{
    public interface IAddUserUseCase
    {
        Task ExecuteAsync(User user);
        Task<List<Customer>> GetCustomersAsync();
    }
}