using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Customers.Interfaces
{
    public interface IViewCustomerListUseCase
    {
        Task<List<Customer>> ExecuteAsync();
    }
}