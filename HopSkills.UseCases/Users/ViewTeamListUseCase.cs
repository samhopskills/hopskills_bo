using HopSkills.CoreBusiness;
using HopSkills.UseCases.PluginInterfaces;
using HopSkills.UseCases.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.UseCases.Users
{
    public class ViewTeamListUseCase : IViewTeamListUseCase
    {
        private readonly ITeamRepository _groupRepository;

        public ViewTeamListUseCase(ITeamRepository groupRepository) => _groupRepository = groupRepository;

        public async Task<List<Team>> ExecuteAsync()
        {
            return await _groupRepository.GetTeamsAsync();
        }
    }
}
