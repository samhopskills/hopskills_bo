using HopSkills.BO.CoreBusiness;
using HopSkills.BO.UseCases.Customers.Interfaces;
using HopSkills.BO.UseCases.PluginInterfaces;

namespace HopSkills.BO.UseCases.Customers
{
    public class AddCustomerUseCase : IAddCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public AddCustomerUseCase(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        public async Task ExecuteAsync(Customer customer)
        {
            await _customerRepository.CreateAsync(customer);
        }
    }
}
