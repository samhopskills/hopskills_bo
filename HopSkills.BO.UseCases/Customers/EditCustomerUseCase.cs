using HopSkills.BO.UseCases.PluginInterfaces;
using HopSkills.BO.CoreBusiness;
using HopSkills.BO.UseCases.Customers.Interfaces;

namespace HopSkills.BO.UseCases.Customers
{
    public class EditCustomerUseCase : IEditCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public EditCustomerUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task ExecuteAsync(Customer customer)
        {
            await _customerRepository.EditAsync(customer);
        }
    }
}
