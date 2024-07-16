using HopSkills.CoreBusiness;
using HopSkills.UseCases.PluginInterfaces;
using HopSkills.UseCases.Users.Interfaces;

namespace HopSkills.UseCases.Users
{
    public class AddRoleUseCase : IAddRoleUseCase
    {
        private readonly IRoleRepository roleRepository;

        public AddRoleUseCase(IRoleRepository _roleRepository) => roleRepository = _roleRepository;

        public async Task ExecuteAsync(Role role)
        {
            await roleRepository.CreateAsync(role);
        }
    }
}
