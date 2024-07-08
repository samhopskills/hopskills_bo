using HopSkills.CoreBusiness;
using HopSkills.UseCases.Company.Interfaces;
using HopSkills.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.UseCases.Company
{
    public class AddCustomerUseCase : IAddCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public AddCustomerUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task CreateCustomer(Customer customer)
        {
            await _customerRepository.CreateAsync(customer);
        }
    }
}
