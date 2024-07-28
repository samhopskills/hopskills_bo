using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.Customers.Interfaces
{
    public interface IAddCustomerUseCase
    {
        Task ExecuteAsync(Customer customer);
    }
}