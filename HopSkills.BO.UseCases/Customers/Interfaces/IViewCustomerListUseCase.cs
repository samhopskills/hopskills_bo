using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.Customers.Interfaces
{
    public interface IViewCustomerListUseCase
    {
        Task<List<Customer>> ExecuteAsync();
    }
}