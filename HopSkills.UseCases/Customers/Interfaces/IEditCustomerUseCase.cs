using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Customers.Interfaces
{
    public interface IEditCustomerUseCase
    {
        Task ExecuteAsync(Customer customer);
    }
}