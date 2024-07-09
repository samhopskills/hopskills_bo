using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Customers.Interfaces
{
    public interface IAddCustomerUseCase
    {
        Task ExecuteAsync(Customer customer);
    }
}