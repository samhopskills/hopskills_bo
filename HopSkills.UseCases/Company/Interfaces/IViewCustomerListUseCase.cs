using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Company.Interfaces
{
    public interface IViewCustomerListUseCase
    {
        Task<List<Customer>> ExecuteAsync();
    }
}