using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopSkills.BO.CoreBusiness;
using HopSkills.BO.UseCases.PluginInterfaces;
using HopSkills.BO.UseCases.Users.Interfaces;

namespace HopSkills.BO.UseCases.Users
{
    public class AddUserUseCase : IAddUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;

        public AddUserUseCase(IUserRepository userRepository, 
            ICustomerRepository customerRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }

        public async Task ExecuteAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }
    }
}
