using HopSkills.CoreBusiness;
using HopSkills.UseCases.Customers.Interfaces;
using HopSkills.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.UseCases.Customers
{
    public class AddCustomerUseCase : IAddCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public AddCustomerUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task ExecuteAsync(Customer customer)
        {
            await _customerRepository.CreateAsync(customer);
        }
    }
}
