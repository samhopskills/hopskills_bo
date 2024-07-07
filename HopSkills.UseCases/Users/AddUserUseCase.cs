using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopSkills.CoreBusiness;
using HopSkills.Plugins.InMemory;
using HopSkills.UseCases.Users.Interfaces;

namespace HopSkills.UseCases.Users
{
    public class AddUserUseCase : IAddUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public AddUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }
    }
}
