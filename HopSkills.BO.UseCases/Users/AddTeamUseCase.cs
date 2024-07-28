using HopSkills.BO.CoreBusiness;
using HopSkills.BO.UseCases.PluginInterfaces;
using HopSkills.BO.UseCases.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.BO.UseCases.Users
{
    public class AddTeamUseCase : IAddTeamUseCase
    {
        private readonly ITeamRepository teamRepository;

        public AddTeamUseCase(ITeamRepository _teamRepository) => teamRepository = _teamRepository;

        public async Task ExecuteAsync(Team team)
        {
            await teamRepository.AddTeamAsync(team);
        }
    }
}
