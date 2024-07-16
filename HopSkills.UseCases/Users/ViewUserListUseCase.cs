using HopSkills.CoreBusiness;
using HopSkills.Plugins.InMemory;
using HopSkills.UseCases.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.UseCases.Users
{
    public class ViewUserListUseCase : IViewUserListUseCase
    {

        private readonly IUserRepository _userRepository;

        public ViewUserListUseCase(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<List<User>> ExecuteAsync()
        {
            return await _userRepository.GetUsers();
        }

        public async Task DeleteAsync(List<User> users)
        {
            await _userRepository.DeleteUserAsync(users);
        }

        public async Task UpdateAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }
    }
}
