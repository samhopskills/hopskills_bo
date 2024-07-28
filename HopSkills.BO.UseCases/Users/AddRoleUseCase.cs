using HopSkills.BO.CoreBusiness;
using HopSkills.BO.UseCases.PluginInterfaces;
using HopSkills.BO.UseCases.Users.Interfaces;

namespace HopSkills.BO.UseCases.Users
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
