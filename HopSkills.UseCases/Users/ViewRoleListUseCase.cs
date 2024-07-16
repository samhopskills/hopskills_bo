using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopSkills.CoreBusiness;
using HopSkills.UseCases.PluginInterfaces;

namespace HopSkills.UseCases.Users
{
    public class ViewRoleListUseCase : IViewRoleListUseCase
    {
        private readonly IRoleRepository roleRepository;

        public ViewRoleListUseCase(IRoleRepository _roleRepository)
        {
            roleRepository = _roleRepository;
        }

        public async Task<List<Role>> ExecuteAsync()
        {
            return await roleRepository.GetAllAsync();
        }

    }
}
