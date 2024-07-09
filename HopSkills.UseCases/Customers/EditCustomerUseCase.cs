using HopSkills.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopSkills.CoreBusiness;
using HopSkills.UseCases.Customers.Interfaces;

namespace HopSkills.UseCases.Customers
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
