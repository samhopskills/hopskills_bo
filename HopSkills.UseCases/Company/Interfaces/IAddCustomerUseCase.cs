using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Company.Interfaces
{
    public interface IAddCustomerUseCase
    {
        Task CreateCustomer(Customer customer);
    }
}