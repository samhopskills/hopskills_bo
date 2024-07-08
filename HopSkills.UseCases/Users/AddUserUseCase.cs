using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopSkills.CoreBusiness;
using HopSkills.Plugins.InMemory;
using HopSkills.UseCases.PluginInterfaces;
using HopSkills.UseCases.Users.Interfaces;

namespace HopSkills.UseCases.Users
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

        public async Task<List<Customer>> GetCustomersAsync(int userId)
        {
            return await _customerRepository.GetAllAsync();
        }
    }
}
