using HopSkills.BO.CoreBusiness;
using HopSkills.BO.UseCases.Customers.Interfaces;
using HopSkills.BO.UseCases.PluginInterfaces;

namespace HopSkills.BO.UseCases.Customers
{
    public class ViewCustomerListUseCase : IViewCustomerListUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public ViewCustomerListUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> ExecuteAsync()
        {
            return await _customerRepository.GetAllAsync();
        }
    }
}
