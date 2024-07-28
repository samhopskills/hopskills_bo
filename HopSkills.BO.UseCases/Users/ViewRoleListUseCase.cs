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
