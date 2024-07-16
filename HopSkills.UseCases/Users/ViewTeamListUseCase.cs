using HopSkills.CoreBusiness;
using HopSkills.Plugins.InMemory;
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
        private readonly ITeamRepository _teamRepository;

        public ViewTeamListUseCase(ITeamRepository teamRepository) => _teamRepository = teamRepository;

        public async Task<List<Team>> ExecuteAsync()
        {
            return await _teamRepository.GetTeamsAsync();
        }

        public async Task DeleteAsync(List<Team> teams)
        {
            await _teamRepository.DeleteTeamAsync(teams);
        }

        public async Task UpdateAsync(Team team)
        {
            await _teamRepository.UpdateTeamAsync(team);
        }
    }
}
