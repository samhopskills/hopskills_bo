using HopSkills.BO.CoreBusiness;
using HopSkills.BO.UseCases.PluginInterfaces;
using HopSkills.BO.UseCases.Users.Interfaces;


namespace HopSkills.BO.UseCases.Users
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
