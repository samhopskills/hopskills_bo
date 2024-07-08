using HopSkills.CoreBusiness;
using HopSkills.UseCases.Company.Interfaces;
using HopSkills.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.UseCases.Customers
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
