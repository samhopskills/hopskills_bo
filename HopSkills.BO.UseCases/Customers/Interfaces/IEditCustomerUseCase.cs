using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.Customers.Interfaces
{
    public interface IEditCustomerUseCase
    {
        Task ExecuteAsync(Customer customer);
    }
}